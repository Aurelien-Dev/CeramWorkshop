using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Repository.Migrations
{
    public partial class AddWorkshop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdWorkshop",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Workshop",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Logo = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workshop", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkshopParameter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdWorkshop = table.Column<int>(type: "integer", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    WorksĥopId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkshopParameter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkshopParameter_Workshop_WorksĥopId",
                        column: x => x.WorksĥopId,
                        principalTable: "Workshop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_IdWorkshop",
                table: "Products",
                column: "IdWorkshop");

            migrationBuilder.CreateIndex(
                name: "IX_WorkshopParameter_WorksĥopId",
                table: "WorkshopParameter",
                column: "WorksĥopId");

            migrationBuilder.InsertData("Workshop", new string[] { "Id", "Name", "Logo" }, new string[] { "0", "AtelierCrémazie", "null" });
            migrationBuilder.UpdateData(
                            table: "Products",
                            keyColumn: "IdWorkshop",
                            keyValue: null,
                            column: "IdWorkshop",
                            value: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Workshop_IdWorkshop",
                table: "Products",
                column: "IdWorkshop",
                principalTable: "Workshop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Workshop_IdWorkshop",
                table: "Products");

            migrationBuilder.DropTable(
                name: "WorkshopParameter");

            migrationBuilder.DropTable(
                name: "Workshop");

            migrationBuilder.DropIndex(
                name: "IX_Products_IdWorkshop",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IdWorkshop",
                table: "Products");
        }
    }
}
