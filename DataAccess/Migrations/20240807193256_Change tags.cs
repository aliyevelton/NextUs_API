using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class Changetags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseCourseTag");

            migrationBuilder.DropTable(
                name: "CourseTagTag");

            migrationBuilder.DropTable(
                name: "JobJobTag");

            migrationBuilder.DropTable(
                name: "JobTagTag");

            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "JobTags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "JobTags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "CourseTags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "CourseTags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_JobTags_JobId",
                table: "JobTags",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTags_TagId",
                table: "JobTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTags_CourseId",
                table: "CourseTags",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTags_TagId",
                table: "CourseTags",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTags_Courses_CourseId",
                table: "CourseTags",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTags_Tags_TagId",
                table: "CourseTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobTags_Jobs_JobId",
                table: "JobTags",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobTags_Tags_TagId",
                table: "JobTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseTags_Courses_CourseId",
                table: "CourseTags");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseTags_Tags_TagId",
                table: "CourseTags");

            migrationBuilder.DropForeignKey(
                name: "FK_JobTags_Jobs_JobId",
                table: "JobTags");

            migrationBuilder.DropForeignKey(
                name: "FK_JobTags_Tags_TagId",
                table: "JobTags");

            migrationBuilder.DropIndex(
                name: "IX_JobTags_JobId",
                table: "JobTags");

            migrationBuilder.DropIndex(
                name: "IX_JobTags_TagId",
                table: "JobTags");

            migrationBuilder.DropIndex(
                name: "IX_CourseTags_CourseId",
                table: "CourseTags");

            migrationBuilder.DropIndex(
                name: "IX_CourseTags_TagId",
                table: "CourseTags");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "JobTags");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "JobTags");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "CourseTags");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "CourseTags");

            migrationBuilder.CreateTable(
                name: "CourseCourseTag",
                columns: table => new
                {
                    CoursesId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCourseTag", x => new { x.CoursesId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_CourseCourseTag_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseCourseTag_CourseTags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "CourseTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseTagTag",
                columns: table => new
                {
                    CoursesId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTagTag", x => new { x.CoursesId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_CourseTagTag_CourseTags_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "CourseTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseTagTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobJobTag",
                columns: table => new
                {
                    JobsId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobJobTag", x => new { x.JobsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_JobJobTag_Jobs_JobsId",
                        column: x => x.JobsId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobJobTag_JobTags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "JobTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobTagTag",
                columns: table => new
                {
                    JobsId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTagTag", x => new { x.JobsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_JobTagTag_JobTags_JobsId",
                        column: x => x.JobsId,
                        principalTable: "JobTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobTagTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseCourseTag_TagsId",
                table: "CourseCourseTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTagTag_TagsId",
                table: "CourseTagTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_JobJobTag_TagsId",
                table: "JobJobTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTagTag_TagsId",
                table: "JobTagTag",
                column: "TagsId");
        }
    }
}
