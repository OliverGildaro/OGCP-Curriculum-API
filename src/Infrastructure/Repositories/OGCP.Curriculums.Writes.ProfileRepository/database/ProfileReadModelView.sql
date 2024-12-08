CREATE VIEW [dbo].[ProfileReadModelView]
AS
SELECT
	p.Id,
	p.FirstName,
	p.LastName,
	p.Summary,
	p.CareerGoals,
	p.DesiredJobRole,
	p.CreatedAt,
	p.UpdatedAt,
	p.PersonalGoals,
	l.Id as LanguageId,
	l.Level,
	l.Name,
	e.Institution,
	e.Summary as EducationSummary,
	e.Degree,
	e.ProjectTitle,
	e.Supervisor,
	e.StartDate,
	e.EndDate
FROM 
	Profiles p
	INNER JOIN ProfileEducations pe
	ON p.Id = pe.ProfileId
	inner JOIN Educations e
	on e.Id = pe.EducationId
	INNER JOIN ProfileLanguages pl
	on pl.ProfileId = p.Id
	INNER JOIN Languages l
	on l.Id = pl.LanguageId
