using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.repositories.interfaces;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.services;

public class StudentProfileService : IStudentProfileService
{
    private readonly IStudentProfileRepository repository;

    public StudentProfileService(IStudentProfileRepository repository)
    {
        this.repository = repository;
    }

    public Result AddEducation(int id, CreateResearchEducationRequest request)
    {
        StudentProfile profile = this.repository.Find(id);
        if (profile is null)
        {
            return Result.Failure("");

        }

        (string institution, DateTime startDate, DateTime? endDate, string projectTitle, string supervisor, string summary ) = request;
        ResearchEducation education = ResearchEducation.Create(institution, startDate, endDate, projectTitle, supervisor, summary).Value;

        profile.AddEducation(education);

        this.repository.SaveChanges();
        return Result.Success();

    }

    public void AddLanguage(int id, CreateLanguageRequest languageRequest)
    {
        StudentProfile profile = this.repository.Find(id);

        Language language = Language.Create(languageRequest.Name, languageRequest.Level);
        Result result = profile.AddLanguage(language);

        repository.SaveChanges();
    }

    public Result Create(CreateStudentProfileRequest request)
    {
        (string firstName, string lastName, string summary, string major, string careerGoals) = request;
        var sdsd =  StudentProfile.Create(firstName, lastName, summary, major, careerGoals);
        this.repository.Add(sdsd.Value);

        this.repository.SaveChanges();
        return Result.Success();

    }

    public IEnumerable<StudentProfile> Get()
    {
        return this.repository.Find();
    }

    public StudentProfile Get(int id)
    {
        return this.repository.Find(id);
    }
}
