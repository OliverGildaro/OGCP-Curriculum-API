using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OGCP.Curriculums.Writes.ProfileRepository.Migrations
{
    /// <inheritdoc />
    public partial class addingIndex1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Profiles_FirstName_LastName",
                table: "Profiles",
                column: "FirstName");

            migrationBuilder.CreateIndex(
                name: "IX_Educations_Degree_Institution_NC",
                table: "Educations",
                columns: new[] { "Degree", "Institution" })
                .Annotation("SqlServer:Include", new[] { "StartDate", "EndDate", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_Educations_ProjectTitle_Institution_NC",
                table: "Educations",
                columns: new[] { "ProjectTitle", "Institution" })
                .Annotation("SqlServer:Include", new[] { "StartDate", "EndDate", "Supervisor" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Profiles_FirstName_LastName",
                table: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_Educations_Degree_Institution_NC",
                table: "Educations");

            migrationBuilder.DropIndex(
                name: "IX_Educations_ProjectTitle_Institution_NC",
                table: "Educations");
        }
    }
}
