using Hangfire;
using Hangfire.PostgreSql;
using Hangfire.RecurringJobs;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Hangfire services.
// builder.Services.AddHangfire(config =>
//                 config.UsePostgreSqlStorage("User ID=postgres;Password=sa;Server=localhost;Port=5432;Database=currency-exchange;"));


// builder.Services.AddHangfireServer();

builder.Services.AddSingleton<ApplicationDbContext>(new ApplicationDbContext("User ID=postgres;Password=sa;Server=localhost;Port=5432;Database=currency-exchange;"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
// app.MapHangfireDashboard();

// app.UseHangfireDashboard("/hangfire", new DashboardOptions
// {
//     DashboardTitle = "Guide.fm Hangfire DashBoard",
//     AppPath = "/Home",
// });

// Hangfire Controller
// Cron Url : https://crontab.guru/#*/5_*_*_*_*

// RecurringJob.AddOrUpdate<CurrencyExchangeJob>(nameof(CurrencyExchangeJob), o => o.UpdateCurrencyExchange(), "* * * * *", TimeZoneInfo.Utc);


app.Run();
