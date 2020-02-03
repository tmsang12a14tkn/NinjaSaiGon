using Microsoft.EntityFrameworkCore.Migrations;

namespace NinjaSaiGon.Data.Migrations
{
    public partial class updateordermodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Drinks_DrinkId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_DrinkId",
                table: "OrderDetails");

            migrationBuilder.AddColumn<string>(
                name: "ToppingName",
                table: "OrderDetailToppings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DrinkName",
                table: "OrderDetails",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DrinkSizeId",
                table: "OrderDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FullPrice",
                table: "OrderDetails",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToppingName",
                table: "OrderDetailToppings");

            migrationBuilder.DropColumn(
                name: "DrinkName",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "DrinkSizeId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "FullPrice",
                table: "OrderDetails");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_DrinkId",
                table: "OrderDetails",
                column: "DrinkId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Drinks_DrinkId",
                table: "OrderDetails",
                column: "DrinkId",
                principalTable: "Drinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
