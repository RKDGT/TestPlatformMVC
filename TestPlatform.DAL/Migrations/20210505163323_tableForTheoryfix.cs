using Microsoft.EntityFrameworkCore.Migrations;

namespace TestPlatform.DAL.Migrations
{
    public partial class tableForTheoryfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TheoryFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    filePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TheoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheoryFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TheoryFiles_CoursesInfoTheories_TheoryId",
                        column: x => x.TheoryId,
                        principalTable: "CoursesInfoTheories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TheoryFiles_TheoryId",
                table: "TheoryFiles",
                column: "TheoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TheoryFiles");
        }
    }
}
