using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class MaterialAddQuantityAndUnity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Quantity",
                table: "Materials",
                type: "double precision",
                nullable: true,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "UniteMesure",
                table: "Materials",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "UniteMesure",
                table: "Materials");
        }
    }
}
