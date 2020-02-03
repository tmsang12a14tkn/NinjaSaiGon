using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NinjaSaiGon.Data.Migrations
{
    public partial class edittopping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsShared",
                table: "Toppings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OtherName",
                table: "Toppings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quota",
                table: "Toppings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "Toppings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DisplayIceOption",
                table: "Drinks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DisplaySizeOption",
                table: "Drinks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DisplaySugarOption",
                table: "Drinks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequireIceOption",
                table: "Drinks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequireSizeOption",
                table: "Drinks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequireSugarOption",
                table: "Drinks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ToppingPhoto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Url = table.Column<string>(nullable: true),
                    PhysicalPath = table.Column<string>(nullable: true),
                    IsPrimary = table.Column<bool>(nullable: false),
                    ToppingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToppingPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToppingPhoto_Toppings_ToppingId",
                        column: x => x.ToppingId,
                        principalTable: "Toppings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Toppings_UnitId",
                table: "Toppings",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ToppingPhoto_ToppingId",
                table: "ToppingPhoto",
                column: "ToppingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Toppings_DrinkUnit_UnitId",
                table: "Toppings",
                column: "UnitId",
                principalTable: "DrinkUnit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Toppings_DrinkUnit_UnitId",
                table: "Toppings");

            migrationBuilder.DropTable(
                name: "ToppingPhoto");

            migrationBuilder.DropIndex(
                name: "IX_Toppings_UnitId",
                table: "Toppings");

            migrationBuilder.DropColumn(
                name: "IsShared",
                table: "Toppings");

            migrationBuilder.DropColumn(
                name: "OtherName",
                table: "Toppings");

            migrationBuilder.DropColumn(
                name: "Quota",
                table: "Toppings");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "Toppings");

            migrationBuilder.DropColumn(
                name: "DisplayIceOption",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "DisplaySizeOption",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "DisplaySugarOption",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "RequireIceOption",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "RequireSizeOption",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "RequireSugarOption",
                table: "Drinks");
        }
    }
}
