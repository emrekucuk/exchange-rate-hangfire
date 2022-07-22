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
builder.Services.AddHangfire(config =>
                config.UsePostgreSqlStorage(builder.Configuration.GetConnectionString("PostgresConnection")));


builder.Services.AddHangfireServer();

builder.Services.AddSingleton<ApplicationDbContext>(new ApplicationDbContext(builder.Configuration.GetConnectionString("PostgresConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    DashboardTitle = "Currency Exchange Hangfire DashBoard",
    AppPath = "/Home",
});

// Hangfire Controller
// Cron Url : https://crontab.guru/#*/5_*_*_*_*

RecurringJob.AddOrUpdate<CurrencyExchangeJob>(nameof(CurrencyExchangeJob), o => o.UpdateCurrencyExchange(), "* * * * *", TimeZoneInfo.Utc);


app.Run();
