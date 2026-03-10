using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelInsuranceAuction.Migrations
{
    /// <inheritdoc />
    public partial class resetDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "InsuranceRequests",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateOnly(2026, 3, 6), new DateOnly(2026, 3, 6) });

            migrationBuilder.UpdateData(
                table: "InsuranceRequests",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateOnly(2026, 3, 6), new DateOnly(2026, 3, 6) });

            migrationBuilder.UpdateData(
                table: "InsuranceRequests",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateOnly(2026, 3, 6), new DateOnly(2026, 3, 6) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "InsuranceRequests",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateOnly(2026, 3, 4), new DateOnly(2026, 3, 4) });

            migrationBuilder.UpdateData(
                table: "InsuranceRequests",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateOnly(2026, 3, 4), new DateOnly(2026, 3, 4) });

            migrationBuilder.UpdateData(
                table: "InsuranceRequests",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateOnly(2026, 3, 4), new DateOnly(2026, 3, 4) });
        }
    }
}
