using Microsoft.EntityFrameworkCore.Migrations;

namespace TestPlatform.DAL.Migrations
{
    public partial class summaryFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalInform",
                table: "CoursesInfoTheories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdditionalInform",
                table: "CoursesInfoTheories",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
