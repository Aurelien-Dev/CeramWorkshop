using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Repository.Migrations
{
    public partial class image : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAccessory_Accessory_IdAccessory",
                table: "ProductAccessory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAccessory_Products_IdProduct",
                table: "ProductAccessory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductFiring_Firing_IdFiring",
                table: "ProductFiring");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductFiring_Products_IdProduct",
                table: "ProductFiring");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductFiring",
                table: "ProductFiring");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductAccessory",
                table: "ProductAccessory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Firing",
                table: "Firing");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accessory",
                table: "Accessory");

            migrationBuilder.RenameTable(
                name: "ProductFiring",
                newName: "ProductFirings");

            migrationBuilder.RenameTable(
                name: "ProductAccessory",
                newName: "ProductAccessories");

            migrationBuilder.RenameTable(
                name: "Firing",
                newName: "Firings");

            migrationBuilder.RenameTable(
                name: "Accessory",
                newName: "Accessories");

            migrationBuilder.RenameIndex(
                name: "IX_ProductFiring_IdFiring",
                table: "ProductFirings",
                newName: "IX_ProductFirings_IdFiring");

            migrationBuilder.RenameIndex(
                name: "IX_ProductAccessory_IdAccessory",
                table: "ProductAccessories",
                newName: "IX_ProductAccessories_IdAccessory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductFirings",
                table: "ProductFirings",
                columns: new[] { "IdProduct", "IdFiring" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductAccessories",
                table: "ProductAccessories",
                columns: new[] { "IdProduct", "IdAccessory" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Firings",
                table: "Firings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accessories",
                table: "Accessories",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ImageInstruction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdProduct = table.Column<int>(type: "integer", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    ThumbUrl = table.Column<string>(type: "text", nullable: false),
                    MediumUrl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageInstruction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageInstruction_Products_IdProduct",
                        column: x => x.IdProduct,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImageInstruction_IdProduct",
                table: "ImageInstruction",
                column: "IdProduct");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAccessories_Accessories_IdAccessory",
                table: "ProductAccessories",
                column: "IdAccessory",
                principalTable: "Accessories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAccessories_Products_IdProduct",
                table: "ProductAccessories",
                column: "IdProduct",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFirings_Firings_IdFiring",
                table: "ProductFirings",
                column: "IdFiring",
                principalTable: "Firings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFirings_Products_IdProduct",
                table: "ProductFirings",
                column: "IdProduct",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAccessories_Accessories_IdAccessory",
                table: "ProductAccessories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAccessories_Products_IdProduct",
                table: "ProductAccessories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductFirings_Firings_IdFiring",
                table: "ProductFirings");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductFirings_Products_IdProduct",
                table: "ProductFirings");

            migrationBuilder.DropTable(
                name: "ImageInstruction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductFirings",
                table: "ProductFirings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductAccessories",
                table: "ProductAccessories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Firings",
                table: "Firings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accessories",
                table: "Accessories");

            migrationBuilder.RenameTable(
                name: "ProductFirings",
                newName: "ProductFiring");

            migrationBuilder.RenameTable(
                name: "ProductAccessories",
                newName: "ProductAccessory");

            migrationBuilder.RenameTable(
                name: "Firings",
                newName: "Firing");

            migrationBuilder.RenameTable(
                name: "Accessories",
                newName: "Accessory");

            migrationBuilder.RenameIndex(
                name: "IX_ProductFirings_IdFiring",
                table: "ProductFiring",
                newName: "IX_ProductFiring_IdFiring");

            migrationBuilder.RenameIndex(
                name: "IX_ProductAccessories_IdAccessory",
                table: "ProductAccessory",
                newName: "IX_ProductAccessory_IdAccessory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductFiring",
                table: "ProductFiring",
                columns: new[] { "IdProduct", "IdFiring" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductAccessory",
                table: "ProductAccessory",
                columns: new[] { "IdProduct", "IdAccessory" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Firing",
                table: "Firing",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accessory",
                table: "Accessory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAccessory_Accessory_IdAccessory",
                table: "ProductAccessory",
                column: "IdAccessory",
                principalTable: "Accessory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAccessory_Products_IdProduct",
                table: "ProductAccessory",
                column: "IdProduct",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFiring_Firing_IdFiring",
                table: "ProductFiring",
                column: "IdFiring",
                principalTable: "Firing",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFiring_Products_IdProduct",
                table: "ProductFiring",
                column: "IdProduct",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
