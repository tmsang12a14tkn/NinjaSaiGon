using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace NinjaSaiGon.Data.Migrations
{
    public partial class person_source : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerSourceCusId",
                table: "Persons",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SocialTypeId",
                table: "Persons",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_CustomerSourceCusId",
                table: "Persons",
                column: "CustomerSourceCusId",
                unique: true,
                filter: "[CustomerSourceCusId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_SocialTypeId",
                table: "Persons",
                column: "SocialTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Persons_CustomerSourceCusId",
                table: "Persons",
                column: "CustomerSourceCusId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_SocialTypes_SocialTypeId",
                table: "Persons",
                column: "SocialTypeId",
                principalTable: "SocialTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AlterColumn<DateTime>(
               name: "StartDate",
               table: "PersonCustomerWorkings",
               nullable: true,
               oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "PersonCustomerWorkings",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.DropForeignKey(
               name: "FK_Persons_PersonCustomerSourceTypes_PersonCustomerSourceTypeId",
               table: "Persons");

            migrationBuilder.AlterColumn<int>(
                name: "PersonCustomerSourceTypeId",
                table: "Persons",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_PersonCustomerSourceTypes_PersonCustomerSourceTypeId",
                table: "Persons",
                column: "PersonCustomerSourceTypeId",
                principalTable: "PersonCustomerSourceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiredDate",
                table: "BankCards",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.DropForeignKey(
                name: "FK_PersonCustomerCares_Persons_EmployeeId",
                table: "PersonCustomerCares");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonCustomerCares_Persons_PersonId",
                table: "PersonCustomerCares");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonCustomerCares_Persons_EmployeeId",
                table: "PersonCustomerCares",
                column: "EmployeeId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonCustomerCares_Persons_PersonId",
                table: "PersonCustomerCares",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.DropForeignKey(
                name: "FK_AgencyBankCards_BankCardTypes_BankCardTypeId",
                table: "AgencyBankCards");

            migrationBuilder.DropForeignKey(
                name: "FK_AgencyBanks_BankAccountTypes_BankAccountTypeId",
                table: "AgencyBanks");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "AgencyPayments",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "AgencyPayments",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "AgencyDeliveries",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "AgencyDeliveries",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "AgencyCares",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "AgencyCares",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<int>(
                name: "BankAccountTypeId",
                table: "AgencyBanks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiredDate",
                table: "AgencyBankCards",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<int>(
                name: "BankCardTypeId",
                table: "AgencyBankCards",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_AgencyBankCards_BankCardTypes_BankCardTypeId",
                table: "AgencyBankCards",
                column: "BankCardTypeId",
                principalTable: "BankCardTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AgencyBanks_BankAccountTypes_BankAccountTypeId",
                table: "AgencyBanks",
                column: "BankAccountTypeId",
                principalTable: "BankAccountTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddColumn<int>(
                name: "AgencyId",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderCustomerType",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsMainContact",
                table: "AgencyRepresentatives",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AgencyId",
                table: "Orders",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Agencies_AgencyId",
                table: "Orders",
                column: "AgencyId",
                principalTable: "Agencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Persons_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddColumn<bool>(
               name: "IsPrimary",
               table: "AgencyContactEmails",
               nullable: false,
               defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPrimary",
                table: "AgencyContactAddresses",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Persons_CustomerSourceCusId",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_SocialTypes_SocialTypeId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_CustomerSourceCusId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_SocialTypeId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "CustomerSourceCusId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "SocialTypeId",
                table: "Persons");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "PersonCustomerWorkings",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "PersonCustomerWorkings",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.DropForeignKey(
               name: "FK_Persons_PersonCustomerSourceTypes_PersonCustomerSourceTypeId",
               table: "Persons");

            migrationBuilder.AlterColumn<int>(
                name: "PersonCustomerSourceTypeId",
                table: "Persons",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_PersonCustomerSourceTypes_PersonCustomerSourceTypeId",
                table: "Persons",
                column: "PersonCustomerSourceTypeId",
                principalTable: "PersonCustomerSourceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AlterColumn<DateTime>(
               name: "ExpiredDate",
               table: "BankCards",
               nullable: false,
               oldClrType: typeof(DateTime),
               oldNullable: true);

            migrationBuilder.DropForeignKey(
                name: "FK_PersonCustomerCares_Persons_EmployeeId",
                table: "PersonCustomerCares");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonCustomerCares_Persons_PersonId",
                table: "PersonCustomerCares");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonCustomerCares_Persons_EmployeeId",
                table: "PersonCustomerCares",
                column: "EmployeeId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonCustomerCares_Persons_PersonId",
                table: "PersonCustomerCares",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.DropForeignKey(
                name: "FK_AgencyBankCards_BankCardTypes_BankCardTypeId",
                table: "AgencyBankCards");

            migrationBuilder.DropForeignKey(
                name: "FK_AgencyBanks_BankAccountTypes_BankAccountTypeId",
                table: "AgencyBanks");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "AgencyPayments",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "AgencyPayments",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "AgencyDeliveries",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "AgencyDeliveries",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "AgencyCares",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "AgencyCares",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BankAccountTypeId",
                table: "AgencyBanks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiredDate",
                table: "AgencyBankCards",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BankCardTypeId",
                table: "AgencyBankCards",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AgencyBankCards_BankCardTypes_BankCardTypeId",
                table: "AgencyBankCards",
                column: "BankCardTypeId",
                principalTable: "BankCardTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AgencyBanks_BankAccountTypes_BankAccountTypeId",
                table: "AgencyBanks",
                column: "BankAccountTypeId",
                principalTable: "BankAccountTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Agencies_AgencyId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Persons_CustomerId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_AgencyId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AgencyId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderCustomerType",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsMainContact",
                table: "AgencyRepresentatives");

            migrationBuilder.DropColumn(
                name: "IsPrimary",
                table: "AgencyContactEmails");

            migrationBuilder.DropColumn(
                name: "IsPrimary",
                table: "AgencyContactAddresses");
        }
    }
}
