using Microsoft.EntityFrameworkCore.Migrations;

namespace NinjaSaiGon.Data.Migrations
{
    public partial class changeorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "OrderDeliveries");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "OrderDeliveries");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "OrderDeliveries");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Orders",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "OrderDeliveries",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "OrderDeliveries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "OrderDeliveries",
                maxLength: 25,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "OrderDeliveries");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "OrderDeliveries");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "OrderDeliveries");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "OrderDeliveries",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "OrderDeliveries",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "OrderDeliveries",
                maxLength: 50,
                nullable: true);
        }
    }
}
