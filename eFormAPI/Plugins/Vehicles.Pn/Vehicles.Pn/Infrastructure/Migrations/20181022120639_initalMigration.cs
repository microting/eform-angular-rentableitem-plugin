using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RentableItems.Pn.Migrations
{
    public partial class initalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                name: "RentableItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id = table.Column<int>(nullable: false),
                    created_at = table.Column<DateTime>(nullable: true),
                    updated_at = table.Column<DateTime>(nullable: true),
                    workflow_state = table.Column<string>(maxLength: 255, nullable: true),
                    version = table.Column<int>(nullable: false),
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
                    table.PrimaryKey("PK_RentableItems", x => x.Id);
                    table.UniqueConstraint("AK_RentableItems_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ContractInspections",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                    table.PrimaryKey("PK_ContractInspections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractInspections_Contract_ContractId",
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
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id = table.Column<int>(nullable: false),
                    created_at = table.Column<DateTime>(nullable: true),
                    updated_at = table.Column<DateTime>(nullable: true),
                    workflow_state = table.Column<string>(maxLength: 255, nullable: true),
                    version = table.Column<int>(nullable: false),
                    Created_By_User_Id = table.Column<int>(nullable: false),
                    Updated_By_User_Id = table.Column<int>(nullable: false),
                    RentableItemId = table.Column<int>(nullable: false),
                    ContractId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentableItemContract", x => x.Id);
                    table.UniqueConstraint("AK_RentableItemContract_id", x => x.id);
                    table.ForeignKey(
                        name: "FK_RentableItemContract_RentableItems_RentableItemId",
                        column: x => x.RentableItemId,
                        principalTable: "RentableItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractInspections_ContractId",
                table: "ContractInspections",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_RentableItemContract_RentableItemId",
                table: "RentableItemContract",
                column: "RentableItemId");

            migrationBuilder.CreateIndex(
                name: "IX_RentableItems_VinNumber",
                table: "RentableItems",
                column: "VinNumber",
                unique: true,
                filter: "[VinNumber] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractInspections");

            migrationBuilder.DropTable(
                name: "RentableItemContract");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "RentableItems");
        }
    }
}
