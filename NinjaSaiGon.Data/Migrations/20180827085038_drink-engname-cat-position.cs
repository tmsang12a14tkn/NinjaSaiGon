using Microsoft.EntityFrameworkCore.Migrations;

namespace NinjaSaiGon.Data.Migrations
{
    public partial class drinkengnamecatposition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "ToppingCategories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EnglishName",
                table: "Drinks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "DrinkCategories",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "ToppingCategories");

            migrationBuilder.DropColumn(
                name: "EnglishName",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "DrinkCategories");
        }
    }
}
