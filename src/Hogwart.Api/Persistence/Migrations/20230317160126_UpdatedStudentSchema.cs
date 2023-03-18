using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hogwart.Api.src.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedStudentSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentDto_Houses_HouseDtoName",
                table: "StudentDto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentDto",
                table: "StudentDto");

            migrationBuilder.RenameTable(
                name: "StudentDto",
                newName: "Students");

            migrationBuilder.RenameIndex(
                name: "IX_StudentDto_HouseDtoName",
                table: "Students",
                newName: "IX_Students_HouseDtoName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Houses_HouseDtoName",
                table: "Students",
                column: "HouseDtoName",
                principalTable: "Houses",
                principalColumn: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Houses_HouseDtoName",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "StudentDto");

            migrationBuilder.RenameIndex(
                name: "IX_Students_HouseDtoName",
                table: "StudentDto",
                newName: "IX_StudentDto_HouseDtoName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentDto",
                table: "StudentDto",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentDto_Houses_HouseDtoName",
                table: "StudentDto",
                column: "HouseDtoName",
                principalTable: "Houses",
                principalColumn: "Name");
        }
    }
}
