using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsumerPortal.Api.Migrations
{
    public partial class SeedDemoUserPasswordHash : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                column: "PasswordHash",
                value: "$2a$11$L6TImdFX/hcvxpnKj2DcW.AsjTQKaOdFtHGQh8DPx7Db/.jWur8LW");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                column: "PasswordHash",
                value: "NOT_USED_UNTIL_WEEK4");
        }
    }
}
