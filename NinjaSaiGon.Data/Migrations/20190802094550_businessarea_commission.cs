using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NinjaSaiGon.Data.Migrations
{
    public partial class businessarea_commission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessAreas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    Coefficient = table.Column<double>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessAreas_BusinessAreas_ParentId",
                        column: x => x.ParentId,
                        principalTable: "BusinessAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommissionFormulas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    NewCusCommissionType = table.Column<int>(nullable: false),
                    NewCusCommissionValue = table.Column<double>(nullable: false),
                    OldCusCommissionType = table.Column<int>(nullable: false),
                    OldCusCommissionValue = table.Column<double>(nullable: false),
                    AreaCommissionType = table.Column<int>(nullable: false),
                    AreaCommissionValue = table.Column<double>(nullable: false),
                    AllCommissionType = table.Column<int>(nullable: false),
                    AllCommissionValue = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommissionFormulas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessAreas_ParentId",
                table: "BusinessAreas",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessAreas");

            migrationBuilder.DropTable(
                name: "CommissionFormulas");
        }
    }
}
