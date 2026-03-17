using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelInsuranceAuction.Migrations
{
    /// <inheritdoc />
    public partial class seedBidSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AutoBiddingSettings",
                columns: new[] { "Id", "AgencyId", "DefaultMinPrice", "DefaultStartPrice", "LoweringTime", "PriceDecrease", "SpecialConditions" },
                values: new object[,]
                {
                    { 1, 1, 50.0, 90.0, 20, 10.0, "Kašnjenje letova" },
                    { 2, 2, 40.0, 80.0, 30, 15.0, "" },
                    { 3, 3, 35.0, 75.0, 10, 5.0, "" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AutoBiddingSettings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AutoBiddingSettings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AutoBiddingSettings",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
