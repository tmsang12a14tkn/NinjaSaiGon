using Microsoft.EntityFrameworkCore.Migrations;

namespace NinjaSaiGon.Data.Migrations
{
    public partial class update_drink_ice_sugar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsShowOrder",
                table: "SugarOptions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "SugarOptions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsShowOrder",
                table: "DrinkSizes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "DrinkSizes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsShowOrder",
                table: "SugarOptions");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "SugarOptions");

            migrationBuilder.DropColumn(
                name: "IsShowOrder",
                table: "DrinkSizes");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "DrinkSizes");
        }
    }
}
