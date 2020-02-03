using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NinjaSaiGon.Data.Migrations
{
    public partial class updatecategory1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "ToppingCategories");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "DrinkCategories");

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "ToppingCategories",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "DrinkCategories",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "DrinkCategories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DrinkCategoryTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrinkCategoryTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToppingCategories_ParentId",
                table: "ToppingCategories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_DrinkCategories_ParentId",
                table: "DrinkCategories",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkCategories_DrinkCategories_ParentId",
                table: "DrinkCategories",
                column: "ParentId",
                principalTable: "DrinkCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ToppingCategories_ToppingCategories_ParentId",
                table: "ToppingCategories",
                column: "ParentId",
                principalTable: "ToppingCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrinkCategories_DrinkCategories_ParentId",
                table: "DrinkCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ToppingCategories_ToppingCategories_ParentId",
                table: "ToppingCategories");

            migrationBuilder.DropTable(
                name: "DrinkCategoryTypes");

            migrationBuilder.DropIndex(
                name: "IX_ToppingCategories_ParentId",
                table: "ToppingCategories");

            migrationBuilder.DropIndex(
                name: "IX_DrinkCategories_ParentId",
                table: "DrinkCategories");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "ToppingCategories");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "DrinkCategories");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "DrinkCategories");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "ToppingCategories",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "DrinkCategories",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
