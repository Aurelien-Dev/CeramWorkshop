using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Repository.Migrations
{
    public partial class UpdateFiringPrimaryKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductFirings",
                table: "ProductFirings");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProductFirings",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductFirings",
                table: "ProductFirings",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFirings_IdProduct",
                table: "ProductFirings",
                column: "IdProduct");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductFirings",
                table: "ProductFirings");

            migrationBuilder.DropIndex(
                name: "IX_ProductFirings_IdProduct",
                table: "ProductFirings");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProductFirings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductFirings",
                table: "ProductFirings",
                columns: new[] { "IdProduct", "IdFiring" });
        }
    }
}
