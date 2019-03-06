using Microsoft.EntityFrameworkCore.Migrations;

namespace LexiconLMS.Migrations
{
    public partial class removeActivitiesFromActivityType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activityy_ActivityType_ActivityTypeId",
                table: "Activityy");

            migrationBuilder.DropForeignKey(
                name: "FK_Activityy_Modules_ModuleId",
                table: "Activityy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Activityy",
                table: "Activityy");

            migrationBuilder.RenameTable(
                name: "Activityy",
                newName: "Activities");

            migrationBuilder.RenameIndex(
                name: "IX_Activityy_ModuleId",
                table: "Activities",
                newName: "IX_Activities_ModuleId");

            migrationBuilder.RenameIndex(
                name: "IX_Activityy_ActivityTypeId",
                table: "Activities",
                newName: "IX_Activities_ActivityTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Activities",
                table: "Activities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_ActivityType_ActivityTypeId",
                table: "Activities",
                column: "ActivityTypeId",
                principalTable: "ActivityType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Modules_ModuleId",
                table: "Activities",
                column: "ModuleId",
                principalTable: "Modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_ActivityType_ActivityTypeId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Modules_ModuleId",
                table: "Activities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Activities",
                table: "Activities");

            migrationBuilder.RenameTable(
                name: "Activities",
                newName: "Activityy");

            migrationBuilder.RenameIndex(
                name: "IX_Activities_ModuleId",
                table: "Activityy",
                newName: "IX_Activityy_ModuleId");

            migrationBuilder.RenameIndex(
                name: "IX_Activities_ActivityTypeId",
                table: "Activityy",
                newName: "IX_Activityy_ActivityTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Activityy",
                table: "Activityy",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Activityy_ActivityType_ActivityTypeId",
                table: "Activityy",
                column: "ActivityTypeId",
                principalTable: "ActivityType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Activityy_Modules_ModuleId",
                table: "Activityy",
                column: "ModuleId",
                principalTable: "Modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
