using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Migrations
{
    /// <inheritdoc />
    public partial class courses5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseStates_StateId",
                table: "Courses");

            migrationBuilder.DropTable(
                name: "CourseStates");

            migrationBuilder.DropIndex(
                name: "IX_Courses_StateId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "Courses");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CourseStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseStates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_StateId",
                table: "Courses",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseStates_StateId",
                table: "Courses",
                column: "StateId",
                principalTable: "CourseStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
