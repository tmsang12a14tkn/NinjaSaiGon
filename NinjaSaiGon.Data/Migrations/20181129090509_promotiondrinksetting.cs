using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NinjaSaiGon.Data.Migrations
{
    public partial class promotiondrinksetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PromotionDrinkSettings",
                columns: table => new
                {
                    PromotionId = table.Column<int>(nullable: false),
                    PromotionGiftType = table.Column<int>(nullable: false),
                    Condition_MinMoney = table.Column<bool>(nullable: false),
                    Condition_MinMoneyValue = table.Column<int>(nullable: false),
                    Condition_MinDrink = table.Column<bool>(nullable: false),
                    Condition_MinDrinkValue = table.Column<int>(nullable: false),
                    Condition_Topping = table.Column<bool>(nullable: false),
                    Condition_WithTopping = table.Column<bool>(nullable: false),
                    PromotionDrinkQuantity = table.Column<int>(nullable: false),
                    ApplyOneTimeOrRepeat = table.Column<bool>(nullable: false),
                    PromotionForm = table.Column<bool>(nullable: false),
                    PromotionDrinkRandom = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionDrinkSettings", x => x.PromotionId);
                    table.ForeignKey(
                        name: "FK_PromotionDrinkSettings_Promotions_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionDrinks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DrinkId = table.Column<int>(nullable: false),
                    DrinkSizeId = table.Column<int>(nullable: false),
                    DrinkSizeName = table.Column<string>(nullable: true),
                    PromotionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionDrinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromotionDrinks_Drinks_DrinkId",
                        column: x => x.DrinkId,
                        principalTable: "Drinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromotionDrinks_PromotionDrinkSettings_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "PromotionDrinkSettings",
                        principalColumn: "PromotionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionDrinkToppings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ToppingId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    PromotionDrinkId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionDrinkToppings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromotionDrinkToppings_PromotionDrinks_PromotionDrinkId",
                        column: x => x.PromotionDrinkId,
                        principalTable: "PromotionDrinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromotionDrinkToppings_Toppings_ToppingId",
                        column: x => x.ToppingId,
                        principalTable: "Toppings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PromotionDrinks_DrinkId",
                table: "PromotionDrinks",
                column: "DrinkId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionDrinks_PromotionId",
                table: "PromotionDrinks",
                column: "PromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionDrinkToppings_PromotionDrinkId",
                table: "PromotionDrinkToppings",
                column: "PromotionDrinkId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionDrinkToppings_ToppingId",
                table: "PromotionDrinkToppings",
                column: "ToppingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PromotionDrinkToppings");

            migrationBuilder.DropTable(
                name: "PromotionDrinks");

            migrationBuilder.DropTable(
                name: "PromotionDrinkSettings");
        }
    }
}
