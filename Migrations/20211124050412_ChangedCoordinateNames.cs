using Microsoft.EntityFrameworkCore.Migrations;

namespace Tic_Tac_Toe.Migrations
{
    public partial class ChangedCoordinateNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "YCoordinate",
                table: "Moves",
                newName: "RowNumber");

            migrationBuilder.RenameColumn(
                name: "XCoordinate",
                table: "Moves",
                newName: "ColumnNumber");

            migrationBuilder.UpdateData(
                table: "Moves",
                keyColumn: "MoveId",
                keyValue: 2,
                columns: new[] { "ColumnNumber", "RowNumber" },
                values: new object[] { 2, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RowNumber",
                table: "Moves",
                newName: "YCoordinate");

            migrationBuilder.RenameColumn(
                name: "ColumnNumber",
                table: "Moves",
                newName: "XCoordinate");

            migrationBuilder.UpdateData(
                table: "Moves",
                keyColumn: "MoveId",
                keyValue: 2,
                columns: new[] { "XCoordinate", "YCoordinate" },
                values: new object[] { 1, 2 });
        }
    }
}
