using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelInsuranceAuction.Migrations
{
    /// <inheritdoc />
    public partial class addForeingKeyToOfferAndInsuranceRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offer_Auctions_AuctionId",
                table: "Offer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Offer",
                table: "Offer");

            migrationBuilder.RenameTable(
                name: "Offer",
                newName: "Offers");

            migrationBuilder.RenameIndex(
                name: "IX_Offer_AuctionId",
                table: "Offers",
                newName: "IX_Offers_AuctionId");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "InsuranceRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AgencyId",
                table: "Offers",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Offers",
                table: "Offers",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "InsuranceRequests",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "InsuranceRequests",
                keyColumn: "Id",
                keyValue: 2,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "InsuranceRequests",
                keyColumn: "Id",
                keyValue: 3,
                column: "UserId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceRequests_UserId",
                table: "InsuranceRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_AgencyId",
                table: "Offers",
                column: "AgencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_InsuranceRequests_AspNetUsers_UserId",
                table: "InsuranceRequests",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Agencies_AgencyId",
                table: "Offers",
                column: "AgencyId",
                principalTable: "Agencies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Auctions_AuctionId",
                table: "Offers",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsuranceRequests_AspNetUsers_UserId",
                table: "InsuranceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Agencies_AgencyId",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Auctions_AuctionId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_InsuranceRequests_UserId",
                table: "InsuranceRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Offers",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_AgencyId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "InsuranceRequests");

            migrationBuilder.DropColumn(
                name: "AgencyId",
                table: "Offers");

            migrationBuilder.RenameTable(
                name: "Offers",
                newName: "Offer");

            migrationBuilder.RenameIndex(
                name: "IX_Offers_AuctionId",
                table: "Offer",
                newName: "IX_Offer_AuctionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Offer",
                table: "Offer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Offer_Auctions_AuctionId",
                table: "Offer",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "Id");
        }
    }
}
