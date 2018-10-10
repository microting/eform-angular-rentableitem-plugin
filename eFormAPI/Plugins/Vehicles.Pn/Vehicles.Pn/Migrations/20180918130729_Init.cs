using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vehicles.Pn.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContractNumber = table.Column<string>(maxLength: 100, nullable: true),
                    CustomerName = table.Column<string>(maxLength: 250, nullable: true),
                    Brand = table.Column<string>(maxLength: 100, nullable: true),
                    ModelName = table.Column<string>(maxLength: 100, nullable: true),
                    RegistrationDate = table.Column<DateTime>(nullable: false),
                    VinNumber = table.Column<string>(maxLength: 17, nullable: true),
                    ContractStartDate = table.Column<DateTime>(nullable: false),
                    ContractEndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VinNumber",
                table: "Vehicles",
                column: "VinNumber",
                unique: true,
                filter: "[VinNumber] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
