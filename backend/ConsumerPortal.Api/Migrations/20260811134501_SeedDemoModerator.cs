using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsumerPortal.Api.Migrations
{
    public partial class SeedDemoModerator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FullName", "PasswordHash", "RoleId" },
                values: new object[] { new Guid("20000000-0000-0000-0000-000000000002"), new DateTimeOffset(new DateTime(2026, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "moderator@consumer-portal.local", "Демонстрационный модератор", "$2a$11$9wdMc/10SfSqXv.HQ8vYN.av0mwDgluA8uQ82Jg7pSlFZBb9NGvFW", 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"));
        }
    }
}
