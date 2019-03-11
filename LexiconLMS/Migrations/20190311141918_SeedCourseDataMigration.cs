using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LexiconLMS.Migrations
{
    public partial class SeedCourseDataMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ActivityType",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "E-Learning" },
                    { 2, "Lectures" },
                    { 3, "Exercise" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Description", "EndDate", "Name", "StartDate" },
                values: new object[] { -1, "Utbildningen mot programmerare och systemutvecklare syftar till att skapa förutsättningar att ut-veckla kunskaper och färdigheter i programmering och att utveckla IT-system, applikationer eller delar av system. Utbildningen syftar till att inom valt språk täcka systemutveckling, frontend, backend, fullstack samt mobil applikationsutveckling.", new DateTime(2019, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Programmerare och systemutvecklare Inriktning Microsoft .NET", new DateTime(2018, 11, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Modules",
                columns: new[] { "Id", "CourseId", "Description", "EndDate", "Name", "StartDate" },
                values: new object[,]
                {
                    { -1, -1, "Lorem ipsum dolor sit amet", new DateTime(2018, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Programmering", new DateTime(2018, 11, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -2, -1, "Cras ut euismod enim", new DateTime(2019, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Avancerad Programmering", new DateTime(2018, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -3, -1, "Ut a lobortis eros, at blandit metu", new DateTime(2019, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Databas", new DateTime(2019, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -4, -1, "Vestibulum pharetra ultrices pulvinar", new DateTime(2019, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "FrontEnd", new DateTime(2019, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -5, -1, "Fusce semper, tortor ac condimentum", new DateTime(2019, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BackEnd", new DateTime(2019, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -6, -1, "Vestibulum sit amet magna turpis", new DateTime(2019, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Applikationsutveckling", new DateTime(2019, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -7, -1, "Nunc libero quam, varius id mattis ut", new DateTime(2019, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Testning av mjukvara", new DateTime(2019, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Activities",
                columns: new[] { "Id", "ActivityTypeId", "Description", "EndDate", "ModuleId", "StartDate" },
                values: new object[,]
                {
                    { -1, 1, "Mauris venenatis", new DateTime(2018, 11, 26, 12, 15, 0, 0, DateTimeKind.Unspecified), -1, new DateTime(2018, 11, 26, 8, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -22, 2, "Nunc tempus finibus mollis", new DateTime(2019, 3, 10, 11, 0, 0, 0, DateTimeKind.Unspecified), -6, new DateTime(2019, 3, 10, 8, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -21, 2, "Mauris venenatis", new DateTime(2019, 3, 9, 10, 15, 0, 0, DateTimeKind.Unspecified), -6, new DateTime(2019, 3, 9, 8, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -20, 1, "Nunc tempus finibus mollis", new DateTime(2019, 2, 24, 11, 0, 0, 0, DateTimeKind.Unspecified), -5, new DateTime(2019, 2, 24, 8, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -19, 3, "Mauris venenatis", new DateTime(2019, 2, 23, 10, 15, 0, 0, DateTimeKind.Unspecified), -5, new DateTime(2019, 2, 23, 8, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -18, 1, "Nunc tempus finibus mollis", new DateTime(2019, 2, 10, 11, 0, 0, 0, DateTimeKind.Unspecified), -4, new DateTime(2019, 2, 11, 8, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -17, 2, "Mauris venenatis", new DateTime(2019, 2, 8, 10, 15, 0, 0, DateTimeKind.Unspecified), -4, new DateTime(2019, 2, 10, 8, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -16, 1, "Nunc tempus finibus mollis", new DateTime(2019, 1, 10, 11, 0, 0, 0, DateTimeKind.Unspecified), -3, new DateTime(2019, 1, 11, 8, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -15, 3, "Mauris venenatis", new DateTime(2019, 1, 8, 10, 15, 0, 0, DateTimeKind.Unspecified), -3, new DateTime(2019, 1, 10, 8, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -14, 2, "Nunc tempus finibus mollis", new DateTime(2018, 12, 10, 16, 0, 0, 0, DateTimeKind.Unspecified), -2, new DateTime(2018, 12, 10, 8, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -13, 1, "Mauris venenatis", new DateTime(2018, 12, 8, 17, 15, 0, 0, DateTimeKind.Unspecified), -2, new DateTime(2018, 12, 8, 8, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -12, 3, "Nunc tempus finibus mollis", new DateTime(2018, 12, 5, 16, 0, 0, 0, DateTimeKind.Unspecified), -1, new DateTime(2018, 12, 5, 8, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -11, 2, "Mauris venenatis", new DateTime(2018, 12, 4, 17, 15, 0, 0, DateTimeKind.Unspecified), -1, new DateTime(2018, 12, 4, 8, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -10, 1, "Nunc tempus finibus mollis", new DateTime(2018, 12, 3, 15, 30, 0, 0, DateTimeKind.Unspecified), -1, new DateTime(2018, 12, 3, 8, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -9, 3, "Mauris venenatis", new DateTime(2018, 12, 2, 17, 0, 0, 0, DateTimeKind.Unspecified), -1, new DateTime(2018, 12, 2, 8, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -8, 2, "Nunc tempus finibus mollis", new DateTime(2018, 12, 1, 17, 0, 0, 0, DateTimeKind.Unspecified), -1, new DateTime(2018, 12, 1, 13, 15, 0, 0, DateTimeKind.Unspecified) },
                    { -7, 1, "Mauris venenatis", new DateTime(2018, 12, 26, 12, 15, 0, 0, DateTimeKind.Unspecified), -1, new DateTime(2018, 12, 1, 8, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -6, 2, "Nunc tempus finibus mollis", new DateTime(2018, 11, 30, 17, 0, 0, 0, DateTimeKind.Unspecified), -1, new DateTime(2018, 11, 30, 8, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -5, 2, "Mauris venenatis", new DateTime(2018, 11, 29, 17, 0, 0, 0, DateTimeKind.Unspecified), -1, new DateTime(2018, 11, 29, 8, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -4, 1, "Nunc tempus finibus mollis", new DateTime(2018, 11, 28, 17, 0, 0, 0, DateTimeKind.Unspecified), -1, new DateTime(2018, 11, 28, 8, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -3, 3, "Mauris venenatis", new DateTime(2018, 11, 27, 17, 0, 0, 0, DateTimeKind.Unspecified), -1, new DateTime(2018, 11, 27, 8, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -2, 2, "Nunc tempus finibus mollis", new DateTime(2018, 11, 26, 17, 0, 0, 0, DateTimeKind.Unspecified), -1, new DateTime(2018, 11, 26, 13, 15, 0, 0, DateTimeKind.Unspecified) },
                    { -23, 1, "Mauris venenatis", new DateTime(2019, 3, 17, 10, 15, 0, 0, DateTimeKind.Unspecified), -7, new DateTime(2019, 3, 17, 8, 0, 0, 0, DateTimeKind.Unspecified) },
                    { -24, 2, "Nunc tempus finibus mollis", new DateTime(2019, 3, 18, 11, 0, 0, 0, DateTimeKind.Unspecified), -7, new DateTime(2019, 3, 18, 8, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: -24);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: -23);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: -22);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: -21);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: -20);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: -19);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: -18);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: -17);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: -16);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: -15);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: -14);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: -13);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: -12);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: -11);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: -10);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: -9);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: -8);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: -7);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: -6);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: -5);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "ActivityType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ActivityType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ActivityType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: -7);

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: -6);

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: -5);

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: -1);
        }
    }
}
