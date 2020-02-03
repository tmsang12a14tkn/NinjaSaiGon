using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NinjaSaiGon.Data.Migrations
{
    public partial class outofstock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "OutOfStock",
                table: "Toppings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "OutOfStockFrom",
                table: "Toppings",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OutOfStockTo",
                table: "Toppings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OutOfStock",
                table: "Drinks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "OutOfStockFrom",
                table: "Drinks",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OutOfStockTo",
                table: "Drinks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OutOfStock",
                table: "Toppings");

            migrationBuilder.DropColumn(
                name: "OutOfStockFrom",
                table: "Toppings");

            migrationBuilder.DropColumn(
                name: "OutOfStockTo",
                table: "Toppings");

            migrationBuilder.DropColumn(
                name: "OutOfStock",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "OutOfStockFrom",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "OutOfStockTo",
                table: "Drinks");
        }
    }
}
