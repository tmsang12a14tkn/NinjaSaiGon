using Microsoft.EntityFrameworkCore.Migrations;

namespace NinjaSaiGon.Data.Migrations
{
    public partial class update_ship_partner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartnerName",
                table: "OrderDeliveries");

            migrationBuilder.AddColumn<int>(
                name: "Partner",
                table: "OrderDeliveries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PartnerDistance",
                table: "OrderDeliveries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PartnerShipFee",
                table: "OrderDeliveries",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Partner",
                table: "OrderDeliveries");

            migrationBuilder.DropColumn(
                name: "PartnerDistance",
                table: "OrderDeliveries");

            migrationBuilder.DropColumn(
                name: "PartnerShipFee",
                table: "OrderDeliveries");

            migrationBuilder.AddColumn<string>(
                name: "PartnerName",
                table: "OrderDeliveries",
                nullable: true);
        }
    }
}
