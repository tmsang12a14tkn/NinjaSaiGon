using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NinjaSaiGon.Data.Migrations
{
    public partial class employee_account_working : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BranchPositions_BranchPositionLevels_LevelId",
                table: "BranchPositions");

            migrationBuilder.DropTable(
                name: "BranchPositionLevels");

            migrationBuilder.DropTable(
                name: "PersonDepartments");

            migrationBuilder.DropTable(
                name: "DepartmentPositions");

            migrationBuilder.DropTable(
                name: "PersonManufactories");

            migrationBuilder.DropTable(
                name: "PersonRepresentativeOffices");

            migrationBuilder.DropTable(
                name: "DepartmentPositionLevels");

            migrationBuilder.DropTable(
                name: "ManufactoryPositions");

            migrationBuilder.DropTable(
                name: "PersonBranches");

            migrationBuilder.DropTable(
                name: "RepresentativeOfficePosition");

            migrationBuilder.DropTable(
                name: "ManufactoryPositionLevels");

            migrationBuilder.DropTable(
                name: "PersonCompanies");

            migrationBuilder.DropTable(
                name: "RepresentativeOfficePositionLevel");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "PositionLevels");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "BranchPositions",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "LevelId",
                table: "BranchPositions",
                newName: "BranchId");

            migrationBuilder.RenameIndex(
                name: "IX_BranchPositions_LevelId",
                table: "BranchPositions",
                newName: "IX_BranchPositions_BranchId");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "BranchPositions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
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
                    table.PrimaryKey("PK_Branches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Branches_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonEmployeeWorkings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BranchPositionId = table.Column<int>(nullable: false),
                    BranchId = table.Column<int>(nullable: false),
                    PositionBeginDate = table.Column<DateTime>(nullable: true),
                    PositionEndDate = table.Column<DateTime>(nullable: true),
                    TrialWorkBeginDate = table.Column<DateTime>(nullable: true),
                    WorkBeginDate = table.Column<DateTime>(nullable: true),
                    IsMainPosition = table.Column<bool>(nullable: false),
                    IsWorking = table.Column<bool>(nullable: false),
                    PersonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonEmployeeWorkings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonEmployeeWorkings_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonEmployeeWorkings_BranchPositions_BranchPositionId",
                        column: x => x.BranchPositionId,
                        principalTable: "BranchPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonEmployeeWorkings_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Branches_CompanyId",
                table: "Branches",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonEmployeeWorkings_BranchId",
                table: "PersonEmployeeWorkings",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonEmployeeWorkings_BranchPositionId",
                table: "PersonEmployeeWorkings",
                column: "BranchPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonEmployeeWorkings_PersonId",
                table: "PersonEmployeeWorkings",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_BranchPositions_Branches_BranchId",
                table: "BranchPositions",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BranchPositions_Branches_BranchId",
                table: "BranchPositions");

            migrationBuilder.DropTable(
                name: "PersonEmployeeWorkings");

            migrationBuilder.DropTable(
                name: "PersonUsers");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "BranchPositions");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "BranchPositions",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "BranchId",
                table: "BranchPositions",
                newName: "LevelId");

            migrationBuilder.RenameIndex(
                name: "IX_BranchPositions_BranchId",
                table: "BranchPositions",
                newName: "IX_BranchPositions_LevelId");

            migrationBuilder.CreateTable(
                name: "BranchPositionLevels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BranchId = table.Column<int>(nullable: false),
                    Color = table.Column<string>(nullable: true),
                    Editable = table.Column<bool>(nullable: false),
                    Level = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchPositionLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentPositionLevels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Color = table.Column<string>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: false),
                    Editable = table.Column<bool>(nullable: false),
                    Level = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentPositionLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ManufactoryPositionLevels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Color = table.Column<string>(nullable: true),
                    Editable = table.Column<bool>(nullable: false),
                    Level = table.Column<string>(nullable: true),
                    ManufactoryId = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManufactoryPositionLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PositionLevels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Color = table.Column<string>(nullable: true),
                    CompanyId = table.Column<int>(nullable: false),
                    Editable = table.Column<bool>(nullable: false),
                    Level = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RepresentativeOfficePositionLevel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Color = table.Column<string>(nullable: true),
                    Editable = table.Column<bool>(nullable: false),
                    Level = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    RepresentativeOfficeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepresentativeOfficePositionLevel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentPositions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LevelId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentPositions_DepartmentPositionLevels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "DepartmentPositionLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManufactoryPositions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LevelId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManufactoryPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManufactoryPositions_ManufactoryPositionLevels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "ManufactoryPositionLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LevelId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Positions_PositionLevels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "PositionLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RepresentativeOfficePosition",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LevelId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepresentativeOfficePosition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepresentativeOfficePosition_RepresentativeOfficePositionLevel_LevelId",
                        column: x => x.LevelId,
                        principalTable: "RepresentativeOfficePositionLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonCompanies",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    PositionId = table.Column<int>(nullable: true),
                    TrialWorkBeginDate = table.Column<string>(nullable: true),
                    WorkBeginDate = table.Column<string>(nullable: true),
                    WorkEndDate = table.Column<string>(nullable: true),
                    WorkStatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonCompanies", x => new { x.PersonId, x.CompanyId });
                    table.ForeignKey(
                        name: "FK_PersonCompanies_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonCompanies_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonBranches",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false),
                    BranchId = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    PositionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonBranches", x => new { x.PersonId, x.BranchId });
                    table.ForeignKey(
                        name: "FK_PersonBranches_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonBranches_BranchPositions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "BranchPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonBranches_PersonCompanies_PersonId_CompanyId",
                        columns: x => new { x.PersonId, x.CompanyId },
                        principalTable: "PersonCompanies",
                        principalColumns: new[] { "PersonId", "CompanyId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonRepresentativeOffices",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false),
                    RepresentativeOfficeId = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    PositionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonRepresentativeOffices", x => new { x.PersonId, x.RepresentativeOfficeId });
                    table.ForeignKey(
                        name: "FK_PersonRepresentativeOffices_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonRepresentativeOffices_RepresentativeOfficePosition_PositionId",
                        column: x => x.PositionId,
                        principalTable: "RepresentativeOfficePosition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonRepresentativeOffices_PersonCompanies_PersonId_CompanyId",
                        columns: x => new { x.PersonId, x.CompanyId },
                        principalTable: "PersonCompanies",
                        principalColumns: new[] { "PersonId", "CompanyId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonManufactories",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false),
                    ManufactoryId = table.Column<int>(nullable: false),
                    BranchId = table.Column<int>(nullable: true),
                    CompanyId = table.Column<int>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    PositionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonManufactories", x => new { x.PersonId, x.ManufactoryId });
                    table.ForeignKey(
                        name: "FK_PersonManufactories_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonManufactories_ManufactoryPositions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "ManufactoryPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonManufactories_PersonBranches_PersonId_BranchId",
                        columns: x => new { x.PersonId, x.BranchId },
                        principalTable: "PersonBranches",
                        principalColumns: new[] { "PersonId", "BranchId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonManufactories_PersonCompanies_PersonId_CompanyId",
                        columns: x => new { x.PersonId, x.CompanyId },
                        principalTable: "PersonCompanies",
                        principalColumns: new[] { "PersonId", "CompanyId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonDepartments",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false),
                    DepartmentId = table.Column<int>(nullable: false),
                    BranchId = table.Column<int>(nullable: true),
                    CompanyId = table.Column<int>(nullable: true),
                    ManufactoryId = table.Column<int>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    PositionId = table.Column<int>(nullable: true),
                    RepresentativeOfficeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonDepartments", x => new { x.PersonId, x.DepartmentId });
                    table.ForeignKey(
                        name: "FK_PersonDepartments_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonDepartments_DepartmentPositions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "DepartmentPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonDepartments_PersonBranches_PersonId_BranchId",
                        columns: x => new { x.PersonId, x.BranchId },
                        principalTable: "PersonBranches",
                        principalColumns: new[] { "PersonId", "BranchId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonDepartments_PersonCompanies_PersonId_CompanyId",
                        columns: x => new { x.PersonId, x.CompanyId },
                        principalTable: "PersonCompanies",
                        principalColumns: new[] { "PersonId", "CompanyId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonDepartments_PersonManufactories_PersonId_ManufactoryId",
                        columns: x => new { x.PersonId, x.ManufactoryId },
                        principalTable: "PersonManufactories",
                        principalColumns: new[] { "PersonId", "ManufactoryId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonDepartments_PersonRepresentativeOffices_PersonId_RepresentativeOfficeId",
                        columns: x => new { x.PersonId, x.RepresentativeOfficeId },
                        principalTable: "PersonRepresentativeOffices",
                        principalColumns: new[] { "PersonId", "RepresentativeOfficeId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BranchPositionLevels_BranchId",
                table: "BranchPositionLevels",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentPositionLevels_DepartmentId",
                table: "DepartmentPositionLevels",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentPositions_LevelId",
                table: "DepartmentPositions",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_ManufactoryPositionLevels_ManufactoryId",
                table: "ManufactoryPositionLevels",
                column: "ManufactoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ManufactoryPositions_LevelId",
                table: "ManufactoryPositions",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonBranches_PositionId",
                table: "PersonBranches",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonBranches_PersonId_CompanyId",
                table: "PersonBranches",
                columns: new[] { "PersonId", "CompanyId" });

            migrationBuilder.CreateIndex(
                name: "IX_PersonCompanies_PositionId",
                table: "PersonCompanies",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonDepartments_PositionId",
                table: "PersonDepartments",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonDepartments_PersonId_BranchId",
                table: "PersonDepartments",
                columns: new[] { "PersonId", "BranchId" });

            migrationBuilder.CreateIndex(
                name: "IX_PersonDepartments_PersonId_CompanyId",
                table: "PersonDepartments",
                columns: new[] { "PersonId", "CompanyId" });

            migrationBuilder.CreateIndex(
                name: "IX_PersonDepartments_PersonId_ManufactoryId",
                table: "PersonDepartments",
                columns: new[] { "PersonId", "ManufactoryId" });

            migrationBuilder.CreateIndex(
                name: "IX_PersonDepartments_PersonId_RepresentativeOfficeId",
                table: "PersonDepartments",
                columns: new[] { "PersonId", "RepresentativeOfficeId" });

            migrationBuilder.CreateIndex(
                name: "IX_PersonManufactories_PositionId",
                table: "PersonManufactories",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonManufactories_PersonId_BranchId",
                table: "PersonManufactories",
                columns: new[] { "PersonId", "BranchId" });

            migrationBuilder.CreateIndex(
                name: "IX_PersonManufactories_PersonId_CompanyId",
                table: "PersonManufactories",
                columns: new[] { "PersonId", "CompanyId" });

            migrationBuilder.CreateIndex(
                name: "IX_PersonRepresentativeOffices_PositionId",
                table: "PersonRepresentativeOffices",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonRepresentativeOffices_PersonId_CompanyId",
                table: "PersonRepresentativeOffices",
                columns: new[] { "PersonId", "CompanyId" });

            migrationBuilder.CreateIndex(
                name: "IX_PositionLevels_CompanyId",
                table: "PositionLevels",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_LevelId",
                table: "Positions",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_RepresentativeOfficePosition_LevelId",
                table: "RepresentativeOfficePosition",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_RepresentativeOfficePositionLevel_RepresentativeOfficeId",
                table: "RepresentativeOfficePositionLevel",
                column: "RepresentativeOfficeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BranchPositions_BranchPositionLevels_LevelId",
                table: "BranchPositions",
                column: "LevelId",
                principalTable: "BranchPositionLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
