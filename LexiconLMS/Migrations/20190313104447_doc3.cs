using Microsoft.EntityFrameworkCore.Migrations;

namespace LexiconLMS.Migrations
{
    public partial class doc3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseDocument_Courses_CourseId",
                table: "CourseDocument");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseDocument",
                table: "CourseDocument");

            migrationBuilder.RenameTable(
                name: "CourseDocument",
                newName: "GenericDocument");

            migrationBuilder.RenameIndex(
                name: "IX_CourseDocument_CourseId",
                table: "GenericDocument",
                newName: "IX_GenericDocument_CourseId");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "GenericDocument",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "GenericDocument",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GenericDocument",
                table: "GenericDocument",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GenericDocument_Courses_CourseId",
                table: "GenericDocument",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenericDocument_Courses_CourseId",
                table: "GenericDocument");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GenericDocument",
                table: "GenericDocument");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "GenericDocument");

            migrationBuilder.RenameTable(
                name: "GenericDocument",
                newName: "CourseDocument");

            migrationBuilder.RenameIndex(
                name: "IX_GenericDocument_CourseId",
                table: "CourseDocument",
                newName: "IX_CourseDocument_CourseId");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "CourseDocument",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseDocument",
                table: "CourseDocument",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseDocument_Courses_CourseId",
                table: "CourseDocument",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
