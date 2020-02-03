using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace NinjaSaiGon.Data.Migrations
{
    public partial class roleauthorize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ControllerActions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ControllerName = table.Column<string>(nullable: true),
                    ActionName = table.Column<string>(nullable: true),
                    Params = table.Column<string>(nullable: true),
                    MenuGroup = table.Column<string>(nullable: true),
                    Site = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsShow = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControllerActions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ControllerActionPermissions",
                columns: table => new
                {
                    ControllerActionId = table.Column<int>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControllerActionPermissions", x => new { x.ControllerActionId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_ControllerActionPermissions_ControllerActions_ControllerActionId",
                        column: x => x.ControllerActionId,
                        principalTable: "ControllerActions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ControllerActionPermissions_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ControllerActionPermissions_RoleId",
                table: "ControllerActionPermissions",
                column: "RoleId");

            migrationBuilder.AlterColumn<string>(
                name: "ExpiredDate",
                table: "BankCards",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExpiredDate",
                table: "AgencyBankCards",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ControllerActionPermissions");

            migrationBuilder.DropTable(
                name: "ControllerActions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiredDate",
                table: "BankCards",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiredDate",
                table: "AgencyBankCards",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
