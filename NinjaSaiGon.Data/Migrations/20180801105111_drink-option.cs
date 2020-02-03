using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NinjaSaiGon.Data.Migrations
{
    public partial class drinkoption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryIceOption",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    IsPrimary = table.Column<bool>(nullable: false),
                    IsHidden = table.Column<bool>(nullable: false),
                    DrinkCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryIceOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryIceOption_DrinkCategories_DrinkCategoryId",
                        column: x => x.DrinkCategoryId,
                        principalTable: "DrinkCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategorySugarOption",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    IsPrimary = table.Column<bool>(nullable: false),
                    IsHidden = table.Column<bool>(nullable: false),
                    DrinkCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategorySugarOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategorySugarOption_DrinkCategories_DrinkCategoryId",
                        column: x => x.DrinkCategoryId,
                        principalTable: "DrinkCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DrinkSize",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    IsPrimary = table.Column<bool>(nullable: false),
                    IsHidden = table.Column<bool>(nullable: false),
                    Prize = table.Column<int>(nullable: false),
                    DrinkId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrinkSize", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrinkSize_Drinks_DrinkId",
                        column: x => x.DrinkId,
                        principalTable: "Drinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IceOption",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    IsPrimary = table.Column<bool>(nullable: false),
                    IsHidden = table.Column<bool>(nullable: false),
                    DrinkId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IceOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IceOption_Drinks_DrinkId",
                        column: x => x.DrinkId,
                        principalTable: "Drinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetailToppings",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(nullable: false),
                    ToppingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetailToppings", x => new { x.OrderDetailId, x.ToppingId });
                    table.ForeignKey(
                        name: "FK_OrderDetailToppings_OrderDetails_OrderDetailId",
                        column: x => x.OrderDetailId,
                        principalTable: "OrderDetails",
                        principalColumn: "OrderDetailId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetailToppings_Toppings_ToppingId",
                        column: x => x.ToppingId,
                        principalTable: "Toppings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SugarOption",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    IsPrimary = table.Column<bool>(nullable: false),
                    IsHidden = table.Column<bool>(nullable: false),
                    DrinkId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SugarOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SugarOption_Drinks_DrinkId",
                        column: x => x.DrinkId,
                        principalTable: "Drinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryIceOption_DrinkCategoryId",
                table: "CategoryIceOption",
                column: "DrinkCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategorySugarOption_DrinkCategoryId",
                table: "CategorySugarOption",
                column: "DrinkCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DrinkSize_DrinkId",
                table: "DrinkSize",
                column: "DrinkId");

            migrationBuilder.CreateIndex(
                name: "IX_IceOption_DrinkId",
                table: "IceOption",
                column: "DrinkId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailToppings_ToppingId",
                table: "OrderDetailToppings",
                column: "ToppingId");

            migrationBuilder.CreateIndex(
                name: "IX_SugarOption_DrinkId",
                table: "SugarOption",
                column: "DrinkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryIceOption");

            migrationBuilder.DropTable(
                name: "CategorySugarOption");

            migrationBuilder.DropTable(
                name: "DrinkSize");

            migrationBuilder.DropTable(
                name: "IceOption");

            migrationBuilder.DropTable(
                name: "OrderDetailToppings");

            migrationBuilder.DropTable(
                name: "SugarOption");
        }
    }
}
