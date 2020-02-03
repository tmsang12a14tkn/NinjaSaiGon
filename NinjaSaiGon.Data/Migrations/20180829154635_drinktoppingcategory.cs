using Microsoft.EntityFrameworkCore.Migrations;

namespace NinjaSaiGon.Data.Migrations
{
    public partial class drinktoppingcategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DrinkToppingCategory",
                columns: table => new
                {
                    DrinkId = table.Column<int>(nullable: false),
                    ToppingCategoryId = table.Column<int>(nullable: false),
                    Min = table.Column<int>(nullable: true),
                    Max = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrinkToppingCategory", x => new { x.DrinkId, x.ToppingCategoryId });
                    table.ForeignKey(
                        name: "FK_DrinkToppingCategory_Drinks_DrinkId",
                        column: x => x.DrinkId,
                        principalTable: "Drinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrinkToppingCategory_ToppingCategories_ToppingCategoryId",
                        column: x => x.ToppingCategoryId,
                        principalTable: "ToppingCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrinkToppingCategory_ToppingCategoryId",
                table: "DrinkToppingCategory",
                column: "ToppingCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrinkToppingCategory");
        }
    }
}
