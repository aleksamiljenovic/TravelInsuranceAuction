using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelInsuranceAuction.Migrations
{
    /// <inheritdoc />
    public partial class addAutoBiddingSettingToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AutoBiddingSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DefaultStartPrice = table.Column<double>(type: "float", nullable: false),
                    DefaultMinPrice = table.Column<double>(type: "float", nullable: false),
                    DecreasePercentage = table.Column<double>(type: "float", nullable: false),
                    LoweringTime = table.Column<int>(type: "int", nullable: false),
                    SpecialConditions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgencyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoBiddingSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AutoBiddingSettings_Agencies_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agencies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AutoBiddingSettings_AgencyId",
                table: "AutoBiddingSettings",
                column: "AgencyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutoBiddingSettings");
        }
    }
}
