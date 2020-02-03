using Microsoft.EntityFrameworkCore.Migrations;

namespace NinjaSaiGon.Data.Migrations
{
    public partial class edittoppingcat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Toppings_ToppingCategories_ToppingCategoryId",
                table: "Toppings");

            migrationBuilder.DropIndex(
                name: "IX_Toppings_ToppingCategoryId",
                table: "Toppings");

            migrationBuilder.DropColumn(
                name: "ToppingCategoryId",
                table: "Toppings");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Toppings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Toppings_CategoryId",
                table: "Toppings",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Toppings_ToppingCategories_CategoryId",
                table: "Toppings",
                column: "CategoryId",
                principalTable: "ToppingCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Toppings_ToppingCategories_CategoryId",
                table: "Toppings");

            migrationBuilder.DropIndex(
                name: "IX_Toppings_CategoryId",
                table: "Toppings");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Toppings");

            migrationBuilder.AddColumn<int>(
                name: "ToppingCategoryId",
                table: "Toppings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Toppings_ToppingCategoryId",
                table: "Toppings",
                column: "ToppingCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Toppings_ToppingCategories_ToppingCategoryId",
                table: "Toppings",
                column: "ToppingCategoryId",
                principalTable: "ToppingCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
