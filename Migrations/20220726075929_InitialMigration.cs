using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace exchange_rate_hangfire.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyExchanges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CurrencyId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyExchanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrencyExchanges_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { new Guid("078d66fd-cbb2-4b19-bcce-40c7150c7a8e"), "CAD", "Kanada Doları" },
                    { new Guid("51f045e1-ab39-4eb9-9c3a-493cf018f0f2"), "SAR", "Suudi Riyali" },
                    { new Guid("ec00761e-9ed7-49fd-841b-1b86cfb4e58c"), "EUR", "Euro" },
                    { new Guid("ef0061b8-45a6-42d4-8aaf-7f4d002b7295"), "GBP", "Ingiliz Poundu" },
                    { new Guid("f973d74b-b7df-40a6-a530-017dcdd870e7"), "USD", "Dolar" },
                    { new Guid("fefd1d40-48c4-492a-83df-f6b605fcef26"), "AUD", "Avustralya Doları" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyExchanges_CurrencyId",
                table: "CurrencyExchanges",
                column: "CurrencyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrencyExchanges");

            migrationBuilder.DropTable(
                name: "Currencies");
        }
    }
}
