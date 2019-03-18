using Microsoft.EntityFrameworkCore.Migrations;

namespace LexiconLMS.Migrations
{
    public partial class teacherPage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ModuleDocument",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CourseDocument",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ActivityDocument",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModuleDocument_UserId",
                table: "ModuleDocument",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseDocument_UserId",
                table: "CourseDocument",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityDocument_UserId",
                table: "ActivityDocument",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityDocument_AspNetUsers_UserId",
                table: "ActivityDocument",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseDocument_AspNetUsers_UserId",
                table: "CourseDocument",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleDocument_AspNetUsers_UserId",
                table: "ModuleDocument",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityDocument_AspNetUsers_UserId",
                table: "ActivityDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseDocument_AspNetUsers_UserId",
                table: "CourseDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_ModuleDocument_AspNetUsers_UserId",
                table: "ModuleDocument");

            migrationBuilder.DropIndex(
                name: "IX_ModuleDocument_UserId",
                table: "ModuleDocument");

            migrationBuilder.DropIndex(
                name: "IX_CourseDocument_UserId",
                table: "CourseDocument");

            migrationBuilder.DropIndex(
                name: "IX_ActivityDocument_UserId",
                table: "ActivityDocument");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ModuleDocument",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CourseDocument",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ActivityDocument",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
