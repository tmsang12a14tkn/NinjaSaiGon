using Microsoft.EntityFrameworkCore.Migrations;

namespace NinjaSaiGon.Data.Migrations
{
    public partial class alter_delivery_distance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PartnerDistance",
                table: "OrderDeliveries",
                nullable: false,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "PartnerDistance",
                table: "OrderDeliveries",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
