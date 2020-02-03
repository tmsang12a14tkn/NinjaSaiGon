using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NinjaSaiGon.Data.Migrations
{
    public partial class updatedrink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsHidden",
                table: "SugarOption",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IsHidden",
                table: "IceOption",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IsHidden",
                table: "DrinkSize",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IsHidden",
                table: "CategorySugarOption",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IsHidden",
                table: "CategoryIceOption",
                newName: "IsActive");

            migrationBuilder.AddColumn<float>(
                name: "Quota",
                table: "SugarOption",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "SugarOption",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Quota",
                table: "IceOption",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "IceOption",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPrimary",
                table: "DrinkToppings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceForExtra",
                table: "DrinkToppings",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceForSale",
                table: "DrinkToppings",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<float>(
                name: "Quota",
                table: "DrinkSize",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "DrinkSize",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Barcode",
                table: "Drinks",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCombo",
                table: "Drinks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                table: "Drinks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOpen",
                table: "Drinks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsShared",
                table: "Drinks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSuggested",
                table: "Drinks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Reenter",
                table: "Drinks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "DrinkUnit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrinkUnit", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SugarOption_UnitId",
                table: "SugarOption",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_IceOption_UnitId",
                table: "IceOption",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_DrinkSize_UnitId",
                table: "DrinkSize",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkSize_DrinkUnit_UnitId",
                table: "DrinkSize",
                column: "UnitId",
                principalTable: "DrinkUnit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IceOption_DrinkUnit_UnitId",
                table: "IceOption",
                column: "UnitId",
                principalTable: "DrinkUnit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SugarOption_DrinkUnit_UnitId",
                table: "SugarOption",
                column: "UnitId",
                principalTable: "DrinkUnit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrinkSize_DrinkUnit_UnitId",
                table: "DrinkSize");

            migrationBuilder.DropForeignKey(
                name: "FK_IceOption_DrinkUnit_UnitId",
                table: "IceOption");

            migrationBuilder.DropForeignKey(
                name: "FK_SugarOption_DrinkUnit_UnitId",
                table: "SugarOption");

            migrationBuilder.DropTable(
                name: "DrinkUnit");

            migrationBuilder.DropIndex(
                name: "IX_SugarOption_UnitId",
                table: "SugarOption");

            migrationBuilder.DropIndex(
                name: "IX_IceOption_UnitId",
                table: "IceOption");

            migrationBuilder.DropIndex(
                name: "IX_DrinkSize_UnitId",
                table: "DrinkSize");

            migrationBuilder.DropColumn(
                name: "Quota",
                table: "SugarOption");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "SugarOption");

            migrationBuilder.DropColumn(
                name: "Quota",
                table: "IceOption");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "IceOption");

            migrationBuilder.DropColumn(
                name: "IsPrimary",
                table: "DrinkToppings");

            migrationBuilder.DropColumn(
                name: "PriceForExtra",
                table: "DrinkToppings");

            migrationBuilder.DropColumn(
                name: "PriceForSale",
                table: "DrinkToppings");

            migrationBuilder.DropColumn(
                name: "Quota",
                table: "DrinkSize");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "DrinkSize");

            migrationBuilder.DropColumn(
                name: "Barcode",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "IsCombo",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "IsFeatured",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "IsOpen",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "IsShared",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "IsSuggested",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "Reenter",
                table: "Drinks");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "SugarOption",
                newName: "IsHidden");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "IceOption",
                newName: "IsHidden");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "DrinkSize",
                newName: "IsHidden");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "CategorySugarOption",
                newName: "IsHidden");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "CategoryIceOption",
                newName: "IsHidden");
        }
    }
}
