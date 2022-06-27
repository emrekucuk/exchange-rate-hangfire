using Hangfire;
using Hangfire.PostgreSql;
using Hangfire.RecurringJobs;
using Microsoft.Extensions.Options;

namespace exchange_rate_hangfire
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddHangfire(config =>
                config.UsePostgreSqlStorage(Configuration.GetConnectionString("PostgreSql")));
            services.AddHangfireServer();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                DashboardTitle = "Guide.fm Hangfire DashBoard",
                AppPath = "/Home",
            });

            // Hangfire Controller
            // Cron Url : https://crontab.guru/#*/5_*_*_*_*

            RecurringJob.AddOrUpdate<CurrencyExchangeJob>(nameof(CurrencyExchangeJob), o => o.UpdateCurrencyExchange(), "* * * * *", TimeZoneInfo.Utc);
        }
    }
}
