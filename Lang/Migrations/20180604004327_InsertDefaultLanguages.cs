using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lang.Migrations
{
    public partial class InsertDefaultLanguages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActivityStatus",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "HeartBeat",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Icon", "IsCommon", "Name" },
                values: new object[,]
                {
                    { "en", "united-states-of-america.svg", true, "English" },
                    { "eo", "esperanto.svg", false, "Experanto" },
                    { "es", "mexico.svg", true, "Spanish" },
                    { "zh", "china.svg", true, "Chinese" },
                    { "ar", "egypt.svg", true, "Arabic" },
                    { "fr", "france.svg", true, "French" },
                    { "de", "germany.svg", true, "German" },
                    { "it", "italy.svg", true, "Italian" },
                    { "ja", "japan.svg", true, "Japanese" },
                    { "pt", "brazil.svg", true, "Portuguese" },
                    { "ru", "russia.svg", true, "Russian" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: "ar");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: "de");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: "en");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: "eo");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: "es");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: "fr");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: "it");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: "ja");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: "pt");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: "ru");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: "zh");

            migrationBuilder.DropColumn(
                name: "ActivityStatus",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HeartBeat",
                table: "AspNetUsers");
        }
    }
}
