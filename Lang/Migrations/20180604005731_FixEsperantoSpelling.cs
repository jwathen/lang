using Microsoft.EntityFrameworkCore.Migrations;

namespace Lang.Migrations
{
    public partial class FixEsperantoSpelling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: "eo",
                column: "Name",
                value: "Esperanto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: "eo",
                column: "Name",
                value: "Experanto");
        }
    }
}
