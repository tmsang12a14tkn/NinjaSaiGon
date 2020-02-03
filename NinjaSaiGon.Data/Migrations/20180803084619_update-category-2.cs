using Microsoft.EntityFrameworkCore.Migrations;

namespace NinjaSaiGon.Data.Migrations
{
    public partial class updatecategory2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DrinkCategories_TypeId",
                table: "DrinkCategories",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkCategories_DrinkCategoryTypes_TypeId",
                table: "DrinkCategories",
                column: "TypeId",
                principalTable: "DrinkCategoryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrinkCategories_DrinkCategoryTypes_TypeId",
                table: "DrinkCategories");

            migrationBuilder.DropIndex(
                name: "IX_DrinkCategories_TypeId",
                table: "DrinkCategories");
        }
    }
}
