using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.repositories.interfaces;
using OGCP.Curriculum.API.services.interfaces;
using System.Linq.Expressions;

namespace OGCP.Curriculum.API.services;

public class StudentProfileService : IStudentProfileService
{
    private readonly IStudentProfileRepository repository;

    public StudentProfileService(IStudentProfileRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result> AddEducation(int id, CreateResearchEducationRequest request)
    {
        StudentProfile profile = await this.repository.Find(id, GetQueryExpression());
        if (profile is null)
        {
            return Result.Failure("");

        }

        (string institution, DateTime startDate, DateTime? endDate, string projectTitle, string supervisor, string summary ) = request;
        ResearchEducation education = ResearchEducation.Create(institution, startDate, endDate, projectTitle, supervisor, summary).Value;

        profile.AddEducation(education);

        return await this.repository.SaveChanges();
    }

    public async Task<Result> AddLanguage(int id, CreateLanguageRequest languageRequest)
    {
        StudentProfile profile = await this.repository.Find(id, GetQueryExpression());

        Language language = Language.Create(languageRequest.Name, languageRequest.Level);
        Result result = profile.AddLanguage(language);

        return await repository.SaveChanges();
    }

    public Task<Result> Create(StudentProfile request)
    {
        this.repository.Add(request);
        return this.repository.SaveChanges();
    }

    public Task<IEnumerable<StudentProfile>> Get()
    {
        return this.repository.Find();
    }

    public Task<StudentProfile> Get(int id)
    {
        return this.repository.Find(id);
    }

    private Expression<Func<StudentProfile, object>>[] GetQueryExpression()
    {
        return
        [
            x => x.Educations,
            x => x.Experiences,
            x => x.LanguagesSpoken,
            x => x.PersonalInfo,
        ];
    }
}
