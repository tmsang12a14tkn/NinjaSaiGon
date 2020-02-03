using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NinjaSaiGon.Data.Migrations
{
    public partial class DrinkDiscount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiscountMoneyValue",
                table: "OrderDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiscountPercentValue",
                table: "OrderDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DiscountReason",
                table: "OrderDetails",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DiscountType",
                table: "OrderDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FreeDrinkReasonId",
                table: "OrderDetails",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FreeDrinkReasons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreeDrinkReasons", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_FreeDrinkReasonId",
                table: "OrderDetails",
                column: "FreeDrinkReasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_FreeDrinkReasons_FreeDrinkReasonId",
                table: "OrderDetails",
                column: "FreeDrinkReasonId",
                principalTable: "FreeDrinkReasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_FreeDrinkReasons_FreeDrinkReasonId",
                table: "OrderDetails");

            migrationBuilder.DropTable(
                name: "FreeDrinkReasons");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_FreeDrinkReasonId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "DiscountMoneyValue",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "DiscountPercentValue",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "DiscountReason",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "DiscountType",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "FreeDrinkReasonId",
                table: "OrderDetails");
        }
    }
}
