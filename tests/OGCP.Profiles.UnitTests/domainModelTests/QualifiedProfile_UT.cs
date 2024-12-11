using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculums.Core.DomainModel;
using OGCP.Curriculums.Core.DomainModel.profiles;
using OGCP.Curriculums.Core.DomainModel.valueObjects;
using static OGCP.Curriculums.Core.DomainModel.valueObjects.LanguageSkill;

namespace OGCP.Profiles.UnitTests.domainModelTests;

public class QualifiedProfile_UT
{
    [Theory]
    [InlineData(
        "Oliver",
        "Castro",
        "I am a fullstack dev with bla",
        ".net backend dev",
        "591",
        "69554851",
        "gildaro.castro@gmail.com")]
    [InlineData(
        "Carolina",
        "Castro",
        "I am a fullstack java dev with bla",
        "java backend dev",
        "591",
        "69554851",
        "gildaro.castro@gmail.com")]
    public void CreateQualifiedProfile(
        string firstName,
        string lastName,
        string summary,
        string desiredRole,
        string countryCode,
        string number,
        string email)
    {
        //var phoneNumb = PhoneNumber.Parse(phone);
        var phoneNumb2 = PhoneNumber.CreateNew(countryCode, number);
        var name = Name.CreateNew(firstName, lastName);
        var emailResult = Email.CreateNew(email);
        var qualifProfResult = QualifiedProfile
            .Create(name.Value, summary, desiredRole, phoneNumb2.Value, emailResult.Value);
        var qualifiedProf = qualifProfResult.Value;

        Assert.NotNull(qualifiedProf);
        Assert.Equal(firstName, qualifiedProf.Name.GivenName);
        Assert.Equal(lastName, qualifiedProf.Name.FamilyNames);
        Assert.Equal(summary, qualifiedProf.Summary);
        Assert.Equal(desiredRole, qualifiedProf.DesiredJobRole);
        Assert.Equal(countryCode, qualifiedProf.Phone.CountryCode);
        Assert.Equal(number, qualifiedProf.Phone.Number);
        Assert.Equal(email, qualifiedProf.Email.Value);
        Assert.IsType<QualifiedProfile>(qualifiedProf);
        Assert.True(qualifProfResult.IsSucces);
    }

    [Theory]
    [InlineData(
        "Oliver",
        "Castro",
        "I am a fullstack dev with bla",
        ".net backend dev",
        Languages.Italian,
        ProficiencyLevel.Intermediate,
        "591",
        "69554851",
        "gildaro.castro@gmail.com")]
    public void CreateQualifiedProfileAndAddLanguagesAndLanguageDetails(
        string firstName,
        string lastName,
        string summary,
        string desiredRole,
        Languages language,
        ProficiencyLevel proficiency,
        string countryCode,
        string number,
        string email)
    {
        var phoneNumb2 = PhoneNumber.CreateNew(countryCode, number);
        var name = Name.CreateNew(firstName, lastName);
        var emailResult = Email.CreateNew(email);
        var qualifProfResult = QualifiedProfile
            .Create(name.Value, summary, desiredRole, phoneNumb2.Value, emailResult.Value);
        var qualifiedProf = qualifProfResult.Value;

        var languageResult = Language.Create(language, proficiency);
        qualifiedProf.AddLanguage(languageResult);

        var skillWriting = LanguageSkill.CreateNew(LangSkill.WRITING, ProficiencyLevel.Intermediate);
        var skillListening = LanguageSkill.CreateNew(LangSkill.LISTENING, ProficiencyLevel.Advanced);
        var reading = LanguageSkill.CreateNew(LangSkill.READING, ProficiencyLevel.Beginner);
        var speaking = LanguageSkill.CreateNew(LangSkill.SPEAKING, ProficiencyLevel.Proficient);

        qualifiedProf.AddLAnguageSkill(0, skillWriting.Value);
        qualifiedProf.AddLAnguageSkill(0, skillListening.Value);
        qualifiedProf.AddLAnguageSkill(0, reading.Value);
        qualifiedProf.AddLAnguageSkill(0, speaking.Value);


        //add native
        //var languageNativeResult = Language.Create(Languages.Spanish, ProficiencyLevel.Native);
        //qualifiedProf.AddLanguage(languageResult);

        //var readingNative = LanguageSkill.CreateNew(LangSkill.READING, ProficiencyLevel.Beginner);
        //qualifiedProf.AddLAnguageSkill(0, readingNative.Value);

        Assert.Single(qualifiedProf.LanguagesSpoken);
        Assert.Equal(4, qualifiedProf.LanguagesSpoken[0].LanguageSkills.Count);
        //Assert.Empty(qualifiedProf.LanguagesSpoken[1].LanguageSkills);
    }

