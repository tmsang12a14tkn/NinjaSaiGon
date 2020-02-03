using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NinjaSaiGon.Data.Migrations
{
    public partial class alter_department : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonEmployeeWorkings_Branches_BranchId",
                table: "PersonEmployeeWorkings");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonEmployeeWorkings_BranchPositions_BranchPositionId",
                table: "PersonEmployeeWorkings");

            migrationBuilder.DropTable(
                name: "BranchPositions");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.RenameColumn(
                name: "BranchPositionId",
                table: "PersonEmployeeWorkings",
                newName: "DepartmentPositionId");

            migrationBuilder.RenameColumn(
                name: "BranchId",
                table: "PersonEmployeeWorkings",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonEmployeeWorkings_BranchPositionId",
                table: "PersonEmployeeWorkings",
                newName: "IX_PersonEmployeeWorkings_DepartmentPositionId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonEmployeeWorkings_BranchId",
                table: "PersonEmployeeWorkings",
                newName: "IX_PersonEmployeeWorkings_DepartmentId");

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentPositions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentPositions_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentPositions_DepartmentId",
                table: "DepartmentPositions",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_CompanyId",
                table: "Departments",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonEmployeeWorkings_Departments_DepartmentId",
                table: "PersonEmployeeWorkings",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonEmployeeWorkings_DepartmentPositions_DepartmentPositionId",
                table: "PersonEmployeeWorkings",
                column: "DepartmentPositionId",
                principalTable: "DepartmentPositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PersonId",
                table: "AspNetUsers",
                column: "PersonId",
                unique: true,
                filter: "[PersonId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Persons_PersonId",
                table: "AspNetUsers",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonEmployeeWorkings_Departments_DepartmentId",
                table: "PersonEmployeeWorkings");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonEmployeeWorkings_DepartmentPositions_DepartmentPositionId",
                table: "PersonEmployeeWorkings");

            migrationBuilder.DropTable(
                name: "DepartmentPositions");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.RenameColumn(
                name: "DepartmentPositionId",
                table: "PersonEmployeeWorkings",
                newName: "BranchPositionId");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "PersonEmployeeWorkings",
                newName: "BranchId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonEmployeeWorkings_DepartmentPositionId",
                table: "PersonEmployeeWorkings",
                newName: "IX_PersonEmployeeWorkings_BranchPositionId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonEmployeeWorkings_DepartmentId",
                table: "PersonEmployeeWorkings",
                newName: "IX_PersonEmployeeWorkings_BranchId");

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    CompanyId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Branches_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BranchPositions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BranchId = table.Column<int>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BranchPositions_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Branches_CompanyId",
                table: "Branches",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchPositions_BranchId",
                table: "BranchPositions",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonEmployeeWorkings_Branches_BranchId",
                table: "PersonEmployeeWorkings",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonEmployeeWorkings_BranchPositions_BranchPositionId",
                table: "PersonEmployeeWorkings",
                column: "BranchPositionId",
                principalTable: "BranchPositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Persons_PersonId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PersonId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "AspNetUsers");
        }
    }
}
