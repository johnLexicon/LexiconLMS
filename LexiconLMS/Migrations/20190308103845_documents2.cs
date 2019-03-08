using Microsoft.EntityFrameworkCore.Migrations;

namespace LexiconLMS.Migrations
{
    public partial class documents2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseDocument_AspNetUsers_UploadedById",
                table: "CourseDocument");

            migrationBuilder.DropIndex(
                name: "IX_CourseDocument_UploadedById",
                table: "CourseDocument");

            migrationBuilder.RenameColumn(
                name: "UploadedById",
                table: "CourseDocument",
                newName: "UserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CourseDocument",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "CourseDocument",
                newName: "UploadedById");

            migrationBuilder.AlterColumn<string>(
                name: "UploadedById",
                table: "CourseDocument",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseDocument_UploadedById",
                table: "CourseDocument",
                column: "UploadedById");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseDocument_AspNetUsers_UploadedById",
                table: "CourseDocument",
                column: "UploadedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
