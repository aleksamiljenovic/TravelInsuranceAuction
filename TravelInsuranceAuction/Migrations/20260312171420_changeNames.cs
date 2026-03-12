using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelInsuranceAuction.Migrations
{
    /// <inheritdoc />
    public partial class changeNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DecreasePercentage",
                table: "AutoBiddingSettings",
                newName: "PriceDecrease");

            migrationBuilder.RenameColumn(
                name: "EndTIme",
                table: "Auctions",
                newName: "EndTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PriceDecrease",
                table: "AutoBiddingSettings",
                newName: "DecreasePercentage");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "Auctions",
                newName: "EndTIme");
        }
    }
}
