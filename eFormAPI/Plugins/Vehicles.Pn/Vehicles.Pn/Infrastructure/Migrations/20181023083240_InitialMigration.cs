using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RentableItems.Pn.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            string autoIDGenStrategy = "SqlServer:ValueGenerationStrategy";
            object autoIDGenStrategyValue = SqlServerValueGenerationStrategy.IdentityColumn;

            // Setup for MySQL Provider
            if (migrationBuilder.ActiveProvider == "Pomelo.EntityFrameworkCore.MySql")
            {
                DbConfig.IsMySQL = true;
                autoIDGenStrategy = "MySQL:ValueGeneratedOnAdd";
                autoIDGenStrategyValue = true;
            }

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation(autoIDGenStrategy, autoIDGenStrategyValue),
                    WorkflowState = table.Column<string>(maxLength: 255, nullable: true),
                    Version = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    Created_By_User_Id = table.Column<int>(nullable: false),
                    Updated_By_User_Id = table.Column<int>(nullable: false),
                    ContractStart = table.Column<DateTime>(nullable: false),
                    ContractEnd = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    ContractNr = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractInspectionVersion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation(autoIDGenStrategy, autoIDGenStrategyValue),
                    Created_at = table.Column<DateTime>(nullable: true),
                    Updated_at = table.Column<DateTime>(nullable: true),
                    Workflow_state = table.Column<string>(maxLength: 255, nullable: true),
                    Version = table.Column<int>(nullable: false),
                    Created_By_User_Id = table.Column<int>(nullable: false),
                    Updated_By_User_Id = table.Column<int>(nullable: false),
                    DoneAt = table.Column<DateTime>(nullable: true),
                    SDK_Case_Id = table.Column<int>(nullable: false),
                    ContractId = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: true),
                    ContractInspectionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractInspectionVersion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractVersions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation(autoIDGenStrategy, autoIDGenStrategyValue),
                    WorkflowState = table.Column<string>(maxLength: 255, nullable: true),
                    Version = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    Created_By_User_Id = table.Column<int>(nullable: false),
                    Updated_By_User_Id = table.Column<int>(nullable: false),
                    ContractStart = table.Column<DateTime>(nullable: false),
                    ContractEnd = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    ContractNr = table.Column<int>(nullable: false),
                    ContractId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractVersions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RentableItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation(autoIDGenStrategy, autoIDGenStrategyValue),
                    Created_at = table.Column<DateTime>(nullable: true),
                    Updated_at = table.Column<DateTime>(nullable: true),
                    Workflow_state = table.Column<string>(maxLength: 255, nullable: true),
                    Version = table.Column<int>(nullable: false),
                    Created_By_User_Id = table.Column<int>(nullable: false),
                    Updated_By_User_Id = table.Column<int>(nullable: false),
                    Brand = table.Column<string>(maxLength: 100, nullable: true),
                    ModelName = table.Column<string>(maxLength: 100, nullable: true),
                    RegistrationDate = table.Column<DateTime>(nullable: false),
                    VinNumber = table.Column<string>(nullable: true),
                    SerialNumber = table.Column<string>(nullable: true),
                    PlateNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentableItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RentableItemsContractVersions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation(autoIDGenStrategy, autoIDGenStrategyValue),
                    Created_at = table.Column<DateTime>(nullable: true),
                    Updated_at = table.Column<DateTime>(nullable: true),
                    Workflow_state = table.Column<string>(maxLength: 255, nullable: true),
                    Version = table.Column<int>(nullable: false),
                    Created_By_User_Id = table.Column<int>(nullable: false),
                    Updated_By_User_Id = table.Column<int>(nullable: false),
                    RentableItemId = table.Column<int>(nullable: false),
                    ContractId = table.Column<int>(nullable: false),
                    RentableItemContractId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentableItemsContractVersions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RentableItemsSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation(autoIDGenStrategy, autoIDGenStrategyValue),
                    Created_at = table.Column<DateTime>(nullable: true),
                    Updated_at = table.Column<DateTime>(nullable: true),
                    Workflow_state = table.Column<string>(maxLength: 255, nullable: true),
                    Version = table.Column<int>(nullable: false),
                    Created_By_User_Id = table.Column<int>(nullable: false),
                    Updated_By_User_Id = table.Column<int>(nullable: false),
                    Eform_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentableItemsSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RentableItemsSettingsVersions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation(autoIDGenStrategy, autoIDGenStrategyValue),
                    Created_at = table.Column<DateTime>(nullable: true),
                    Updated_at = table.Column<DateTime>(nullable: true),
                    Workflow_state = table.Column<string>(maxLength: 255, nullable: true),
                    Version = table.Column<int>(nullable: false),
                    Created_By_User_Id = table.Column<int>(nullable: false),
                    Updated_By_User_Id = table.Column<int>(nullable: false),
                    Eform_Id = table.Column<int>(nullable: false),
                    RentableItemsSettingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentableItemsSettingsVersions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RentableItemsVersion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation(autoIDGenStrategy, autoIDGenStrategyValue),
                    Created_at = table.Column<DateTime>(nullable: true),
                    Updated_at = table.Column<DateTime>(nullable: true),
                    Workflow_state = table.Column<string>(maxLength: 255, nullable: true),
                    Version = table.Column<int>(nullable: false),
                    Created_By_User_Id = table.Column<int>(nullable: false),
                    Updated_By_User_Id = table.Column<int>(nullable: false),
                    Brand = table.Column<string>(maxLength: 100, nullable: true),
                    ModelName = table.Column<string>(maxLength: 100, nullable: true),
                    RegistrationDate = table.Column<DateTime>(nullable: false),
                    VinNumber = table.Column<string>(nullable: true),
                    SerialNumber = table.Column<string>(nullable: true),
                    PlateNumber = table.Column<string>(nullable: true),
                    RentableItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentableItemsVersion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractInspection",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation(autoIDGenStrategy, autoIDGenStrategyValue),
                    WorkflowState = table.Column<string>(maxLength: 255, nullable: true),
                    Version = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    Created_By_User_Id = table.Column<int>(nullable: false),
                    Updated_By_User_Id = table.Column<int>(nullable: false),
                    DoneAt = table.Column<DateTime>(nullable: true),
                    ContractId = table.Column<int>(nullable: false),
                    SDK_Case_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractInspection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractInspection_Contract_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RentableItemContract",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation(autoIDGenStrategy, autoIDGenStrategyValue),
                    Created_at = table.Column<DateTime>(nullable: true),
                    Updated_at = table.Column<DateTime>(nullable: true),
                    Workflow_state = table.Column<string>(maxLength: 255, nullable: true),
                    Version = table.Column<int>(nullable: false),
                    Created_By_User_Id = table.Column<int>(nullable: false),
                    Updated_By_User_Id = table.Column<int>(nullable: false),
                    RentableItemId = table.Column<int>(nullable: false),
                    ContractId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentableItemContract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentableItemContract_RentableItem_RentableItemId",
                        column: x => x.RentableItemId,
                        principalTable: "RentableItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractInspection_ContractId",
                table: "ContractInspection",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_RentableItemContract_RentableItemId",
                table: "RentableItemContract",
                column: "RentableItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractInspection");

            migrationBuilder.DropTable(
                name: "ContractInspectionVersion");

            migrationBuilder.DropTable(
                name: "ContractVersions");

            migrationBuilder.DropTable(
                name: "RentableItemContract");

            migrationBuilder.DropTable(
                name: "RentableItemsContractVersions");

            migrationBuilder.DropTable(
                name: "RentableItemsSettings");

            migrationBuilder.DropTable(
                name: "RentableItemsSettingsVersions");

            migrationBuilder.DropTable(
                name: "RentableItemsVersion");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "RentableItem");
        }
    }
}
