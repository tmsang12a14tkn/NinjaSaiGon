using Microsoft.EntityFrameworkCore.Migrations;

namespace NinjaSaiGon.Data.Migrations
{
    public partial class editordertotal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OrderTotal",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "OrderTotal",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
