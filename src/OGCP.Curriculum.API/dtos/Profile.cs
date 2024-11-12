using OGCP.Curriculum.API.helpers;
using OGCP.Curriculum.API.models;
using System.Text.Json.Serialization;

namespace OGCP.Curriculum.API.dtos;

public abstract class ProfileRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Summary { get; set; }
    public ProfileEnum RequestType { get; set; } // Discriminator field
}

public enum ProfileEnum
{
    CreateGeneralProfileRequest = 1,
    CreateQualifiedProfileRequest
}

public class CreateGeneralProfileRequest : ProfileRequest
{
    public Profiletype Profiletype { get; set; }
    public bool IsPublic { get; set; }

    public void Deconstruct(out string firstName, out string lastName, out string summary)
    {
        firstName = this.FirstName;  // Assuming FirstName is a property in ProfileRequest
        lastName = this.LastName;    // Assuming LastName is a property in ProfileRequest
        summary = this.Summary;      // Assuming Summary is a property in ProfileRequest
    }
}

public class CreateQualifiedProfileRequest : ProfileRequest
{
    public Profiletype Profiletype { get; set; }
    public string DesiredJobRole { get; set; }
    public bool IsPublic { get; set; }
}


public class CreateStudentProfileRequest : ProfileRequest
{
    public Profiletype Profiletype { get; set; }
    public string Major { get; set; }
    public string DesiredJobRole { get; set; }
    public bool IsPublic { get; set; }
    public string CareerGoals { get; set; }
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
    public string Degree { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime? EndDate { get; init; }
}
