using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelInsuranceAuction.Migrations
{
    /// <inheritdoc />
    public partial class databaseReset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Agencies",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Generali");

            migrationBuilder.UpdateData(
                table: "Agencies",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Dunav osiguranje");

            migrationBuilder.UpdateData(
                table: "Agencies",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Wiener");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Agencies",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "ArgusTours");

            migrationBuilder.UpdateData(
                table: "Agencies",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "VivaTravel");

            migrationBuilder.UpdateData(
                table: "Agencies",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Travellino");
        }
    }
}
