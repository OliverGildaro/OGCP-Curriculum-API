        --protected override void Up(MigrationBuilder migrationBuilder)
        --{
        --    // Step 1: Create the stored procedure
        --    migrationBuilder.Sql(@"
        --        CREATE PROCEDURE DeleteOrphanedEducations
        --            AS
        --            BEGIN
        --                -- Delete orphaned Educations from the Educations table
        --                DELETE FROM Educations
        --                WHERE Id NOT IN (
        --                    SELECT EducationId FROM ProfileEducations
        --                );
        --            END;
        --        ");
        --}

        --protected override void Down(MigrationBuilder migrationBuilder)
        --{
        --    // Step 2: Drop the stored procedure if rolling back the migration
        --    migrationBuilder.Sql("DROP PROCEDURE DeleteOrphanedEducations;");
        --}






        
--CREATE PROCEDURE dbo.GetEducationsBYRangeDate
--	@startDate	DATETIME,
--	@endDate DATETIME
--AS
--BEGIN
--	SET NOCOUNT ON; -- Evita mensajes de conteo de filas afectadas.
--	DECLARE @count as INT = 0;

--	SELECT p.FirstName,
--        p.LastName,
--        e.Institution,
--        e.StartDate,
--        e.EndDate,
--        e.Degree,
--        e.ProjectTitle from Profiles p
--			INNER JOIN ProfileEducations pe
--			ON p.Id = pe.ProfileId
--			INNER JOIN Educations e
--			ON e.Id = pe.EducationId
--				WHERE e.StartDate >= @startDate AND (e.EndDate <= @endDate OR e.EndDate IS NULL)
--				ORDER BY e.Institution

--	SELECT @count = COUNT(*)
--		FROM ProfileEducations pe
--		INNER JOIN Educations e ON e.Id = pe.EducationId
--			WHERE e.StartDate >= @startDate AND (e.EndDate <= @endDate OR e.EndDate IS NULL);

--    SELECT @count AS TotalEducationsInRange;

--	-- Adicional: Contar cuántos perfiles están asociados a cada institución
--    SELECT 
--        e.Institution,
--        COUNT(DISTINCT p.Id) AS ProfileCount
--		FROM Profiles p
--		INNER JOIN ProfileEducations pe ON p.Id = pe.ProfileId
--		INNER JOIN Educations e ON e.Id = pe.EducationId
--			WHERE e.StartDate >= @startDate AND (e.EndDate <= @endDate OR e.EndDate IS NULL)
--			GROUP BY e.Institution
--			ORDER BY ProfileCount DESC, e.Institution;
--END

--EXEC dbo.GetEducationsByRangeDate 
--    @startDate = '2020-12-14T00:00:00', 
--    @endDate = null;