using Microsoft.EntityFrameworkCore.Migrations;

namespace NinjaSaiGon.Data.Migrations
{
    public partial class alterpromo1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ApplyDayOfWeek",
                table: "Promotions",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ApplyDayOfWeek",
                table: "Promotions",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
