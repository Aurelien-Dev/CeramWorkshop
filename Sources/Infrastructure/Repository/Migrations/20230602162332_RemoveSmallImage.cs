using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSmallImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlSmall",
                table: "ImageInstruction");

            migrationBuilder.RenameColumn(
                name: "UrlLarge",
                table: "ImageInstruction",
                newName: "Url");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "ImageInstruction",
                newName: "UrlLarge");

            migrationBuilder.AddColumn<string>(
                name: "UrlSmall",
                table: "ImageInstruction",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
