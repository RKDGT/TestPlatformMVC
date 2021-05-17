using Microsoft.EntityFrameworkCore.Migrations;

namespace TestPlatform.DAL.Migrations
{
    public partial class fixRElationAndImgPathPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoursesInfo_Courses_CourseId",
                table: "CoursesInfo");

            migrationBuilder.DropIndex(
                name: "IX_CoursesInfo_CourseId",
                table: "CoursesInfo");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "CoursesInfo");

            migrationBuilder.AddColumn<string>(
                name: "MainCourseImgPath",
                table: "CoursesInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CourseInfoId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseInfoId",
                table: "Courses",
                column: "CourseInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CoursesInfo_CourseInfoId",
                table: "Courses",
                column: "CourseInfoId",
                principalTable: "CoursesInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CoursesInfo_CourseInfoId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CourseInfoId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "MainCourseImgPath",
                table: "CoursesInfo");

            migrationBuilder.DropColumn(
                name: "CourseInfoId",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "CoursesInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CoursesInfo_CourseId",
                table: "CoursesInfo",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesInfo_Courses_CourseId",
                table: "CoursesInfo",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
