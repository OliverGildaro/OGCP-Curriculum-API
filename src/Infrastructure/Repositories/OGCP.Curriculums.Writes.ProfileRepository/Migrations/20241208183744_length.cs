using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OGCP.Curriculums.Writes.ProfileRepository.Migrations
{
    /// <inheritdoc />
    public partial class length : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Supervisor",
                table: "Educations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Summary",
                table: "Educations",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProjectTitle",
                table: "Educations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Degree",
                table: "Educations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Checksum",
                table: "Languages",
                type: "varbinary(max)",
                nullable: true,
                computedColumnSql: "CONVERT(VARBINARY(1024), CHECKSUM([Name], [Level]))",
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true,
                oldComputedColumnSql: "CONVERT(VARBINARY(1024),CHECKSUM([Name],[Level]))");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Supervisor",
                table: "Educations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Summary",
                table: "Educations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProjectTitle",
                table: "Educations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Degree",
                table: "Educations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Checksum",
                table: "Languages",
                type: "varbinary(max)",
                nullable: true,
                computedColumnSql: "CONVERT(VARBINARY(1024),CHECKSUM([Name],[Level]))",
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true,
                oldComputedColumnSql: "CONVERT(VARBINARY(1024), CHECKSUM([Name], [Level]))");
        }
    }
}
