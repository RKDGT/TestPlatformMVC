using Microsoft.EntityFrameworkCore.Migrations;

namespace TestPlatform.DAL.Migrations
{
    public partial class fixTestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CoursesInfoTheoriesTests",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "CoursesInfoTheoriesTests");
        }
    }
}
