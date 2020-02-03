using Microsoft.EntityFrameworkCore.Migrations;

namespace NinjaSaiGon.Data.Migrations
{
    public partial class orderpromotion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PromotionApplyRule",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PromotionCode",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PromotionDiscountValue",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShipFee",
                table: "Orders",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PromotionApplyRule",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PromotionCode",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PromotionDiscountValue",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShipFee",
                table: "Orders");
        }
    }
}
