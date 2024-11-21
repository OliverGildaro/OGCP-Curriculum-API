using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.dtos;



public class CreateLanguageRequest
{
    public Languages Name { get; set; }
    public ProficiencyLevel Level { get; set; }
}

public class CreateEducationRequest
{
    public string Institution { get; set; }
    public EducationLevel Degree { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}


public class CreateDegreeEducationRequest : CreateEducationRequest
{
    public EducationLevel Degree { get; set; }

    public void Deconstruct(out string institution, out EducationLevel degree, out DateTime startDate, out DateTime? endDate)
    {
        institution = base.Institution;
        degree = this.Degree;
        startDate = base.StartDate;
        endDate = base.EndDate;
    }
}

public class CreateResearchEducationRequest : CreateEducationRequest
{
    public string ProjectTitle { get; set; }
    public string Supervisor { get; set; }
    public string Summary { get; set; }
    public void Deconstruct(
        out string institution,
        out DateTime startDate,
        out DateTime? endDate,
        out string projectTitle,
        out string supervisor,
        out string summary)
    {
        institution = this.Institution;
        startDate = this.StartDate;
        endDate = this.EndDate;
        projectTitle = this.ProjectTitle;
        supervisor = this.Supervisor;
        summary = this.Summary;
    }
}




public class PersonalInfoRequest
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string Phone { get; init; }
    public string Summary { get; init; }
}

public class SkillRequest
{
    public string Name { get; init; }
    public string Level { get; init; }
}

public class WorkExperienceRequest
{
    public string Company { get; init; }
    public string Position { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public string Description { get; init; }
}

public class EducationRequest
{
    public string Institution { get; init; }
    public EducationLevel Degree { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime? EndDate { get; init; }
}


