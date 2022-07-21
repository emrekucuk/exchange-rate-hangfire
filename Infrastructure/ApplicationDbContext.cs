using Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencyExchange> CurrencyExchanges { get; set; }

        public ApplicationDbContext(string connectionString) : base(BuildDbContextOptions(connectionString))
        { }

        public static DbContextOptions<ApplicationDbContext> BuildDbContextOptions(string connectionString)
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            dbContextOptionsBuilder.UseNpgsql(connectionString);

            return dbContextOptionsBuilder.Options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Currency>().HasData(
                new Currency()
                {
                    Id = Guid.Parse("f973d74b-b7df-40a6-a530-017dcdd870e7"),
                    Name = "Dolar",
                    Code = "USD",
                },
                new Currency()
                {
                    Id = Guid.Parse("ec00761e-9ed7-49fd-841b-1b86cfb4e58c"),
                    Name = "Euro",
                    Code = "EUR",
                }
            );
        }
    }
}