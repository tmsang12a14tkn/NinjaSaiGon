using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NinjaSaiGon.Data.Migrations
{
    public partial class private_promotion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PrivatePromotions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ApplyWithOther = table.Column<bool>(nullable: false),
                    ApplyTimeType = table.Column<int>(nullable: false),
                    ApplyFor = table.Column<int>(nullable: false),
                    ApplyRule = table.Column<int>(nullable: false),
                    DiscountType = table.Column<int>(nullable: false),
                    DiscountValue = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivatePromotions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrivatePromotionCodes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    MaxUseCode = table.Column<int>(nullable: false),
                    CurrentUseCode = table.Column<int>(nullable: false),
                    IsInfinityUse = table.Column<bool>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    IsInfinityTime = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CodeComment = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PrivatePromotionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivatePromotionCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrivatePromotionCodes_PrivatePromotions_PrivatePromotionId",
                        column: x => x.PrivatePromotionId,
                        principalTable: "PrivatePromotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrivatePromotionDrinkSettings",
                columns: table => new
                {
                    PrivatePromotionId = table.Column<int>(nullable: false),
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
                    PromotionDrinkRandom = table.Column<bool>(nullable: false),
                    PromotionDrinkBuyQuantity = table.Column<int>(nullable: false),
                    PromotionDrinkGiveQuantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivatePromotionDrinkSettings", x => x.PrivatePromotionId);
                    table.ForeignKey(
                        name: "FK_PrivatePromotionDrinkSettings_PrivatePromotions_PrivatePromotionId",
                        column: x => x.PrivatePromotionId,
                        principalTable: "PrivatePromotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrivatePromotionPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Url = table.Column<string>(nullable: true),
                    PhysicalPath = table.Column<string>(nullable: true),
                    IsPrimary = table.Column<bool>(nullable: false),
                    PrivatePromotionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivatePromotionPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrivatePromotionPhotos_PrivatePromotions_PrivatePromotionId",
                        column: x => x.PrivatePromotionId,
                        principalTable: "PrivatePromotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrivatePromotionDrinks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DrinkId = table.Column<int>(nullable: false),
                    DrinkSizeId = table.Column<int>(nullable: false),
                    DrinkSizeName = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    PrivatePromotionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivatePromotionDrinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrivatePromotionDrinks_Drinks_DrinkId",
                        column: x => x.DrinkId,
                        principalTable: "Drinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrivatePromotionDrinks_PrivatePromotionDrinkSettings_PrivatePromotionId",
                        column: x => x.PrivatePromotionId,
                        principalTable: "PrivatePromotionDrinkSettings",
                        principalColumn: "PrivatePromotionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrivatePromotionDrinkToppings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ToppingId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    PrivatePromotionDrinkId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivatePromotionDrinkToppings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrivatePromotionDrinkToppings_PrivatePromotionDrinks_PrivatePromotionDrinkId",
                        column: x => x.PrivatePromotionDrinkId,
                        principalTable: "PrivatePromotionDrinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrivatePromotionDrinkToppings_Toppings_ToppingId",
                        column: x => x.ToppingId,
                        principalTable: "Toppings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrivatePromotionCodes_PrivatePromotionId",
                table: "PrivatePromotionCodes",
                column: "PrivatePromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_PrivatePromotionDrinks_DrinkId",
                table: "PrivatePromotionDrinks",
                column: "DrinkId");

            migrationBuilder.CreateIndex(
                name: "IX_PrivatePromotionDrinks_PrivatePromotionId",
                table: "PrivatePromotionDrinks",
                column: "PrivatePromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_PrivatePromotionDrinkToppings_PrivatePromotionDrinkId",
                table: "PrivatePromotionDrinkToppings",
                column: "PrivatePromotionDrinkId");

            migrationBuilder.CreateIndex(
                name: "IX_PrivatePromotionDrinkToppings_ToppingId",
                table: "PrivatePromotionDrinkToppings",
                column: "ToppingId");

            migrationBuilder.CreateIndex(
                name: "IX_PrivatePromotionPhotos_PrivatePromotionId",
                table: "PrivatePromotionPhotos",
                column: "PrivatePromotionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrivatePromotionCodes");

            migrationBuilder.DropTable(
                name: "PrivatePromotionDrinkToppings");

            migrationBuilder.DropTable(
                name: "PrivatePromotionPhotos");

            migrationBuilder.DropTable(
                name: "PrivatePromotionDrinks");

            migrationBuilder.DropTable(
                name: "PrivatePromotionDrinkSettings");

            migrationBuilder.DropTable(
                name: "PrivatePromotions");
        }
    }
}
