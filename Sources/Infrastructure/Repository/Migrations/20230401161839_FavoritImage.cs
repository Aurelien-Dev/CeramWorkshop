using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class FavoritImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkshopParameters_Workshops_WorksĥopId",
                table: "WorkshopParameters");

            migrationBuilder.DropColumn(
                name: "IdWorkshop",
                table: "WorkshopParameters");

            migrationBuilder.RenameColumn(
                name: "WorksĥopId",
                table: "WorkshopParameters",
                newName: "WorkshopId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkshopParameters_WorksĥopId",
                table: "WorkshopParameters",
                newName: "IX_WorkshopParameters_WorkshopId");

            migrationBuilder.AddColumn<bool>(
                name: "IsFavoriteImage",
                table: "ImageInstruction",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkshopParameters_Workshops_WorkshopId",
                table: "WorkshopParameters",
                column: "WorkshopId",
                principalTable: "Workshops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkshopParameters_Workshops_WorkshopId",
                table: "WorkshopParameters");

            migrationBuilder.DropColumn(
                name: "IsFavoriteImage",
                table: "ImageInstruction");

            migrationBuilder.RenameColumn(
                name: "WorkshopId",
                table: "WorkshopParameters",
                newName: "WorksĥopId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkshopParameters_WorkshopId",
                table: "WorkshopParameters",
                newName: "IX_WorkshopParameters_WorksĥopId");

            migrationBuilder.AddColumn<int>(
                name: "IdWorkshop",
                table: "WorkshopParameters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkshopParameters_Workshops_WorksĥopId",
                table: "WorkshopParameters",
                column: "WorksĥopId",
                principalTable: "Workshops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
