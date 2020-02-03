using Microsoft.EntityFrameworkCore.Migrations;

namespace NinjaSaiGon.Data.Migrations
{
    public partial class altorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAutoDiscount",
                table: "Orders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAutoShipFee",
                table: "Orders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAutoSurcharge",
                table: "Orders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SurchargeAmount",
                table: "Orders",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAutoDiscount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsAutoShipFee",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsAutoSurcharge",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SurchargeAmount",
                table: "Orders");
        }
    }
}
