using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class DeleteUselessFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediumUrl",
                table: "ImageInstruction");

            migrationBuilder.DropColumn(
                name: "ThumbUrl",
                table: "ImageInstruction");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MediumUrl",
                table: "ImageInstruction",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ThumbUrl",
                table: "ImageInstruction",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
