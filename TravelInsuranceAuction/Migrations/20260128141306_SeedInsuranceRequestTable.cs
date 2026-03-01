using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelInsuranceAuction.Migrations
{
    /// <inheritdoc />
    public partial class SeedInsuranceRequestTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "InsuranceRequests",
                columns: new[] { "RequestId", "Destination", "EndDate", "NumberOfTravelers", "StartDate" },
                values: new object[,]
                {
                    { 1, "London", new DateTime(2026, 1, 28, 0, 0, 0, 0, DateTimeKind.Local), 2, new DateTime(2026, 1, 28, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 2, "Paris", new DateTime(2026, 1, 28, 0, 0, 0, 0, DateTimeKind.Local), 2, new DateTime(2026, 1, 28, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 3, "Lisabon", new DateTime(2026, 1, 28, 0, 0, 0, 0, DateTimeKind.Local), 4, new DateTime(2026, 1, 28, 0, 0, 0, 0, DateTimeKind.Local) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "InsuranceRequests",
                keyColumn: "RequestId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "InsuranceRequests",
                keyColumn: "RequestId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "InsuranceRequests",
                keyColumn: "RequestId",
                keyValue: 3);
        }
    }
}
