using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddImageSizing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "ImageInstruction",
                newName: "UrlSmall");

            migrationBuilder.AddColumn<string>(
                name: "UrlLarge",
                table: "ImageInstruction",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UrlMedium",
                table: "ImageInstruction",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlLarge",
                table: "ImageInstruction");

            migrationBuilder.DropColumn(
                name: "UrlMedium",
                table: "ImageInstruction");

            migrationBuilder.RenameColumn(
                name: "UrlSmall",
                table: "ImageInstruction",
                newName: "Url");
        }
    }
}
