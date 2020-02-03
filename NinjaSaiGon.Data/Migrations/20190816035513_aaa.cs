using Microsoft.EntityFrameworkCore.Migrations;

namespace NinjaSaiGon.Data.Migrations
{
    public partial class aaa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDeliveries_DeliveryPartner_DeliveryPartnerId",
                table: "OrderDeliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderSource_OrderSourceId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderSource_OrderSourceType_OrderSourceTypeId",
                table: "OrderSource");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderSourceType",
                table: "OrderSourceType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderSource",
                table: "OrderSource");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliveryPartner",
                table: "DeliveryPartner");

            migrationBuilder.RenameTable(
                name: "OrderSourceType",
                newName: "OrderSourceTypes");

            migrationBuilder.RenameTable(
                name: "OrderSource",
                newName: "OrderSources");

            migrationBuilder.RenameTable(
                name: "DeliveryPartner",
                newName: "DeliveryPartners");

            migrationBuilder.RenameIndex(
                name: "IX_OrderSource_OrderSourceTypeId",
                table: "OrderSources",
                newName: "IX_OrderSources_OrderSourceTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderSourceTypes",
                table: "OrderSourceTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderSources",
                table: "OrderSources",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliveryPartners",
                table: "DeliveryPartners",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDeliveries_DeliveryPartners_DeliveryPartnerId",
                table: "OrderDeliveries",
                column: "DeliveryPartnerId",
                principalTable: "DeliveryPartners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderSources_OrderSourceId",
                table: "Orders",
                column: "OrderSourceId",
                principalTable: "OrderSources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderSources_OrderSourceTypes_OrderSourceTypeId",
                table: "OrderSources",
                column: "OrderSourceTypeId",
                principalTable: "OrderSourceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDeliveries_DeliveryPartners_DeliveryPartnerId",
                table: "OrderDeliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderSources_OrderSourceId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderSources_OrderSourceTypes_OrderSourceTypeId",
                table: "OrderSources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderSourceTypes",
                table: "OrderSourceTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderSources",
                table: "OrderSources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliveryPartners",
                table: "DeliveryPartners");

            migrationBuilder.RenameTable(
                name: "OrderSourceTypes",
                newName: "OrderSourceType");

            migrationBuilder.RenameTable(
                name: "OrderSources",
                newName: "OrderSource");

            migrationBuilder.RenameTable(
                name: "DeliveryPartners",
                newName: "DeliveryPartner");

            migrationBuilder.RenameIndex(
                name: "IX_OrderSources_OrderSourceTypeId",
                table: "OrderSource",
                newName: "IX_OrderSource_OrderSourceTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderSourceType",
                table: "OrderSourceType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderSource",
                table: "OrderSource",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliveryPartner",
                table: "DeliveryPartner",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_OrderSource_OrderSourceType_OrderSourceTypeId",
                table: "OrderSource",
                column: "OrderSourceTypeId",
                principalTable: "OrderSourceType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
