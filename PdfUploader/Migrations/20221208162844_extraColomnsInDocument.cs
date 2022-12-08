using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PdfUploader.Migrations
{
    public partial class extraColomnsInDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Documents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DocumentPosition",
                table: "Documents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Dowloads",
                table: "Documents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Documents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Views",
                table: "Documents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "DocumentPosition",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Dowloads",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Views",
                table: "Documents");
        }
    }
}
