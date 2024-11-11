using OGCP.Curriculum.API.models;

namespace OGCP.Curriculum.API.dtos;

public record Request
{

}
public record CreateQualifiedProfileRequest : Request
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Summary { get; set; }
    public Profiletype Profiletype { get; set; }
    public string DesiredJobRole { get; set; }
    public bool IsPublic { get; set; }
}

public record CreateGeneralProfileRequest : Request
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Summary { get; set; }
    public Profiletype Profiletype { get; set; }
    public bool IsPublic { get; set; }
}

public record CreateStudentProfileRequest : Request
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Summary { get; set; }
    public string Major { get; set; }
    public Profiletype Profiletype { get; set; }
    public string DesiredJobRole { get; set; }
    public bool IsPublic { get; set; }
    public string CareerGoals { get; set; }
}

public record PersonalInfoRequest
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string Phone { get; init; }
    public string Summary { get; init; }
}

public record SkillRequest
{
    public string Name { get; init; }
    public string Level { get; init; }
}

public record WorkExperienceRequest
{
    public string Company { get; init; }
    public string Position { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public string Description { get; init; }
}

public record EducationRequest
{
    public string Institution { get; init; }
    public string Degree { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime? EndDate { get; init; }
}
