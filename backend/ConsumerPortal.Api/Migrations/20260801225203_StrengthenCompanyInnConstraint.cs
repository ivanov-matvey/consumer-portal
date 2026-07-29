using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsumerPortal.Api.Migrations
{
    public partial class StrengthenCompanyInnConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Companies_Inn_Lenght",
                table: "Companies");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Companies_Inn_Lenght",
                table: "Companies",
                sql: "LEN([Inn]) IN (10, 12) AND [Inn] NOT LIKE '%[^0-9]%'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Companies_Inn_Lenght",
                table: "Companies");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Companies_Inn_Lenght",
                table: "Companies",
                sql: "LEN([Inn]) IN (10, 12)");
        }
    }
}
