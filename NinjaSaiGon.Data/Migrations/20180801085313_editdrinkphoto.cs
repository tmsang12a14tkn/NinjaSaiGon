using Microsoft.EntityFrameworkCore.Migrations;

namespace NinjaSaiGon.Data.Migrations
{
    public partial class editdrinkphoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrinkPhoto_Drinks_DrinkId",
                table: "DrinkPhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DrinkPhoto",
                table: "DrinkPhoto");

            migrationBuilder.RenameTable(
                name: "DrinkPhoto",
                newName: "DrinkPhotos");

            migrationBuilder.RenameIndex(
                name: "IX_DrinkPhoto_DrinkId",
                table: "DrinkPhotos",
                newName: "IX_DrinkPhotos_DrinkId");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "DrinkPhotos",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DrinkPhotos",
                table: "DrinkPhotos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "OrderDetailTopping",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(nullable: false),
                    ToppingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetailTopping", x => new { x.OrderDetailId, x.ToppingId });
                    table.ForeignKey(
                        name: "FK_OrderDetailTopping_OrderDetails_OrderDetailId",
                        column: x => x.OrderDetailId,
                        principalTable: "OrderDetails",
                        principalColumn: "OrderDetailId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetailTopping_Toppings_ToppingId",
                        column: x => x.ToppingId,
                        principalTable: "Toppings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailTopping_ToppingId",
                table: "OrderDetailTopping",
                column: "ToppingId");

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkPhotos_Drinks_DrinkId",
                table: "DrinkPhotos",
                column: "DrinkId",
                principalTable: "Drinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrinkPhotos_Drinks_DrinkId",
                table: "DrinkPhotos");

            migrationBuilder.DropTable(
                name: "OrderDetailTopping");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DrinkPhotos",
                table: "DrinkPhotos");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "DrinkPhotos");

            migrationBuilder.RenameTable(
                name: "DrinkPhotos",
                newName: "DrinkPhoto");

            migrationBuilder.RenameIndex(
                name: "IX_DrinkPhotos_DrinkId",
                table: "DrinkPhoto",
                newName: "IX_DrinkPhoto_DrinkId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DrinkPhoto",
                table: "DrinkPhoto",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkPhoto_Drinks_DrinkId",
                table: "DrinkPhoto",
                column: "DrinkId",
                principalTable: "Drinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
