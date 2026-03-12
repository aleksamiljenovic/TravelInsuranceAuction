using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelInsuranceAuction.Migrations
{
    /// <inheritdoc />
    public partial class refreshDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "InsuranceRequests",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "InsuranceRequests",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "InsuranceRequests",
                keyColumn: "Id",
                keyValue: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "InsuranceRequests",
                columns: new[] { "Id", "Destination", "EndDate", "NumberOfTravelers", "StartDate", "UserId" },
                values: new object[,]
                {
                    { 1, "London", new DateOnly(2026, 3, 10), 2, new DateOnly(2026, 3, 10), null },
                    { 2, "Paris", new DateOnly(2026, 3, 10), 2, new DateOnly(2026, 3, 10), null },
                    { 3, "Lisabon", new DateOnly(2026, 3, 10), 4, new DateOnly(2026, 3, 10), null }
                });
        }
    }
}
