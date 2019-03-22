using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LexiconLMS.Migrations
{
    public partial class doc4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "ActivityDocument",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DocumentData = table.Column<byte[]>(nullable: true),
                    UploadTime = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    ActivityId = table.Column<int>(nullable: false),
                    ActivityyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityDocument_Activities_ActivityyId",
                        column: x => x.ActivityyId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ModuleDocument",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DocumentData = table.Column<byte[]>(nullable: true),
                    UploadTime = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    ModuleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModuleDocument_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityDocument_ActivityyId",
                table: "ActivityDocument",
                column: "ActivityyId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleDocument_ModuleId",
                table: "ModuleDocument",
                column: "ModuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseDocument_Courses_CourseId",
                table: "CourseDocument",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseDocument_Courses_CourseId",
                table: "CourseDocument");

            migrationBuilder.DropTable(
                name: "ActivityDocument");

            migrationBuilder.DropTable(
                name: "ModuleDocument");

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
    }
}
