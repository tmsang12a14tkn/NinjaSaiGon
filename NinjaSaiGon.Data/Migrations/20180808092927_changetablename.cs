using Microsoft.EntityFrameworkCore.Migrations;

namespace NinjaSaiGon.Data.Migrations
{
    public partial class changetablename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrinkSize_Drinks_DrinkId",
                table: "DrinkSize");

            migrationBuilder.DropForeignKey(
                name: "FK_DrinkSize_DrinkUnit_UnitId",
                table: "DrinkSize");

            migrationBuilder.DropForeignKey(
                name: "FK_IceOption_Drinks_DrinkId",
                table: "IceOption");

            migrationBuilder.DropForeignKey(
                name: "FK_IceOption_DrinkUnit_UnitId",
                table: "IceOption");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDelivery_Orders_OrderId",
                table: "OrderDelivery");

            migrationBuilder.DropForeignKey(
                name: "FK_SugarOption_Drinks_DrinkId",
                table: "SugarOption");

            migrationBuilder.DropForeignKey(
                name: "FK_SugarOption_DrinkUnit_UnitId",
                table: "SugarOption");

            migrationBuilder.DropForeignKey(
                name: "FK_ToppingPhoto_Toppings_ToppingId",
                table: "ToppingPhoto");

            migrationBuilder.DropForeignKey(
                name: "FK_Toppings_DrinkUnit_UnitId",
                table: "Toppings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ToppingPhoto",
                table: "ToppingPhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SugarOption",
                table: "SugarOption");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDelivery",
                table: "OrderDelivery");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IceOption",
                table: "IceOption");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DrinkUnit",
                table: "DrinkUnit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DrinkSize",
                table: "DrinkSize");

            migrationBuilder.RenameTable(
                name: "ToppingPhoto",
                newName: "ToppingPhotos");

            migrationBuilder.RenameTable(
                name: "SugarOption",
                newName: "SugarOptions");

            migrationBuilder.RenameTable(
                name: "OrderDelivery",
                newName: "OrderDeliveries");

            migrationBuilder.RenameTable(
                name: "IceOption",
                newName: "IceOptions");

            migrationBuilder.RenameTable(
                name: "DrinkUnit",
                newName: "DrinkUnits");

            migrationBuilder.RenameTable(
                name: "DrinkSize",
                newName: "DrinkSizes");

            migrationBuilder.RenameIndex(
                name: "IX_ToppingPhoto_ToppingId",
                table: "ToppingPhotos",
                newName: "IX_ToppingPhotos_ToppingId");

            migrationBuilder.RenameIndex(
                name: "IX_SugarOption_UnitId",
                table: "SugarOptions",
                newName: "IX_SugarOptions_UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_SugarOption_DrinkId",
                table: "SugarOptions",
                newName: "IX_SugarOptions_DrinkId");

            migrationBuilder.RenameIndex(
                name: "IX_IceOption_UnitId",
                table: "IceOptions",
                newName: "IX_IceOptions_UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_IceOption_DrinkId",
                table: "IceOptions",
                newName: "IX_IceOptions_DrinkId");

            migrationBuilder.RenameIndex(
                name: "IX_DrinkSize_UnitId",
                table: "DrinkSizes",
                newName: "IX_DrinkSizes_UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_DrinkSize_DrinkId",
                table: "DrinkSizes",
                newName: "IX_DrinkSizes_DrinkId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ToppingPhotos",
                table: "ToppingPhotos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SugarOptions",
                table: "SugarOptions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDeliveries",
                table: "OrderDeliveries",
                column: "OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IceOptions",
                table: "IceOptions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DrinkUnits",
                table: "DrinkUnits",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DrinkSizes",
                table: "DrinkSizes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkSizes_Drinks_DrinkId",
                table: "DrinkSizes",
                column: "DrinkId",
                principalTable: "Drinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkSizes_DrinkUnits_UnitId",
                table: "DrinkSizes",
                column: "UnitId",
                principalTable: "DrinkUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IceOptions_Drinks_DrinkId",
                table: "IceOptions",
                column: "DrinkId",
                principalTable: "Drinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IceOptions_DrinkUnits_UnitId",
                table: "IceOptions",
                column: "UnitId",
                principalTable: "DrinkUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDeliveries_Orders_OrderId",
                table: "OrderDeliveries",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SugarOptions_Drinks_DrinkId",
                table: "SugarOptions",
                column: "DrinkId",
                principalTable: "Drinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SugarOptions_DrinkUnits_UnitId",
                table: "SugarOptions",
                column: "UnitId",
                principalTable: "DrinkUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ToppingPhotos_Toppings_ToppingId",
                table: "ToppingPhotos",
                column: "ToppingId",
                principalTable: "Toppings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Toppings_DrinkUnits_UnitId",
                table: "Toppings",
                column: "UnitId",
                principalTable: "DrinkUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrinkSizes_Drinks_DrinkId",
                table: "DrinkSizes");

            migrationBuilder.DropForeignKey(
                name: "FK_DrinkSizes_DrinkUnits_UnitId",
                table: "DrinkSizes");

            migrationBuilder.DropForeignKey(
                name: "FK_IceOptions_Drinks_DrinkId",
                table: "IceOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_IceOptions_DrinkUnits_UnitId",
                table: "IceOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDeliveries_Orders_OrderId",
                table: "OrderDeliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_SugarOptions_Drinks_DrinkId",
                table: "SugarOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_SugarOptions_DrinkUnits_UnitId",
                table: "SugarOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_ToppingPhotos_Toppings_ToppingId",
                table: "ToppingPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_Toppings_DrinkUnits_UnitId",
                table: "Toppings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ToppingPhotos",
                table: "ToppingPhotos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SugarOptions",
                table: "SugarOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDeliveries",
                table: "OrderDeliveries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IceOptions",
                table: "IceOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DrinkUnits",
                table: "DrinkUnits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DrinkSizes",
                table: "DrinkSizes");

            migrationBuilder.RenameTable(
                name: "ToppingPhotos",
                newName: "ToppingPhoto");

            migrationBuilder.RenameTable(
                name: "SugarOptions",
                newName: "SugarOption");

            migrationBuilder.RenameTable(
                name: "OrderDeliveries",
                newName: "OrderDelivery");

            migrationBuilder.RenameTable(
                name: "IceOptions",
                newName: "IceOption");

            migrationBuilder.RenameTable(
                name: "DrinkUnits",
                newName: "DrinkUnit");

            migrationBuilder.RenameTable(
                name: "DrinkSizes",
                newName: "DrinkSize");

            migrationBuilder.RenameIndex(
                name: "IX_ToppingPhotos_ToppingId",
                table: "ToppingPhoto",
                newName: "IX_ToppingPhoto_ToppingId");

            migrationBuilder.RenameIndex(
                name: "IX_SugarOptions_UnitId",
                table: "SugarOption",
                newName: "IX_SugarOption_UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_SugarOptions_DrinkId",
                table: "SugarOption",
                newName: "IX_SugarOption_DrinkId");

            migrationBuilder.RenameIndex(
                name: "IX_IceOptions_UnitId",
                table: "IceOption",
                newName: "IX_IceOption_UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_IceOptions_DrinkId",
                table: "IceOption",
                newName: "IX_IceOption_DrinkId");

            migrationBuilder.RenameIndex(
                name: "IX_DrinkSizes_UnitId",
                table: "DrinkSize",
                newName: "IX_DrinkSize_UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_DrinkSizes_DrinkId",
                table: "DrinkSize",
                newName: "IX_DrinkSize_DrinkId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ToppingPhoto",
                table: "ToppingPhoto",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SugarOption",
                table: "SugarOption",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDelivery",
                table: "OrderDelivery",
                column: "OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IceOption",
                table: "IceOption",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DrinkUnit",
                table: "DrinkUnit",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DrinkSize",
                table: "DrinkSize",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkSize_Drinks_DrinkId",
                table: "DrinkSize",
                column: "DrinkId",
                principalTable: "Drinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkSize_DrinkUnit_UnitId",
                table: "DrinkSize",
                column: "UnitId",
                principalTable: "DrinkUnit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IceOption_Drinks_DrinkId",
                table: "IceOption",
                column: "DrinkId",
                principalTable: "Drinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IceOption_DrinkUnit_UnitId",
                table: "IceOption",
                column: "UnitId",
                principalTable: "DrinkUnit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDelivery_Orders_OrderId",
                table: "OrderDelivery",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SugarOption_Drinks_DrinkId",
                table: "SugarOption",
                column: "DrinkId",
                principalTable: "Drinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SugarOption_DrinkUnit_UnitId",
                table: "SugarOption",
                column: "UnitId",
                principalTable: "DrinkUnit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ToppingPhoto_Toppings_ToppingId",
                table: "ToppingPhoto",
                column: "ToppingId",
                principalTable: "Toppings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Toppings_DrinkUnit_UnitId",
                table: "Toppings",
                column: "UnitId",
                principalTable: "DrinkUnit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
