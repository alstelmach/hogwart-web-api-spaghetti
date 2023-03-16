using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Hogwart.Api.src.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreatedHousesAndStudentsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Houses",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Houses", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "StudentDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Nationality = table.Column<string>(type: "text", nullable: true),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    HeightInCentimeters = table.Column<float>(type: "real", nullable: false),
                    DoesSpeakParseltongue = table.Column<bool>(type: "boolean", nullable: false),
                    DoesPlayQuidditch = table.Column<bool>(type: "boolean", nullable: false),
                    IsAmbitious = table.Column<bool>(type: "boolean", nullable: false),
                    HouseDtoName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentDto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentDto_Houses_HouseDtoName",
                        column: x => x.HouseDtoName,
                        principalTable: "Houses",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentDto_HouseDtoName",
                table: "StudentDto",
                column: "HouseDtoName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentDto");

            migrationBuilder.DropTable(
                name: "Houses");
        }
    }
}
