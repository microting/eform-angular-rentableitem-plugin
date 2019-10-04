using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RentableItems.Pn.Migrations
{
    public partial class ChangingSDKCaseIdToInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created_By_User_Id",
                table: "RentableItemsVersion");

            migrationBuilder.DropColumn(
                name: "Created_at",
                table: "RentableItemsVersion");

            migrationBuilder.DropColumn(
                name: "Updated_By_User_Id",
                table: "RentableItemsVersion");

            migrationBuilder.DropColumn(
                name: "Updated_at",
                table: "RentableItemsVersion");

            migrationBuilder.DropColumn(
                name: "Workflow_state",
                table: "RentableItemsVersion");

            migrationBuilder.DropColumn(
                name: "Created_By_User_Id",
                table: "RentableItemsSettingsVersions");

            migrationBuilder.DropColumn(
                name: "Created_at",
                table: "RentableItemsSettingsVersions");

            migrationBuilder.DropColumn(
                name: "Eform_Id",
                table: "RentableItemsSettingsVersions");

            migrationBuilder.DropColumn(
                name: "Updated_at",
                table: "RentableItemsSettingsVersions");

            migrationBuilder.DropColumn(
                name: "Workflow_state",
                table: "RentableItemsSettingsVersions");

            migrationBuilder.DropColumn(
                name: "Created_By_User_Id",
                table: "RentableItemsSettings");

            migrationBuilder.DropColumn(
                name: "Created_at",
                table: "RentableItemsSettings");

            migrationBuilder.DropColumn(
                name: "Updated_By_User_Id",
                table: "RentableItemsSettings");

            migrationBuilder.DropColumn(
                name: "Updated_at",
                table: "RentableItemsSettings");

            migrationBuilder.DropColumn(
                name: "Workflow_state",
                table: "RentableItemsSettings");

            migrationBuilder.DropColumn(
                name: "Created_By_User_Id",
                table: "RentableItemsContractVersions");

            migrationBuilder.DropColumn(
                name: "Created_at",
                table: "RentableItemsContractVersions");

            migrationBuilder.DropColumn(
                name: "Updated_By_User_Id",
                table: "RentableItemsContractVersions");

            migrationBuilder.DropColumn(
                name: "Updated_at",
                table: "RentableItemsContractVersions");

            migrationBuilder.DropColumn(
                name: "Workflow_state",
                table: "RentableItemsContractVersions");

            migrationBuilder.DropColumn(
                name: "Created_By_User_Id",
                table: "RentableItemContract");

            migrationBuilder.DropColumn(
                name: "Created_at",
                table: "RentableItemContract");

            migrationBuilder.DropColumn(
                name: "Updated_By_User_Id",
                table: "RentableItemContract");

            migrationBuilder.DropColumn(
                name: "Updated_at",
                table: "RentableItemContract");

            migrationBuilder.DropColumn(
                name: "Workflow_state",
                table: "RentableItemContract");

            migrationBuilder.DropColumn(
                name: "Created_By_User_Id",
                table: "RentableItem");

            migrationBuilder.DropColumn(
                name: "Created_at",
                table: "RentableItem");

            migrationBuilder.DropColumn(
                name: "Updated_By_User_Id",
                table: "RentableItem");

            migrationBuilder.DropColumn(
                name: "Updated_at",
                table: "RentableItem");

            migrationBuilder.DropColumn(
                name: "Workflow_state",
                table: "RentableItem");

            migrationBuilder.DropColumn(
                name: "Created_By_User_Id",
                table: "ContractVersions");

            migrationBuilder.DropColumn(
                name: "Updated_By_User_Id",
                table: "ContractVersions");

            migrationBuilder.DropColumn(
                name: "Created_By_User_Id",
                table: "ContractInspectionVersion");

            migrationBuilder.DropColumn(
                name: "Created_at",
                table: "ContractInspectionVersion");

            migrationBuilder.DropColumn(
                name: "SDK_Case_Id",
                table: "ContractInspectionVersion");

            migrationBuilder.DropColumn(
                name: "Updated_at",
                table: "ContractInspectionVersion");

            migrationBuilder.DropColumn(
                name: "Workflow_state",
                table: "ContractInspectionVersion");

            migrationBuilder.DropColumn(
                name: "Created_By_User_Id",
                table: "ContractInspection");

            migrationBuilder.DropColumn(
                name: "SDK_Case_Id",
                table: "ContractInspection");

            migrationBuilder.DropColumn(
                name: "Created_By_User_Id",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "Updated_By_User_Id",
                table: "Contract");

            migrationBuilder.RenameColumn(
                name: "Updated_By_User_Id",
                table: "RentableItemsSettingsVersions",
                newName: "eFormId");

            migrationBuilder.RenameColumn(
                name: "eForm_Id",
                table: "RentableItemsSettings",
                newName: "eFormId");

            migrationBuilder.RenameColumn(
                name: "Updated_By_User_Id",
                table: "ContractInspectionVersion",
                newName: "SDKCaseId");

            migrationBuilder.RenameColumn(
                name: "Updated_By_User_Id",
                table: "ContractInspection",
                newName: "SDKCaseId");

            migrationBuilder.AlterColumn<int>(
                name: "Version",
                table: "ContractVersions",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ContractVersions",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ContractInspection",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Version",
                table: "Contract",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Contract",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "eFormId",
                table: "RentableItemsSettingsVersions",
                newName: "Updated_By_User_Id");

            migrationBuilder.RenameColumn(
                name: "eFormId",
                table: "RentableItemsSettings",
                newName: "eForm_Id");

            migrationBuilder.RenameColumn(
                name: "SDKCaseId",
                table: "ContractInspectionVersion",
                newName: "Updated_By_User_Id");

            migrationBuilder.RenameColumn(
                name: "SDKCaseId",
                table: "ContractInspection",
                newName: "Updated_By_User_Id");

            migrationBuilder.AddColumn<int>(
                name: "Created_By_User_Id",
                table: "RentableItemsVersion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created_at",
                table: "RentableItemsVersion",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Updated_By_User_Id",
                table: "RentableItemsVersion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated_at",
                table: "RentableItemsVersion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Workflow_state",
                table: "RentableItemsVersion",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Created_By_User_Id",
                table: "RentableItemsSettingsVersions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created_at",
                table: "RentableItemsSettingsVersions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Eform_Id",
                table: "RentableItemsSettingsVersions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated_at",
                table: "RentableItemsSettingsVersions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Workflow_state",
                table: "RentableItemsSettingsVersions",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Created_By_User_Id",
                table: "RentableItemsSettings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created_at",
                table: "RentableItemsSettings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Updated_By_User_Id",
                table: "RentableItemsSettings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated_at",
                table: "RentableItemsSettings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Workflow_state",
                table: "RentableItemsSettings",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Created_By_User_Id",
                table: "RentableItemsContractVersions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created_at",
                table: "RentableItemsContractVersions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Updated_By_User_Id",
                table: "RentableItemsContractVersions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated_at",
                table: "RentableItemsContractVersions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Workflow_state",
                table: "RentableItemsContractVersions",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Created_By_User_Id",
                table: "RentableItemContract",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created_at",
                table: "RentableItemContract",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Updated_By_User_Id",
                table: "RentableItemContract",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated_at",
                table: "RentableItemContract",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Workflow_state",
                table: "RentableItemContract",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Created_By_User_Id",
                table: "RentableItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created_at",
                table: "RentableItem",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Updated_By_User_Id",
                table: "RentableItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated_at",
                table: "RentableItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Workflow_state",
                table: "RentableItem",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Version",
                table: "ContractVersions",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ContractVersions",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<int>(
                name: "Created_By_User_Id",
                table: "ContractVersions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Updated_By_User_Id",
                table: "ContractVersions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Created_By_User_Id",
                table: "ContractInspectionVersion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created_at",
                table: "ContractInspectionVersion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SDK_Case_Id",
                table: "ContractInspectionVersion",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated_at",
                table: "ContractInspectionVersion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Workflow_state",
                table: "ContractInspectionVersion",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ContractInspection",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<int>(
                name: "Created_By_User_Id",
                table: "ContractInspection",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SDK_Case_Id",
                table: "ContractInspection",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Version",
                table: "Contract",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Contract",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<int>(
                name: "Created_By_User_Id",
                table: "Contract",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Updated_By_User_Id",
                table: "Contract",
                nullable: false,
                defaultValue: 0);
        }
    }
}
