using Hangfire;
using Hangfire.PostgreSql;
using Hangfire.Schedules;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql("User ID=postgres;Password=sa;Server=localhost;Port=5432;Database=greenlibrary;Integrated Security=true;Pooling=true;"));


// Add Hangfire services.
builder.Services.AddHangfire(config =>
                config.UsePostgreSqlStorage("User ID=postgres;Password=sa;Server=localhost;Port=5432;Database=Hangfire;"));


builder.Services.AddHangfireServer();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
app.MapHangfireDashboard();
app.UseHangfireDashboard();
RecurringJobs.UpdateKur();

app.Run();
