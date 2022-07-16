using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class ProductDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TopDiameter",
                table: "Products",
                newName: "TopDiameterFinish");

            migrationBuilder.RenameColumn(
                name: "Height",
                table: "Products",
                newName: "HeightFinish");

            migrationBuilder.RenameColumn(
                name: "BottomDiameter",
                table: "Products",
                newName: "BottomDiameterFinish");

            migrationBuilder.AddColumn<string>(
                name: "GlazingInstruction",
                table: "Products",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GlazingInstruction",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "TopDiameterFinish",
                table: "Products",
                newName: "TopDiameter");

            migrationBuilder.RenameColumn(
                name: "HeightFinish",
                table: "Products",
                newName: "Height");

            migrationBuilder.RenameColumn(
                name: "BottomDiameterFinish",
                table: "Products",
                newName: "BottomDiameter");
        }
    }
}
