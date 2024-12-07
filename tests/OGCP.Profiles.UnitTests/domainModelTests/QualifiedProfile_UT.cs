using ArtForAll.Shared.Contracts.DDD;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Profiles.UnitTests.domainModelTests;

public class QualifiedProfile_UT
{
    [Theory]
    [InlineData("Oliver", "Castro", "I am a fullstack dev with bla", ".net backend dev")]
    [InlineData("Carolina", "Castro", "I am a fullstack java dev with bla", "java backend dev")]
    public void CreateQualifiedProfile(string firstName, string lastName, string summary, string desiredRole)
    {
        var qualifProfResult = QualifiedProfile.Create(firstName, lastName, summary, desiredRole);
        var qualifiedProf = qualifProfResult.Value;

        Assert.NotNull(qualifiedProf);
        Assert.Equal(firstName, qualifiedProf.FirstName);
        Assert.Equal(lastName, qualifiedProf.LastName);
        Assert.Equal(summary, qualifiedProf.Summary);
        Assert.Equal(desiredRole, qualifiedProf.DesiredJobRole);
        Assert.IsType<QualifiedProfile>(qualifiedProf);
        Assert.True(qualifProfResult.IsSucces);
    }

    [Theory]
    [InlineData("Oliver", "Castro", "I am a fullstack dev with bla", ".net backend dev", Languages.English, ProficiencyLevel.Proficient)]
    [InlineData("Carolina", "Castro", "I am a fullstack java dev with bla", "java backend dev", Languages.Italian, ProficiencyLevel.Intermediate)]
    public void CreatedQualifiedProfile_CanNotAddTheSameLanguageTwice(
        string firstName,
        string lastName,
        string summary,
        string desiredRole,
        Languages language,
        ProficiencyLevel proficiency)
    {
        var qualifProfResult = QualifiedProfile.Create(firstName, lastName, summary, desiredRole);
        var qualifiedProf = qualifProfResult.Value;

        var languageResult = Language.Create(language, proficiency);
        qualifiedProf.AddLanguage(languageResult);

        var language2 = Language.Create(language, proficiency);
        var resultAdd = qualifiedProf.AddLanguage(language2);
        
        Assert.False(resultAdd.IsSucces);
        Assert.Equal($"{language} can not be added twice", resultAdd.Message);
    }

    [Theory]
    [InlineData("Oliver", "Castro", "I am a fullstack dev with bla", ".net backend dev",  "UMSS", EducationLevel.Doctorate, "2021-12-14")]
    [InlineData("Carolina", "Castro", "I am a fullstack java dev with bla", "java backend dev", "UMSS", EducationLevel.Doctorate, "2021-12-14")]
    public void CreatedQualifiedProfile_CanNotAddTheSameEducationTwice(
        string firstName,
        string lastName,
        string summary,
        string desiredRole,
        string institution,
        EducationLevel educationLevel,
        string startDate)
    {
        var qualifProfResult = QualifiedProfile.Create(firstName, lastName, summary, desiredRole);
        var qualifiedProf = qualifProfResult.Value;

        var education = DegreeEducation.Create(institution, educationLevel, DateOnly.Parse(startDate), null).Value;
        qualifiedProf.AddEducation(education);
        var result = qualifiedProf.AddEducation(education);

        Assert.False(result.IsSucces);
        Assert.Equal("UMSS can not be added twice", result.Message);
    }

    [Theory]
    [InlineData("Oliver", "Castro", "I am a fullstack dev with bla", ".net backend dev", "Jalasoft", "2021-12-14", "2022-12-14", "Description", "Backend")]
    [InlineData("Carolina", "Castro", "I am a fullstack java dev with bla", "java backend dev", "Jalasoft", "2021-12-14", "2021-12-14", "Desc", "Frontend")]
    public void CreatedQualifiedProfile_CanNotAddTheSameWorkExperienceTwice(
    string firstName,
    string lastName,
    string summary,
    string desiredRole,
    string company,
    DateTime startDate,
    DateTime endDate,
    string description,
    string position)
    {
        var qualifProfResult = QualifiedProfile.Create(firstName, lastName, summary, desiredRole);
        var qualifiedProf = qualifProfResult.Value;

        var WorkExp1 = WorkExperience.Create(company, startDate, endDate, description, position).Value;
        qualifiedProf.AddJobExperience(WorkExp1);
        var result = qualifiedProf.AddJobExperience(WorkExp1);

        Assert.False(result.IsSucces);
        Assert.Equal("This work experience can not be added twice", result.Message);
    }

    [Theory]
    [InlineData(null, "Castro", "I am a fullstack java dev with bla", "java backend dev", "First name is required.")]
    [InlineData("Carolina", null, "I am a fullstack java dev with bla", "java backend dev", "Last name is required.")]
    [InlineData("Carolina", "Castro", "I am a fullstack java dev with bla", null, "Desired job role is required.")]
    public void CreateQualifiedProfile_failsWithNullValues(string firstName, string lastName, string summary, string desiredRole, string expectedErrorMessage)
    {
        var qualifProfResult = QualifiedProfile.Create(firstName, lastName, summary, desiredRole);
        var error = qualifProfResult.Error;

        Assert.False(qualifProfResult.IsSucces);
        Assert.Equal(expectedErrorMessage, error.Message);
        Assert.IsType<Error>(error);
    }
}

