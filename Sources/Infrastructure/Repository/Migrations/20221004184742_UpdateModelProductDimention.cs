using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class UpdateModelProductDimention : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TopDiameterFinish",
                table: "Products",
                newName: "TopDiameter");

            migrationBuilder.RenameColumn(
                name: "HeightFinish",
                table: "Products",
                newName: "ShrinkingCoeficient");

            migrationBuilder.RenameColumn(
                name: "BottomDiameterFinish",
                table: "Products",
                newName: "Height");

            migrationBuilder.AddColumn<double>(
                name: "BottomDiameter",
                table: "Products",
                type: "double precision",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Quantity",
                table: "Materials",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Cost",
                table: "Materials",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BottomDiameter",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "TopDiameter",
                table: "Products",
                newName: "TopDiameterFinish");

            migrationBuilder.RenameColumn(
                name: "ShrinkingCoeficient",
                table: "Products",
                newName: "HeightFinish");

            migrationBuilder.RenameColumn(
                name: "Height",
                table: "Products",
                newName: "BottomDiameterFinish");

            migrationBuilder.AlterColumn<double>(
                name: "Quantity",
                table: "Materials",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<double>(
                name: "Cost",
                table: "Materials",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");
        }
    }
}
