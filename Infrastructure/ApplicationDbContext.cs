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
                },
                new Currency()
                {
                    Id = Guid.Parse("ef0061b8-45a6-42d4-8aaf-7f4d002b7295"),
                    Name = "Ingiliz Poundu",
                    Code = "GBP",
                },
                new Currency()
                {
                    Id = Guid.Parse("fefd1d40-48c4-492a-83df-f6b605fcef26"),
                    Name = "Avustralya Doları",
                    Code = "AUD",
                },
                new Currency()
                {
                    Id = Guid.Parse("078d66fd-cbb2-4b19-bcce-40c7150c7a8e"),
                    Name = "Kanada Doları",
                    Code = "CAD",
                },
                new Currency()
                {
                    Id = Guid.Parse("51f045e1-ab39-4eb9-9c3a-493cf018f0f2"),
                    Name = "Suudi Riyali",
                    Code = "SAR",
                }
            );
        }
    }
}