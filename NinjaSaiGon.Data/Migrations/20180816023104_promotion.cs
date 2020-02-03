using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NinjaSaiGon.Data.Migrations
{
    public partial class promotion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "OrderDetailToppings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "OrderDetailToppings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "OrderDetailToppings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "OrderDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Promotions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PromotionCode = table.Column<string>(nullable: true),
                    ApplyWithOther = table.Column<bool>(nullable: false),
                    ApplyTimeType = table.Column<int>(nullable: false),
                    ApplyFromDate = table.Column<DateTime>(nullable: false),
                    ApplyToDate = table.Column<DateTime>(nullable: false),
                    ApplyDayOfWeek = table.Column<int>(nullable: false),
                    ApplyFor = table.Column<int>(nullable: false),
                    ApplyRule = table.Column<int>(nullable: false),
                    DiscountType = table.Column<int>(nullable: false),
                    DiscountValue = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PromotionPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Url = table.Column<string>(nullable: true),
                    PhysicalPath = table.Column<string>(nullable: true),
                    IsPrimary = table.Column<bool>(nullable: false),
                    PromotionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromotionPhotos_Promotions_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PromotionPhotos_PromotionId",
                table: "PromotionPhotos",
                column: "PromotionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PromotionPhotos");

            migrationBuilder.DropTable(
                name: "Promotions");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "OrderDetailToppings");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrderDetailToppings");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OrderDetailToppings");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OrderDetails");
        }
    }
}
