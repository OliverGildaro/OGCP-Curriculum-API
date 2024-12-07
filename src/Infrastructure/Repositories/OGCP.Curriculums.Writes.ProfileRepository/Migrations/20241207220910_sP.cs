using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OGCP.Curriculums.Writes.ProfileRepository.Migrations
{
    /// <inheritdoc />
    public partial class sP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE PROCEDURE DeleteOrphanedEducations
                AS
                BEGIN
                    -- Delete orphaned Educations from the Educations table
                    DELETE FROM Educations
                    WHERE Id NOT IN (
                        SELECT EducationId FROM ProfileEducations
                    );
                END;
            ");

            migrationBuilder.Sql(@"
            CREATE PROCEDURE dbo.GetEducationsByRangeDate
                @startDate  DATE,
	            @endDate DATE
                AS
                BEGIN
                    SET NOCOUNT ON; --Evita mensajes de conteo de filas afectadas.
                    DECLARE @count as INT = 0;

                    --select all profiles educations that were completed in a range date
                    SELECT p.FirstName,
                    p.LastName,
                    e.Institution,
                    e.StartDate,
                    e.EndDate,
                    e.Degree,
                    e.ProjectTitle from Profiles p
                        INNER JOIN ProfileEducations pe
                            ON p.Id = pe.ProfileId
                        INNER JOIN Educations e
                            ON e.Id = pe.EducationId
                                WHERE e.StartDate >= @startDate AND(e.EndDate <= @endDate OR e.EndDate IS NULL)
                                ORDER BY e.Institution
                    
                    --COunt how many eductions where completted in a range date
                    SELECT @count = COUNT(*)
                        FROM ProfileEducations pe
                        INNER JOIN Educations e ON e.Id = pe.EducationId
                            WHERE e.StartDate >= @startDate AND(e.EndDate <= @endDate OR e.EndDate IS NULL);
                        SELECT @count AS TotalEducationsInRange;

                        --Adicional: Contar cuántos perfiles están asociados a cada institución en el rango de fechas
                    SELECT
                        e.Institution,
                        COUNT(DISTINCT p.Id) AS ProfileCount
                            FROM Profiles p
                                INNER JOIN ProfileEducations pe ON p.Id = pe.ProfileId
                                INNER JOIN Educations e ON e.Id = pe.EducationId
                                    WHERE e.StartDate >= @startDate AND(e.EndDate <= @endDate OR e.EndDate IS NULL)
                                    GROUP BY e.Institution
                                    ORDER BY ProfileCount DESC, e.Institution;
                END"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE DeleteOrphanedEducations;");
            migrationBuilder.Sql("DROP PROCEDURE GetEducationsByRangeDate;");
        }
    }
}
