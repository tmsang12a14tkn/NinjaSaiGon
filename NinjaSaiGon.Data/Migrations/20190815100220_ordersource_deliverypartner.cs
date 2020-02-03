using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NinjaSaiGon.Data.Migrations
{
    public partial class ordersource_deliverypartner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderSource",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "OrderSourceId",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryPartnerId",
                table: "OrderDeliveries",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "InterestRate",
                table: "AgencyPayments",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<double>(
                name: "DiscountAmount",
                table: "AgencyPayments",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "DeliveryPartner",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryPartner", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderSourceType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSourceType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderSource",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    OrderSourceTypeId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderSource_OrderSourceType_OrderSourceTypeId",
                        column: x => x.OrderSourceTypeId,
                        principalTable: "OrderSourceType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderSourceId",
                table: "Orders",
                column: "OrderSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDeliveries_DeliveryPartnerId",
                table: "OrderDeliveries",
                column: "DeliveryPartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSource_OrderSourceTypeId",
                table: "OrderSource",
                column: "OrderSourceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDeliveries_DeliveryPartner_DeliveryPartnerId",
                table: "OrderDeliveries",
                column: "DeliveryPartnerId",
                principalTable: "DeliveryPartner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderSource_OrderSourceId",
                table: "Orders",
                column: "OrderSourceId",
                principalTable: "OrderSource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDeliveries_DeliveryPartner_DeliveryPartnerId",
                table: "OrderDeliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderSource_OrderSourceId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "DeliveryPartner");

            migrationBuilder.DropTable(
                name: "OrderSource");

            migrationBuilder.DropTable(
                name: "OrderSourceType");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderSourceId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_OrderDeliveries_DeliveryPartnerId",
                table: "OrderDeliveries");

            migrationBuilder.DropColumn(
                name: "OrderSourceId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryPartnerId",
                table: "OrderDeliveries");

            migrationBuilder.AddColumn<string>(
                name: "OrderSource",
                table: "Orders",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InterestRate",
                table: "AgencyPayments",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "DiscountAmount",
                table: "AgencyPayments",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
