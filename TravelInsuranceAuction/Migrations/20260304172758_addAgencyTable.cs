using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelInsuranceAuction.Migrations
{
    /// <inheritdoc />
    public partial class addAgencyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agencies",
                columns: table => new
                {
                    AgencyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agencies", x => x.AgencyId);
                });

            migrationBuilder.InsertData(
                table: "Agencies",
                columns: new[] { "AgencyId", "City", "Name", "PhoneNumber", "StreetAddress" },
                values: new object[,]
                {
                    { 1, "Beograd", "ArgusTours", "1234567890", "Kneza Milosa 18" },
                    { 2, "Beograd", "VivaTravel", "1234567890", "Nehruova 44" },
                    { 3, "Beograd", "Travellino", "1234567890", "Milutina Milankovica 23" }
                });

            migrationBuilder.UpdateData(
                table: "InsuranceRequests",
                keyColumn: "RequestId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateOnly(2026, 3, 4), new DateOnly(2026, 3, 4) });

            migrationBuilder.UpdateData(
                table: "InsuranceRequests",
                keyColumn: "RequestId",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateOnly(2026, 3, 4), new DateOnly(2026, 3, 4) });

            migrationBuilder.UpdateData(
                table: "InsuranceRequests",
                keyColumn: "RequestId",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateOnly(2026, 3, 4), new DateOnly(2026, 3, 4) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agencies");

            migrationBuilder.UpdateData(
                table: "InsuranceRequests",
                keyColumn: "RequestId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateOnly(2026, 3, 3), new DateOnly(2026, 3, 3) });

            migrationBuilder.UpdateData(
                table: "InsuranceRequests",
                keyColumn: "RequestId",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateOnly(2026, 3, 3), new DateOnly(2026, 3, 3) });

            migrationBuilder.UpdateData(
                table: "InsuranceRequests",
                keyColumn: "RequestId",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateOnly(2026, 3, 3), new DateOnly(2026, 3, 3) });
        }
    }
}
