using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RentableItems.Pn.Migrations
{
    public partial class AddingConfigurationValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string autoIDGenStrategy = "SqlServer:ValueGenerationStrategy";
            object autoIDGenStrategyValue = SqlServerValueGenerationStrategy.IdentityColumn;

            // Setup for MySQL Provider
            if (migrationBuilder.ActiveProvider == "Pomelo.EntityFrameworkCore.MySql")
            {
                DbConfig.IsMySQL = true;
                autoIDGenStrategy = "MySql:ValueGenerationStrategy";
                autoIDGenStrategyValue = MySqlValueGenerationStrategy.IdentityColumn;
            }
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "RentableItemsVersion",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "RentableItemsVersion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "RentableItemsVersion",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "RentableItemsVersion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "WorkflowState",
                table: "RentableItemsVersion",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "RentableItemsSettingsVersions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "RentableItemsSettingsVersions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "RentableItemsSettingsVersions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "RentableItemsSettingsVersions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "WorkflowState",
                table: "RentableItemsSettingsVersions",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "RentableItemsSettings",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "RentableItemsSettings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "RentableItemsSettings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "RentableItemsSettings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "WorkflowState",
                table: "RentableItemsSettings",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "RentableItemsFields",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "RentableItemsFields",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "RentableItemsFields",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "RentableItemsFields",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "RentableItemsFields",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "WorkflowState",
                table: "RentableItemsFields",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "RentableItemsContractVersions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "RentableItemsContractVersions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "RentableItemsContractVersions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "RentableItemsContractVersions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "WorkflowState",
                table: "RentableItemsContractVersions",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "RentableItemContract",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "RentableItemContract",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "RentableItemContract",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "RentableItemContract",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "WorkflowState",
                table: "RentableItemContract",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "RentableItem",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "RentableItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "RentableItem",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "RentableItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "WorkflowState",
                table: "RentableItem",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Fields",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Fields",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Fields",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "Fields",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Fields",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "WorkflowState",
                table: "Fields",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "ContractVersions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "ContractVersions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ContractInspectionVersion",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "ContractInspectionVersion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ContractInspectionVersion",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "ContractInspectionVersion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "WorkflowState",
                table: "ContractInspectionVersion",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "ContractInspection",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "ContractInspection",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Contract",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "Contract",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PluginConfigurationValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation(autoIDGenStrategy, autoIDGenStrategyValue),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    WorkflowState = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: false),
                    UpdatedByUserId = table.Column<int>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PluginConfigurationValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PluginConfigurationValueVersions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation(autoIDGenStrategy, autoIDGenStrategyValue),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    WorkflowState = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: false),
                    UpdatedByUserId = table.Column<int>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PluginConfigurationValueVersions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PluginConfigurationValues");

            migrationBuilder.DropTable(
                name: "PluginConfigurationValueVersions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "RentableItemsVersion");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "RentableItemsVersion");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "RentableItemsVersion");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "RentableItemsVersion");

            migrationBuilder.DropColumn(
                name: "WorkflowState",
                table: "RentableItemsVersion");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "RentableItemsSettingsVersions");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "RentableItemsSettingsVersions");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "RentableItemsSettingsVersions");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "RentableItemsSettingsVersions");

            migrationBuilder.DropColumn(
                name: "WorkflowState",
                table: "RentableItemsSettingsVersions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "RentableItemsSettings");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "RentableItemsSettings");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "RentableItemsSettings");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "RentableItemsSettings");

            migrationBuilder.DropColumn(
                name: "WorkflowState",
                table: "RentableItemsSettings");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "RentableItemsFields");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "RentableItemsFields");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "RentableItemsFields");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "RentableItemsFields");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "RentableItemsFields");

            migrationBuilder.DropColumn(
                name: "WorkflowState",
                table: "RentableItemsFields");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "RentableItemsContractVersions");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "RentableItemsContractVersions");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "RentableItemsContractVersions");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "RentableItemsContractVersions");

            migrationBuilder.DropColumn(
                name: "WorkflowState",
                table: "RentableItemsContractVersions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "RentableItemContract");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "RentableItemContract");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "RentableItemContract");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "RentableItemContract");

            migrationBuilder.DropColumn(
                name: "WorkflowState",
                table: "RentableItemContract");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "RentableItem");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "RentableItem");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "RentableItem");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "RentableItem");

            migrationBuilder.DropColumn(
                name: "WorkflowState",
                table: "RentableItem");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "WorkflowState",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "ContractVersions");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "ContractVersions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ContractInspectionVersion");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "ContractInspectionVersion");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ContractInspectionVersion");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "ContractInspectionVersion");

            migrationBuilder.DropColumn(
                name: "WorkflowState",
                table: "ContractInspectionVersion");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "ContractInspection");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "ContractInspection");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Contract");
        }
    }
}
