using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsumerPortal.Api.Migrations
{
    public partial class SeedDemoUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FullName", "PasswordHash", "RoleId" },
                values: new object[] { new Guid("20000000-0000-0000-0000-000000000001"), new DateTimeOffset(new DateTime(2026, 7, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "demo@consumer-portal.local", "Демонстрационный пользователь", "NOT_USED_UNTIL_WEEK4", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"));
        }
    }
}
