using System.Reflection;
using Hangfire;
using Hangfire.PostgreSql;
using Hangfire.RecurringJobs;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
            {
                // c.AddServer(new OpenApiServer() { Url = environmentService.BackofficeApiUrl, Description = environmentService.EnvironmentName });
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Currency Exchange Rate", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                // c.IncludeXmlComments(xmlPath);

                // For avoid Scheme Collision for the same name classes
                c.CustomSchemaIds(x => x.FullName);

                // Define Security Scheme
                c.AddSecurityDefinition("Bearer", // Name #1
                    new OpenApiSecurityScheme()
                    {
                        Description = "JWT Authorization header using the Bearer scheme.",
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer"
                    });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Id = "Bearer", // Name #1
								Type = ReferenceType.SecurityScheme
                            }
                        },new List<string>()
                    }
                });
            });


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
