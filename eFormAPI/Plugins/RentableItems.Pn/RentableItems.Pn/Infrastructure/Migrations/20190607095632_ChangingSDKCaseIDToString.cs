namespace RentableItems.Pn.Infrastructure.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ChangingSDKCaseIDToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SDK_Case_Id",
                table: "ContractInspectionVersion",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "SDK_Case_Id",
                table: "ContractInspection",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SDK_Case_Id",
                table: "ContractInspectionVersion",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SDK_Case_Id",
                table: "ContractInspection",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
