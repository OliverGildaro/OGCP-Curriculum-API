using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OGCP.Curriculum.API.dtos;

public abstract class ProfileRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Summary { get; set; }
    public ProfileEnum RequestType { get; set; } // Discriminator field
}


public class CreateGeneralProfileRequest : ProfileRequest
{
    //public Profiletype Profiletype { get; set; }
    public string[] PersonalGoals { get; set; } = new string[] {};

    public void Deconstruct(out string firstName, out string lastName, out string summary, out string[] personalGoals)
    {
        firstName = this.FirstName;  // Assuming FirstName is a property in ProfileRequest
        lastName = this.LastName;    // Assuming LastName is a property in ProfileRequest
        summary = this.Summary;      // Assuming Summary is a property in ProfileRequest
        personalGoals = this.PersonalGoals;
    }
}

public class CreateQualifiedProfileRequest : ProfileRequest
{
    //public Profiletype Profiletype { get; set; }
    public void Deconstruct(out string firstName, out string lastName, out string summary, out string desiredJobRole)
    {
        firstName = this.FirstName;  // Assuming FirstName is a property in ProfileRequest
        lastName = this.LastName;    // Assuming LastName is a property in ProfileRequest
        summary = this.Summary;      // Assuming Summary is a property in ProfileRequest
        desiredJobRole = this.DesiredJobRole;
    }
    public string DesiredJobRole { get; set; }
}

public class CreateStudentProfileRequest : ProfileRequest
{
    //public Profiletype Profiletype { get; set; }
    public string Major { get; set; }
    public string CareerGoals { get; set; }

    public void Deconstruct(out string firstName, out string lastName, out string summary, out string major, out string careerGoals)
    {
        firstName = this.FirstName;  // Assuming FirstName is a property in ProfileRequest
        lastName = this.LastName;    // Assuming LastName is a property in ProfileRequest
        summary = this.Summary;      // Assuming Summary is a property in ProfileRequest
        major = this.Major;
        careerGoals = this.CareerGoals;
    }
}

public class CreateLanguageRequest
{
    public LanguageEnum Name { get; set; }
    public LevelEnum Level { get; set; }
}


public class CreateEducationRequest
{
    public string Institution { get; set; }
    public DegreeEnum Degree { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }


    public void Deconstruct(out string institution, out DegreeEnum degree, out DateTime startDate, out DateTime? endDate)
    {
        institution = this.Institution;
        degree = this.Degree;
        startDate = this.StartDate;
        endDate = this.EndDate;
    }
}

public enum DegreeEnum
{
    AssociateDegree,
    BachelorDegree,
    MasterDegree,
    Doctorate,
    PhD,
    Diploma,
    Certificate,
    HighSchool
}

public enum LevelEnum
{
    BEGINNER = 1,
    INTERMEDIATE = 2,
    PROFICIENT = 3,
    ADVANCED = 4,
    EXPERT = 5
}

public enum LanguageEnum
{
    SPANISH = 1,
    ENGLISH = 2,
    KOREAN = 3,
    ITALIAN = 4,
    PORTUGUESE = 5
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

public enum ProfileEnum
{
    CreateGeneralProfileRequest = 1,
    CreateQualifiedProfileRequest,
    CreateStudentProfileRequest
}
