using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OGCP.Curriculum.API.Migrations
{
    /// <inheritdoc />
    public partial class correctInheritance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Responsibilities",
                table: "Internship",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Position",
                table: "ExtracurricularActivity",
                newName: "Role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Internship",
                newName: "Responsibilities");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "ExtracurricularActivity",
                newName: "Position");
        }
    }
}
