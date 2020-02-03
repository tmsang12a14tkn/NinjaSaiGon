using Microsoft.EntityFrameworkCore.Migrations;

namespace NinjaSaiGon.Data.Migrations
{
    public partial class renametable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrinkToppingCategory_Drinks_DrinkId",
                table: "DrinkToppingCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_DrinkToppingCategory_ToppingCategories_ToppingCategoryId",
                table: "DrinkToppingCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DrinkToppingCategory",
                table: "DrinkToppingCategory");

            migrationBuilder.RenameTable(
                name: "DrinkToppingCategory",
                newName: "DrinkToppingCategories");

            migrationBuilder.RenameIndex(
                name: "IX_DrinkToppingCategory_ToppingCategoryId",
                table: "DrinkToppingCategories",
                newName: "IX_DrinkToppingCategories_ToppingCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DrinkToppingCategories",
                table: "DrinkToppingCategories",
                columns: new[] { "DrinkId", "ToppingCategoryId" });

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkToppingCategories_Drinks_DrinkId",
                table: "DrinkToppingCategories",
                column: "DrinkId",
                principalTable: "Drinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkToppingCategories_ToppingCategories_ToppingCategoryId",
                table: "DrinkToppingCategories",
                column: "ToppingCategoryId",
                principalTable: "ToppingCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrinkToppingCategories_Drinks_DrinkId",
                table: "DrinkToppingCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_DrinkToppingCategories_ToppingCategories_ToppingCategoryId",
                table: "DrinkToppingCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DrinkToppingCategories",
                table: "DrinkToppingCategories");

            migrationBuilder.RenameTable(
                name: "DrinkToppingCategories",
                newName: "DrinkToppingCategory");

            migrationBuilder.RenameIndex(
                name: "IX_DrinkToppingCategories_ToppingCategoryId",
                table: "DrinkToppingCategory",
                newName: "IX_DrinkToppingCategory_ToppingCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DrinkToppingCategory",
                table: "DrinkToppingCategory",
                columns: new[] { "DrinkId", "ToppingCategoryId" });

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkToppingCategory_Drinks_DrinkId",
                table: "DrinkToppingCategory",
                column: "DrinkId",
                principalTable: "Drinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkToppingCategory_ToppingCategories_ToppingCategoryId",
                table: "DrinkToppingCategory",
                column: "ToppingCategoryId",
                principalTable: "ToppingCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
