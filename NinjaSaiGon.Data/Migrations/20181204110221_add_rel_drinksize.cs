using Microsoft.EntityFrameworkCore.Migrations;

namespace NinjaSaiGon.Data.Migrations
{
    public partial class add_rel_drinksize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PromotionDrinks_DrinkSizeId",
                table: "PromotionDrinks",
                column: "DrinkSizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionDrinks_DrinkSizes_DrinkSizeId",
                table: "PromotionDrinks",
                column: "DrinkSizeId",
                principalTable: "DrinkSizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PromotionDrinks_DrinkSizes_DrinkSizeId",
                table: "PromotionDrinks");

            migrationBuilder.DropIndex(
                name: "IX_PromotionDrinks_DrinkSizeId",
                table: "PromotionDrinks");
        }
    }
}
