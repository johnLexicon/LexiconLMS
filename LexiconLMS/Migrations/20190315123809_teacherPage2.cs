using Microsoft.EntityFrameworkCore.Migrations;

namespace LexiconLMS.Migrations
{
    public partial class teacherPage2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityDocument_Activities_ActivityyId",
                table: "ActivityDocument");

            migrationBuilder.DropIndex(
                name: "IX_ActivityDocument_ActivityyId",
                table: "ActivityDocument");

            migrationBuilder.DropColumn(
                name: "ActivityyId",
                table: "ActivityDocument");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityDocument_ActivityId",
                table: "ActivityDocument",
                column: "ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityDocument_Activities_ActivityId",
                table: "ActivityDocument",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityDocument_Activities_ActivityId",
                table: "ActivityDocument");

            migrationBuilder.DropIndex(
                name: "IX_ActivityDocument_ActivityId",
                table: "ActivityDocument");

            migrationBuilder.AddColumn<int>(
                name: "ActivityyId",
                table: "ActivityDocument",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActivityDocument_ActivityyId",
                table: "ActivityDocument",
                column: "ActivityyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityDocument_Activities_ActivityyId",
                table: "ActivityDocument",
                column: "ActivityyId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
