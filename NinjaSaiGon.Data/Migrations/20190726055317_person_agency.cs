using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NinjaSaiGon.Data.Migrations
{
    public partial class person_agency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddressTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgencyBusinesses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyBusinesses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgencyDiscountCustomerTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyDiscountCustomerTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgencyDiscountTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyDiscountTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgencyGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankAccountTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankCardTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankCardTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BranchPositionLevels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Level = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Editable = table.Column<bool>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    BranchId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchPositionLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentPositionLevels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Level = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Editable = table.Column<bool>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    DepartmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentPositionLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ethnics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ethnics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IDDocumentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDDocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ManufactoryPositionLevels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Level = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Editable = table.Column<bool>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    ManufactoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManufactoryPositionLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaritalStatusTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaritalStatusTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nationalities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationalities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OTTTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OTTTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTermTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTermTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonalDOBTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalDOBTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonalNameTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalNameTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonalPhotoTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalPhotoTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonCustomerSourceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonCustomerSourceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonLevels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Points = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhoneTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PickupTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PickupTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PositionLevels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Level = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Editable = table.Column<bool>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Religions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Religions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RepresentativeOfficePositionLevel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Level = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Editable = table.Column<bool>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    RepresentativeOfficeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepresentativeOfficePositionLevel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SocialTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Agencies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AgencyType = table.Column<int>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    TradingName = table.Column<string>(nullable: true),
                    TaxCode = table.Column<string>(nullable: true),
                    BusinessRegNumber = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    AgencyGroupId = table.Column<int>(nullable: true),
                    AgencyBusinessId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agencies_AgencyBusinesses_AgencyBusinessId",
                        column: x => x.AgencyBusinessId,
                        principalTable: "AgencyBusinesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Agencies_AgencyGroups_AgencyGroupId",
                        column: x => x.AgencyGroupId,
                        principalTable: "AgencyGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BranchPositions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true),
                    LevelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BranchPositions_BranchPositionLevels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "BranchPositionLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentPositions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true),
                    LevelId = table.Column<int>(nullable: false)
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
                    Value = table.Column<string>(nullable: true),
                    LevelId = table.Column<int>(nullable: false)
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
                    Value = table.Column<string>(nullable: true),
                    LevelId = table.Column<int>(nullable: false)
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
                name: "DistrictPlaces",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ProvinceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistrictPlaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DistrictPlaces_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RepresentativeOfficePosition",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true),
                    LevelId = table.Column<int>(nullable: false)
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
                name: "AgencyBanks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountNumber = table.Column<string>(nullable: true),
                    BankName = table.Column<string>(nullable: true),
                    AccountName = table.Column<string>(nullable: true),
                    SwiftCode = table.Column<string>(nullable: true),
                    BankBranch = table.Column<string>(nullable: true),
                    BankAccountTypeId = table.Column<int>(nullable: false),
                    AgencyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyBanks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgencyBanks_Agencies_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgencyBanks_BankAccountTypes_BankAccountTypeId",
                        column: x => x.BankAccountTypeId,
                        principalTable: "BankAccountTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgencyContactInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyContactInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgencyContactInfos_Agencies_Id",
                        column: x => x.Id,
                        principalTable: "Agencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgencyDeliveries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PickupTypeId = table.Column<int>(nullable: true),
                    AgencyId = table.Column<int>(nullable: false),
                    AgencyDiscountCustomerTypeId = table.Column<int>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyDeliveries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgencyDeliveries_AgencyDiscountCustomerTypes_AgencyDiscountCustomerTypeId",
                        column: x => x.AgencyDiscountCustomerTypeId,
                        principalTable: "AgencyDiscountCustomerTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgencyDeliveries_Agencies_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgencyDeliveries_PickupTypes_PickupTypeId",
                        column: x => x.PickupTypeId,
                        principalTable: "PickupTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AgencyPayments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PaymentTypeId = table.Column<int>(nullable: true),
                    CurrencyTypeId = table.Column<int>(nullable: true),
                    PaymentTermTypeId = table.Column<int>(nullable: true),
                    PaymentLimit = table.Column<int>(nullable: false),
                    InterestRate = table.Column<float>(nullable: false),
                    PaymentCredit = table.Column<int>(nullable: false),
                    InvoiceLimit = table.Column<int>(nullable: false),
                    AgencyDiscountTypeId = table.Column<int>(nullable: true),
                    DiscountAmount = table.Column<float>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    AgencyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgencyPayments_AgencyDiscountTypes_AgencyDiscountTypeId",
                        column: x => x.AgencyDiscountTypeId,
                        principalTable: "AgencyDiscountTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgencyPayments_Agencies_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgencyPayments_CurrencyTypes_CurrencyTypeId",
                        column: x => x.CurrencyTypeId,
                        principalTable: "CurrencyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgencyPayments_PaymentTermTypes_PaymentTermTypeId",
                        column: x => x.PaymentTermTypeId,
                        principalTable: "PaymentTermTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgencyPayments_PaymentTypes_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    TitlePrefixType = table.Column<int>(nullable: false),
                    PersonType = table.Column<int>(nullable: false),
                    GenderId = table.Column<int>(nullable: false),
                    EthnicId = table.Column<int>(nullable: true),
                    ReligionId = table.Column<int>(nullable: true),
                    PersonLevelId = table.Column<int>(nullable: true),
                    PersonCustomerSourceTypeId = table.Column<int>(nullable: false),
                    CustomerSourceEmpId = table.Column<int>(nullable: true),
                    AgencyId = table.Column<int>(nullable: true),
                    CustomerSourceNote = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PrimaryName = table.Column<string>(nullable: true),
                    PrimaryDOB = table.Column<string>(nullable: true),
                    PrimaryPhoto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_Agencies_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Persons_Persons_CustomerSourceEmpId",
                        column: x => x.CustomerSourceEmpId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Persons_Ethnics_EthnicId",
                        column: x => x.EthnicId,
                        principalTable: "Ethnics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Persons_PersonCustomerSourceTypes_PersonCustomerSourceTypeId",
                        column: x => x.PersonCustomerSourceTypeId,
                        principalTable: "PersonCustomerSourceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Persons_PersonLevels_PersonLevelId",
                        column: x => x.PersonLevelId,
                        principalTable: "PersonLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Persons_Religions_ReligionId",
                        column: x => x.ReligionId,
                        principalTable: "Religions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Wards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    DistrictPlaceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wards_DistrictPlaces_DistrictPlaceId",
                        column: x => x.DistrictPlaceId,
                        principalTable: "DistrictPlaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgencyBankCards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CardNumber = table.Column<string>(nullable: true),
                    ExpiredDate = table.Column<DateTime>(nullable: false),
                    BankCardTypeId = table.Column<int>(nullable: false),
                    AgencyBankId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyBankCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgencyBankCards_AgencyBanks_AgencyBankId",
                        column: x => x.AgencyBankId,
                        principalTable: "AgencyBanks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgencyBankCards_BankCardTypes_BankCardTypeId",
                        column: x => x.BankCardTypeId,
                        principalTable: "BankCardTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgencyContactEmails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    TypeId = table.Column<int>(nullable: false),
                    AgencyContactInfoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyContactEmails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgencyContactEmails_AgencyContactInfos_AgencyContactInfoId",
                        column: x => x.AgencyContactInfoId,
                        principalTable: "AgencyContactInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgencyContactEmails_EmailTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "EmailTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgencyContactOTTs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Account = table.Column<string>(nullable: true),
                    IsPrimary = table.Column<bool>(nullable: false),
                    TypeId = table.Column<int>(nullable: false),
                    AgencyContactInfoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyContactOTTs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgencyContactOTTs_AgencyContactInfos_AgencyContactInfoId",
                        column: x => x.AgencyContactInfoId,
                        principalTable: "AgencyContactInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgencyContactOTTs_OTTTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "OTTTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgencyContactPhones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PhoneNumber = table.Column<string>(nullable: true),
                    IsPrimary = table.Column<bool>(nullable: false),
                    TypeId = table.Column<int>(nullable: false),
                    AgencyContactInfoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyContactPhones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgencyContactPhones_AgencyContactInfos_AgencyContactInfoId",
                        column: x => x.AgencyContactInfoId,
                        principalTable: "AgencyContactInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgencyContactPhones_PhoneTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "PhoneTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgencyContactSocials",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Account = table.Column<string>(nullable: true),
                    IsPrimary = table.Column<bool>(nullable: false),
                    TypeId = table.Column<int>(nullable: false),
                    AgencyContactInfoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyContactSocials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgencyContactSocials_AgencyContactInfos_AgencyContactInfoId",
                        column: x => x.AgencyContactInfoId,
                        principalTable: "AgencyContactInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgencyContactSocials_SocialTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "SocialTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgencyCares",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AgencyId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyCares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgencyCares_Agencies_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgencyCares_Persons_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgencyRepresentatives",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false),
                    AgencyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyRepresentatives", x => new { x.PersonId, x.AgencyId });
                    table.ForeignKey(
                        name: "FK_AgencyRepresentatives_Agencies_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgencyRepresentatives_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    RelativeInfo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactInfos_Persons_Id",
                        column: x => x.Id,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IDCards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IDNumber = table.Column<string>(nullable: true),
                    ProvidedDate = table.Column<string>(nullable: true),
                    ExpriredDate = table.Column<string>(nullable: true),
                    ProvidedPlace = table.Column<string>(nullable: true),
                    PersonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IDCards_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IDDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IDDocumentTypeId = table.Column<int>(nullable: true),
                    IDNumber = table.Column<string>(nullable: true),
                    ProvidedDate = table.Column<string>(nullable: true),
                    ExpriredDate = table.Column<string>(nullable: true),
                    ProvidedPlace = table.Column<string>(nullable: true),
                    PersonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IDDocuments_IDDocumentTypes_IDDocumentTypeId",
                        column: x => x.IDDocumentTypeId,
                        principalTable: "IDDocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IDDocuments_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaritalStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TypeId = table.Column<int>(nullable: false),
                    PersonId = table.Column<int>(nullable: false),
                    From = table.Column<DateTime>(nullable: true),
                    To = table.Column<DateTime>(nullable: true),
                    IsCurrent = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaritalStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaritalStatuses_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaritalStatuses_MaritalStatusTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "MaritalStatusTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Passports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IDNumber = table.Column<string>(nullable: true),
                    ProvidedDate = table.Column<string>(nullable: true),
                    ExpriredDate = table.Column<string>(nullable: true),
                    ProvidedPlace = table.Column<string>(nullable: true),
                    PersonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Passports_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalDOBs",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false),
                    TypeId = table.Column<int>(nullable: false),
                    Day = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalDOBs", x => new { x.PersonId, x.TypeId });
                    table.ForeignKey(
                        name: "FK_PersonalDOBs_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalDOBs_PersonalDOBTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "PersonalDOBTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalHomeTowns",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false),
                    Country = table.Column<string>(nullable: true),
                    Province = table.Column<string>(nullable: true),
                    District = table.Column<string>(nullable: true),
                    Ward = table.Column<string>(nullable: true),
                    MoreInfo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalHomeTowns", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_PersonalHomeTowns_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalNames",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    MidName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    PersonId = table.Column<int>(nullable: false),
                    TypeId = table.Column<int>(nullable: false),
                    From = table.Column<DateTime>(nullable: true),
                    To = table.Column<DateTime>(nullable: true),
                    IsPrimary = table.Column<bool>(nullable: false),
                    FullName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalNames_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalNames_PersonalNameTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "PersonalNameTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalNationality",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false),
                    NationalityId = table.Column<int>(nullable: false),
                    From = table.Column<string>(nullable: true),
                    To = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalNationality", x => new { x.PersonId, x.NationalityId });
                    table.ForeignKey(
                        name: "FK_PersonalNationality_Nationalities_NationalityId",
                        column: x => x.NationalityId,
                        principalTable: "Nationalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalNationality_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TypeId = table.Column<int>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    PersonId = table.Column<int>(nullable: false),
                    IsPrimary = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalPhotos_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalPhotos_PersonalPhotoTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "PersonalPhotoTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalPOBs",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false),
                    Country = table.Column<string>(nullable: true),
                    Province = table.Column<string>(nullable: true),
                    District = table.Column<string>(nullable: true),
                    Ward = table.Column<string>(nullable: true),
                    MoreInfo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalPOBs", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_PersonalPOBs_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonBanks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountNumber = table.Column<string>(nullable: true),
                    BankName = table.Column<string>(nullable: true),
                    AccountName = table.Column<string>(nullable: true),
                    SwiftCode = table.Column<string>(nullable: true),
                    BankBranch = table.Column<string>(nullable: true),
                    BankAccountTypeId = table.Column<int>(nullable: true),
                    PersonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonBanks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonBanks_BankAccountTypes_BankAccountTypeId",
                        column: x => x.BankAccountTypeId,
                        principalTable: "BankAccountTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonBanks_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonCompanies",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false),
                    PositionId = table.Column<int>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    TrialWorkBeginDate = table.Column<string>(nullable: true),
                    WorkStatus = table.Column<bool>(nullable: false),
                    WorkBeginDate = table.Column<string>(nullable: true),
                    WorkEndDate = table.Column<string>(nullable: true)
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
                name: "PersonCustomerCares",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonCustomerCares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonCustomerCares_Persons_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonCustomerCares_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PersonCustomerWorkings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Position = table.Column<string>(nullable: true),
                    Department = table.Column<string>(nullable: true),
                    Company = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    IsMainPosition = table.Column<bool>(nullable: false),
                    IsWorking = table.Column<bool>(nullable: false),
                    PersonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonCustomerWorkings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonCustomerWorkings_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    VehicleType = table.Column<string>(nullable: true),
                    Manufacturer = table.Column<string>(nullable: true),
                    CylinderCapacity = table.Column<string>(nullable: true),
                    FrameCode = table.Column<string>(nullable: true),
                    MachineCode = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Owner = table.Column<string>(nullable: true),
                    IsPrimary = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    PersonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleInfos_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgencyContactAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NationalityId = table.Column<int>(nullable: true),
                    ProvinceId = table.Column<int>(nullable: true),
                    DistrictPlaceId = table.Column<int>(nullable: true),
                    WardId = table.Column<int>(nullable: true),
                    MoreInfo = table.Column<string>(nullable: true),
                    From = table.Column<DateTime>(nullable: true),
                    To = table.Column<DateTime>(nullable: true),
                    TypeId = table.Column<int>(nullable: true),
                    AgencyContactInfoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyContactAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgencyContactAddresses_AgencyContactInfos_AgencyContactInfoId",
                        column: x => x.AgencyContactInfoId,
                        principalTable: "AgencyContactInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgencyContactAddresses_DistrictPlaces_DistrictPlaceId",
                        column: x => x.DistrictPlaceId,
                        principalTable: "DistrictPlaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgencyContactAddresses_Nationalities_NationalityId",
                        column: x => x.NationalityId,
                        principalTable: "Nationalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgencyContactAddresses_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgencyContactAddresses_AddressTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "AddressTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgencyContactAddresses_Wards_WardId",
                        column: x => x.WardId,
                        principalTable: "Wards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NationalityId = table.Column<int>(nullable: true),
                    ProvinceId = table.Column<int>(nullable: true),
                    DistrictPlaceId = table.Column<int>(nullable: true),
                    WardId = table.Column<int>(nullable: true),
                    MoreInfo = table.Column<string>(nullable: true),
                    IsPrimary = table.Column<bool>(nullable: false),
                    From = table.Column<DateTime>(nullable: true),
                    To = table.Column<DateTime>(nullable: true),
                    TypeId = table.Column<int>(nullable: true),
                    ContactInfoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactAddresses_ContactInfos_ContactInfoId",
                        column: x => x.ContactInfoId,
                        principalTable: "ContactInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactAddresses_DistrictPlaces_DistrictPlaceId",
                        column: x => x.DistrictPlaceId,
                        principalTable: "DistrictPlaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactAddresses_Nationalities_NationalityId",
                        column: x => x.NationalityId,
                        principalTable: "Nationalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactAddresses_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactAddresses_AddressTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "AddressTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactAddresses_Wards_WardId",
                        column: x => x.WardId,
                        principalTable: "Wards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactEmails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    IsPrimary = table.Column<bool>(nullable: false),
                    TypeId = table.Column<int>(nullable: false),
                    ContactInfoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactEmails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactEmails_ContactInfos_ContactInfoId",
                        column: x => x.ContactInfoId,
                        principalTable: "ContactInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactEmails_EmailTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "EmailTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactOTTs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Account = table.Column<string>(nullable: true),
                    IsPrimary = table.Column<bool>(nullable: false),
                    TypeId = table.Column<int>(nullable: false),
                    ContactInfoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactOTTs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactOTTs_ContactInfos_ContactInfoId",
                        column: x => x.ContactInfoId,
                        principalTable: "ContactInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactOTTs_OTTTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "OTTTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactPhones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PhoneNumber = table.Column<string>(nullable: true),
                    IsPrimary = table.Column<bool>(nullable: false),
                    TypeId = table.Column<int>(nullable: false),
                    ContactInfoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactPhones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactPhones_ContactInfos_ContactInfoId",
                        column: x => x.ContactInfoId,
                        principalTable: "ContactInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactPhones_PhoneTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "PhoneTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactSocials",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Account = table.Column<string>(nullable: true),
                    IsPrimary = table.Column<bool>(nullable: false),
                    TypeId = table.Column<int>(nullable: false),
                    ContactInfoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactSocials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactSocials_ContactInfos_ContactInfoId",
                        column: x => x.ContactInfoId,
                        principalTable: "ContactInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactSocials_SocialTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "SocialTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankCards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CardNumber = table.Column<string>(nullable: true),
                    ExpiredDate = table.Column<DateTime>(nullable: false),
                    BankCardTypeId = table.Column<int>(nullable: true),
                    PersonBankId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankCards_BankCardTypes_BankCardTypeId",
                        column: x => x.BankCardTypeId,
                        principalTable: "BankCardTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BankCards_PersonBanks_PersonBankId",
                        column: x => x.PersonBankId,
                        principalTable: "PersonBanks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonBranches",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false),
                    BranchId = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: true),
                    PositionId = table.Column<int>(nullable: true),
                    Note = table.Column<string>(nullable: true)
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
                    PositionId = table.Column<int>(nullable: true),
                    Note = table.Column<string>(nullable: true)
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
                name: "VehiclePhotos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Url = table.Column<string>(nullable: true),
                    VehicleInfoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehiclePhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehiclePhotos_VehicleInfos_VehicleInfoId",
                        column: x => x.VehicleInfoId,
                        principalTable: "VehicleInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonManufactories",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false),
                    ManufactoryId = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: true),
                    BranchId = table.Column<int>(nullable: true),
                    PositionId = table.Column<int>(nullable: true),
                    Note = table.Column<string>(nullable: true)
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
                    CompanyId = table.Column<int>(nullable: true),
                    BranchId = table.Column<int>(nullable: true),
                    ManufactoryId = table.Column<int>(nullable: true),
                    RepresentativeOfficeId = table.Column<int>(nullable: true),
                    PositionId = table.Column<int>(nullable: true),
                    Note = table.Column<string>(nullable: true)
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
                name: "IX_Agencies_AgencyBusinessId",
                table: "Agencies",
                column: "AgencyBusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Agencies_AgencyGroupId",
                table: "Agencies",
                column: "AgencyGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyBankCards_AgencyBankId",
                table: "AgencyBankCards",
                column: "AgencyBankId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyBankCards_BankCardTypeId",
                table: "AgencyBankCards",
                column: "BankCardTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyBanks_AgencyId",
                table: "AgencyBanks",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyBanks_BankAccountTypeId",
                table: "AgencyBanks",
                column: "BankAccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyCares_AgencyId",
                table: "AgencyCares",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyCares_EmployeeId",
                table: "AgencyCares",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyContactAddresses_AgencyContactInfoId",
                table: "AgencyContactAddresses",
                column: "AgencyContactInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyContactAddresses_DistrictPlaceId",
                table: "AgencyContactAddresses",
                column: "DistrictPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyContactAddresses_NationalityId",
                table: "AgencyContactAddresses",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyContactAddresses_ProvinceId",
                table: "AgencyContactAddresses",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyContactAddresses_TypeId",
                table: "AgencyContactAddresses",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyContactAddresses_WardId",
                table: "AgencyContactAddresses",
                column: "WardId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyContactEmails_AgencyContactInfoId",
                table: "AgencyContactEmails",
                column: "AgencyContactInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyContactEmails_TypeId",
                table: "AgencyContactEmails",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyContactOTTs_AgencyContactInfoId",
                table: "AgencyContactOTTs",
                column: "AgencyContactInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyContactOTTs_TypeId",
                table: "AgencyContactOTTs",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyContactPhones_AgencyContactInfoId",
                table: "AgencyContactPhones",
                column: "AgencyContactInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyContactPhones_TypeId",
                table: "AgencyContactPhones",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyContactSocials_AgencyContactInfoId",
                table: "AgencyContactSocials",
                column: "AgencyContactInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyContactSocials_TypeId",
                table: "AgencyContactSocials",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyDeliveries_AgencyDiscountCustomerTypeId",
                table: "AgencyDeliveries",
                column: "AgencyDiscountCustomerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyDeliveries_AgencyId",
                table: "AgencyDeliveries",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyDeliveries_PickupTypeId",
                table: "AgencyDeliveries",
                column: "PickupTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyPayments_AgencyDiscountTypeId",
                table: "AgencyPayments",
                column: "AgencyDiscountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyPayments_AgencyId",
                table: "AgencyPayments",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyPayments_CurrencyTypeId",
                table: "AgencyPayments",
                column: "CurrencyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyPayments_PaymentTermTypeId",
                table: "AgencyPayments",
                column: "PaymentTermTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyPayments_PaymentTypeId",
                table: "AgencyPayments",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyRepresentatives_AgencyId",
                table: "AgencyRepresentatives",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_BankCards_BankCardTypeId",
                table: "BankCards",
                column: "BankCardTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BankCards_PersonBankId",
                table: "BankCards",
                column: "PersonBankId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchPositionLevels_BranchId",
                table: "BranchPositionLevels",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchPositions_LevelId",
                table: "BranchPositions",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactAddresses_ContactInfoId",
                table: "ContactAddresses",
                column: "ContactInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactAddresses_DistrictPlaceId",
                table: "ContactAddresses",
                column: "DistrictPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactAddresses_NationalityId",
                table: "ContactAddresses",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactAddresses_ProvinceId",
                table: "ContactAddresses",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactAddresses_TypeId",
                table: "ContactAddresses",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactAddresses_WardId",
                table: "ContactAddresses",
                column: "WardId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactEmails_ContactInfoId",
                table: "ContactEmails",
                column: "ContactInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactEmails_TypeId",
                table: "ContactEmails",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactOTTs_ContactInfoId",
                table: "ContactOTTs",
                column: "ContactInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactOTTs_TypeId",
                table: "ContactOTTs",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPhones_ContactInfoId",
                table: "ContactPhones",
                column: "ContactInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPhones_TypeId",
                table: "ContactPhones",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactSocials_ContactInfoId",
                table: "ContactSocials",
                column: "ContactInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactSocials_TypeId",
                table: "ContactSocials",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentPositionLevels_DepartmentId",
                table: "DepartmentPositionLevels",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentPositions_LevelId",
                table: "DepartmentPositions",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_DistrictPlaces_ProvinceId",
                table: "DistrictPlaces",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_IDCards_PersonId",
                table: "IDCards",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_IDDocuments_IDDocumentTypeId",
                table: "IDDocuments",
                column: "IDDocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_IDDocuments_PersonId",
                table: "IDDocuments",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_ManufactoryPositionLevels_ManufactoryId",
                table: "ManufactoryPositionLevels",
                column: "ManufactoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ManufactoryPositions_LevelId",
                table: "ManufactoryPositions",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_MaritalStatuses_PersonId",
                table: "MaritalStatuses",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_MaritalStatuses_TypeId",
                table: "MaritalStatuses",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Passports_PersonId",
                table: "Passports",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDOBs_TypeId",
                table: "PersonalDOBs",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalNames_PersonId",
                table: "PersonalNames",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalNames_TypeId",
                table: "PersonalNames",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalNationality_NationalityId",
                table: "PersonalNationality",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalPhotos_PersonId",
                table: "PersonalPhotos",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalPhotos_TypeId",
                table: "PersonalPhotos",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonBanks_BankAccountTypeId",
                table: "PersonBanks",
                column: "BankAccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonBanks_PersonId",
                table: "PersonBanks",
                column: "PersonId");

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
                name: "IX_PersonCustomerCares_EmployeeId",
                table: "PersonCustomerCares",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonCustomerCares_PersonId",
                table: "PersonCustomerCares",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonCustomerWorkings_PersonId",
                table: "PersonCustomerWorkings",
                column: "PersonId");

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
                name: "IX_Persons_AgencyId",
                table: "Persons",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_CustomerSourceEmpId",
                table: "Persons",
                column: "CustomerSourceEmpId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_EthnicId",
                table: "Persons",
                column: "EthnicId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_PersonCustomerSourceTypeId",
                table: "Persons",
                column: "PersonCustomerSourceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_PersonLevelId",
                table: "Persons",
                column: "PersonLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_ReligionId",
                table: "Persons",
                column: "ReligionId");

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

            migrationBuilder.CreateIndex(
                name: "IX_VehicleInfos_PersonId",
                table: "VehicleInfos",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_VehiclePhotos_VehicleInfoId",
                table: "VehiclePhotos",
                column: "VehicleInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Wards_DistrictPlaceId",
                table: "Wards",
                column: "DistrictPlaceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgencyBankCards");

            migrationBuilder.DropTable(
                name: "AgencyCares");

            migrationBuilder.DropTable(
                name: "AgencyContactAddresses");

            migrationBuilder.DropTable(
                name: "AgencyContactEmails");

            migrationBuilder.DropTable(
                name: "AgencyContactOTTs");

            migrationBuilder.DropTable(
                name: "AgencyContactPhones");

            migrationBuilder.DropTable(
                name: "AgencyContactSocials");

            migrationBuilder.DropTable(
                name: "AgencyDeliveries");

            migrationBuilder.DropTable(
                name: "AgencyPayments");

            migrationBuilder.DropTable(
                name: "AgencyRepresentatives");

            migrationBuilder.DropTable(
                name: "BankCards");

            migrationBuilder.DropTable(
                name: "ContactAddresses");

            migrationBuilder.DropTable(
                name: "ContactEmails");

            migrationBuilder.DropTable(
                name: "ContactOTTs");

            migrationBuilder.DropTable(
                name: "ContactPhones");

            migrationBuilder.DropTable(
                name: "ContactSocials");

            migrationBuilder.DropTable(
                name: "IDCards");

            migrationBuilder.DropTable(
                name: "IDDocuments");

            migrationBuilder.DropTable(
                name: "MaritalStatuses");

            migrationBuilder.DropTable(
                name: "Passports");

            migrationBuilder.DropTable(
                name: "PersonalDOBs");

            migrationBuilder.DropTable(
                name: "PersonalHomeTowns");

            migrationBuilder.DropTable(
                name: "PersonalNames");

            migrationBuilder.DropTable(
                name: "PersonalNationality");

            migrationBuilder.DropTable(
                name: "PersonalPhotos");

            migrationBuilder.DropTable(
                name: "PersonalPOBs");

            migrationBuilder.DropTable(
                name: "PersonCustomerCares");

            migrationBuilder.DropTable(
                name: "PersonCustomerWorkings");

            migrationBuilder.DropTable(
                name: "PersonDepartments");

            migrationBuilder.DropTable(
                name: "VehiclePhotos");

            migrationBuilder.DropTable(
                name: "AgencyBanks");

            migrationBuilder.DropTable(
                name: "AgencyContactInfos");

            migrationBuilder.DropTable(
                name: "AgencyDiscountCustomerTypes");

            migrationBuilder.DropTable(
                name: "PickupTypes");

            migrationBuilder.DropTable(
                name: "AgencyDiscountTypes");

            migrationBuilder.DropTable(
                name: "CurrencyTypes");

            migrationBuilder.DropTable(
                name: "PaymentTermTypes");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropTable(
                name: "BankCardTypes");

            migrationBuilder.DropTable(
                name: "PersonBanks");

            migrationBuilder.DropTable(
                name: "AddressTypes");

            migrationBuilder.DropTable(
                name: "Wards");

            migrationBuilder.DropTable(
                name: "EmailTypes");

            migrationBuilder.DropTable(
                name: "OTTTypes");

            migrationBuilder.DropTable(
                name: "PhoneTypes");

            migrationBuilder.DropTable(
                name: "ContactInfos");

            migrationBuilder.DropTable(
                name: "SocialTypes");

            migrationBuilder.DropTable(
                name: "IDDocumentTypes");

            migrationBuilder.DropTable(
                name: "MaritalStatusTypes");

            migrationBuilder.DropTable(
                name: "PersonalDOBTypes");

            migrationBuilder.DropTable(
                name: "PersonalNameTypes");

            migrationBuilder.DropTable(
                name: "Nationalities");

            migrationBuilder.DropTable(
                name: "PersonalPhotoTypes");

            migrationBuilder.DropTable(
                name: "DepartmentPositions");

            migrationBuilder.DropTable(
                name: "PersonManufactories");

            migrationBuilder.DropTable(
                name: "PersonRepresentativeOffices");

            migrationBuilder.DropTable(
                name: "VehicleInfos");

            migrationBuilder.DropTable(
                name: "BankAccountTypes");

            migrationBuilder.DropTable(
                name: "DistrictPlaces");

            migrationBuilder.DropTable(
                name: "DepartmentPositionLevels");

            migrationBuilder.DropTable(
                name: "ManufactoryPositions");

            migrationBuilder.DropTable(
                name: "PersonBranches");

            migrationBuilder.DropTable(
                name: "RepresentativeOfficePosition");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropTable(
                name: "ManufactoryPositionLevels");

            migrationBuilder.DropTable(
                name: "BranchPositions");

            migrationBuilder.DropTable(
                name: "PersonCompanies");

            migrationBuilder.DropTable(
                name: "RepresentativeOfficePositionLevel");

            migrationBuilder.DropTable(
                name: "BranchPositionLevels");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Agencies");

            migrationBuilder.DropTable(
                name: "Ethnics");

            migrationBuilder.DropTable(
                name: "PersonCustomerSourceTypes");

            migrationBuilder.DropTable(
                name: "PersonLevels");

            migrationBuilder.DropTable(
                name: "Religions");

            migrationBuilder.DropTable(
                name: "PositionLevels");

            migrationBuilder.DropTable(
                name: "AgencyBusinesses");

            migrationBuilder.DropTable(
                name: "AgencyGroups");
        }
    }
}
