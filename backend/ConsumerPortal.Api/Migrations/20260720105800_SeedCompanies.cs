using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsumerPortal.Api.Migrations
{
    public partial class SeedCompanies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Category", "Inn", "Name" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), 1, "7701234567", "Городская управляющая компания" },
                    { new Guid("10000000-0000-0000-0000-000000000002"), 1, "7812345678", "Комфортный дом" },
                    { new Guid("10000000-0000-0000-0000-000000000003"), 2, "770123456789", "Магазин У дома" },
                    { new Guid("10000000-0000-0000-0000-000000000004"), 2, "780123456789", "Торговая сеть Север" },
                    { new Guid("10000000-0000-0000-0000-000000000005"), 3, "7712345678", "Быстрая связь" },
                    { new Guid("10000000-0000-0000-0000-000000000006"), 3, "781234567890", "Телеком Регион" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"));
        }
    }
}
