using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Repository.Migrations
{
    public partial class AddAccessories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CostKwH",
                table: "ProductFiring",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalKwH",
                table: "ProductFiring",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Accessory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Cost = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accessory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductAccessory",
                columns: table => new
                {
                    IdAccessory = table.Column<int>(type: "integer", nullable: false),
                    IdProduct = table.Column<int>(type: "integer", nullable: false),
                    Cost = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAccessory", x => new { x.IdProduct, x.IdAccessory });
                    table.ForeignKey(
                        name: "FK_ProductAccessory_Accessory_IdAccessory",
                        column: x => x.IdAccessory,
                        principalTable: "Accessory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductAccessory_Products_IdProduct",
                        column: x => x.IdProduct,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAccessory_IdAccessory",
                table: "ProductAccessory",
                column: "IdAccessory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductAccessory");

            migrationBuilder.DropTable(
                name: "Accessory");

            migrationBuilder.DropColumn(
                name: "CostKwH",
                table: "ProductFiring");

            migrationBuilder.DropColumn(
                name: "TotalKwH",
                table: "ProductFiring");
        }
    }
}
