using Microsoft.EntityFrameworkCore.Migrations;

namespace NinjaSaiGon.Data.Migrations
{
    public partial class addcatphoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryPhoto_DrinkCategories_DrinkCategoryId",
                table: "CategoryPhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryPhoto",
                table: "CategoryPhoto");

            migrationBuilder.RenameTable(
                name: "CategoryPhoto",
                newName: "DrinkCategoryPhotos");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryPhoto_DrinkCategoryId",
                table: "DrinkCategoryPhotos",
                newName: "IX_DrinkCategoryPhotos_DrinkCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DrinkCategoryPhotos",
                table: "DrinkCategoryPhotos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkCategoryPhotos_DrinkCategories_DrinkCategoryId",
                table: "DrinkCategoryPhotos",
                column: "DrinkCategoryId",
                principalTable: "DrinkCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrinkCategoryPhotos_DrinkCategories_DrinkCategoryId",
                table: "DrinkCategoryPhotos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DrinkCategoryPhotos",
                table: "DrinkCategoryPhotos");

            migrationBuilder.RenameTable(
                name: "DrinkCategoryPhotos",
                newName: "CategoryPhoto");

            migrationBuilder.RenameIndex(
                name: "IX_DrinkCategoryPhotos_DrinkCategoryId",
                table: "CategoryPhoto",
                newName: "IX_CategoryPhoto_DrinkCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryPhoto",
                table: "CategoryPhoto",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryPhoto_DrinkCategories_DrinkCategoryId",
                table: "CategoryPhoto",
                column: "DrinkCategoryId",
                principalTable: "DrinkCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
