using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NinjaSaiGon.Data.Migrations
{
    public partial class updateorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CardCode",
                table: "Orders",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerNote",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeName",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeNote",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDeliveried",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "OrderSource",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SocialNetwork",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StatusNote",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardCode",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CustomerNote",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "EmployeeName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "EmployeeNote",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderDeliveried",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderSource",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SocialNetwork",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "StatusNote",
                table: "Orders");
        }
    }
}