    [Theory]
    [InlineData(
    "Oliver",
    "Castro",
    "I am a fullstack dev with bla",
    ".net backend dev",
    Languages.Italian, ProficiencyLevel.Intermediate,
    "591",
    "69554851",
    "gildaro.castro@gmail.com")]
    [InlineData(
    "Carolina",
    "Castro",
    "I am a fullstack java dev with bla",
    "java backend dev",
    Languages.English, ProficiencyLevel.Proficient,
    "591",
    "69554851",
    "gildaro.castro@gmail.com")]
    public void CreatedQualifiedProfile_CanNotAddTheSameLanguageTwice(
        string firstName,
        string lastName,
        string summary,
        string desiredRole,
        Languages language,
        ProficiencyLevel proficiency,
        string countryCode,
        string number,
        string email)
    {
        var phoneNumb2 = PhoneNumber.CreateNew(countryCode, number);
        var name = Name.CreateNew(firstName, lastName);
        var emailResult = Email.CreateNew(email);
        var qualifProfResult = QualifiedProfile
            .Create(name.Value, summary, desiredRole, phoneNumb2.Value, emailResult.Value);
        var qualifiedProf = qualifProfResult.Value;

        var languageResult = Language.Create(language, proficiency);
        qualifiedProf.AddLanguage(languageResult);

        var language2 = Language.Create(language, proficiency);
        var resultAdd = qualifiedProf.AddLanguage(language2);

        Assert.False(resultAdd.IsSucces);
        Assert.Equal($"{language} can not be added twice", resultAdd.Message);
    }

    [Theory]
    [InlineData("Oliver", "Castro", "I am a fullstack dev with bla", ".net backend dev", "UMSS", EducationLevel.Doctorate, "2021-12-14",
        "+59169554851", "gildaro.castro@gmail.com")]
    [InlineData("Carolina", "Castro", "I am a fullstack java dev with bla", "java backend dev", "UMSS", EducationLevel.Doctorate, "2021-12-14",
        "+59169554851", "gildaro.castro@gmail.com")]
    public void CreatedQualifiedProfile_CanNotAddTheSameEducationTwice(
        string firstName,
        string lastName,
        string summary,
        string desiredRole,
        string institution,
        EducationLevel educationLevel,
        string startDate,
        string phone,
        string email)
    {
        var phoneNumb = PhoneNumber.Parse(phone);
        var name = Name.CreateNew(firstName, lastName);
        var emailResult = Email.CreateNew(email);
        var qualifProfResult = QualifiedProfile
            .Create(name.Value, summary, desiredRole, phoneNumb, emailResult.Value);
        var qualifiedProf = qualifProfResult.Value;

        var education = DegreeEducation.Create(institution, educationLevel, DateOnly.Parse(startDate), null).Value;
        qualifiedProf.AddEducation(education);
        var result = qualifiedProf.AddEducation(education);

        Assert.False(result.IsSucces);
        Assert.Equal("UMSS can not be added twice", result.Message);
    }

    [Theory]
    [InlineData("Oliver", "Castro", "I am a fullstack dev with bla", ".net backend dev",
        "+59169554851", "gildaro.castro@gmail.com",
        "Jalasoft",
        "2021-12-14", "2022-12-14", "Description", "Backend")]
    [InlineData("Carolina", "Castro", "I am a fullstack java dev with bla", "java backend dev",
        "+59169554851", "gildaro.castro@gmail.com", "Jalasoft", "2021-12-14", "2021-12-14", "Desc", "Frontend")]
    public void CreatedQualifiedProfile_CanNotAddTheSameWorkExperienceTwice(
    string firstName,
    string lastName,
    string summary,
    string phone,
    string email,
    string desiredRole,
    string company,
    DateTime startDate,
    DateTime endDate,
    string description,
    string position)
    {
        var phoneNumb = PhoneNumber.Parse(phone);
        var name = Name.CreateNew(firstName, lastName);
        var emailResult = Email.CreateNew(email);
        var qualifProfResult = QualifiedProfile
            .Create(name.Value, summary, desiredRole, phoneNumb, emailResult.Value);
        var qualifiedProf = qualifProfResult.Value;

        var WorkExp1 = WorkExperience.Create(company, startDate, endDate, description, position).Value;
        qualifiedProf.AddJobExperience(WorkExp1);
        var result = qualifiedProf.AddJobExperience(WorkExp1);

        Assert.False(result.IsSucces);
        Assert.Equal("This work experience can not be added twice", result.Message);
    }

    [Theory]
    [InlineData(null, "Castro", "I am a fullstack java dev with bla", "java backend dev", "Value is required for 'firstName'.",
        "+59169554851", "gildaro.castro@gmail.com")]
    [InlineData("Carolina", null, "I am a fullstack java dev with bla", "java backend dev", "Value is required for 'lastName'.",
        "+59169554851", "gildaro.castro@gmail.com")]
    public void CreateQualifiedProfile_failsWithNullValues(
        string firstName,
        string lastName,
        string summary,
        string desiredRole,
        string expectedErrorMessage,
        string phone,
        string email)
    {
        var phoneNumb = PhoneNumber.Parse(phone);
        var name = Name.CreateNew(firstName, lastName);
        var emailResult = Email.CreateNew(email);
        var qualifProfResult = QualifiedProfile
            .Create(name.Value, summary, desiredRole, phoneNumb, emailResult.Value);

        var error = qualifProfResult.Error;

        Assert.False(qualifProfResult.IsSucces);
        Assert.Equal(expectedErrorMessage, error.Message);
        Assert.IsType<Error>(error);
    }
}

