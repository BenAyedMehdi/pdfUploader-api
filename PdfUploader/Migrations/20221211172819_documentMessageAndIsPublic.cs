using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PdfUploader.Migrations
{
    public partial class documentMessageAndIsPublic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Documents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "Documents");
        }
    }
}
