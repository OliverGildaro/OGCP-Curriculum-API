using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.repositories.interfaces;
using OGCP.Curriculum.API.services.interfaces;
using System.Linq.Expressions;

namespace OGCP.Curriculum.API.services;

public class QualifiedProfileService : IQualifiedProfileService
{
    private readonly IQualifiedProfileRepository repository;

    public QualifiedProfileService(IQualifiedProfileRepository repository)
    {
        this.repository = repository;
    }

    public Result AddEducation(int id, CreateDegreeEducationRequest request)
    {
        QualifiedProfile profile = this.repository.Find(id);
        if (profile is null)
        {
            return Result.Failure("");
        }

        (string institution, EducationLevel degree, DateTime startDate, DateTime? endDate ) = request;
        Education education = DegreeEducation.Create(institution, degree, startDate, endDate).Value;

        profile.AddEducation(education);

        this.repository.SaveChanges();
        return Result.Success();

    }

    public Result AddJobExperience<T>(int id, T request)
    {
        var profile = this.repository.Find(id);
        JobExperience jobExperince = FactoryJob.Get(request);
        profile.AddJobExperience(jobExperince);

        this.repository.SaveChanges();
        return Result.Success();

    }

    public void AddLanguage(int id, CreateLanguageRequest languageRequest)
    {
        QualifiedProfile profile = this.Find(id);

        Language language = Language.Create(languageRequest.Name, languageRequest.Level);
        Result result = profile.AddLanguage(language);

        repository.SaveChanges();
    }

    private QualifiedProfile Find(int id)
    {
        var defaultIncludes = new Expression<Func<QualifiedProfile, object>>[]
        {
            x => x.Educations,
            x => x.Experiences,
            x => x.LanguagesSpoken,
            x => x.PersonalInfo,
        };

        return this.repository.Find(id, defaultIncludes);
    }

    public Result Create(CreateQualifiedProfileRequest request)
    {
        (string firstName, string lastName, string summary, string desiredJobRole) = request;
        var resu = QualifiedProfile.Create(firstName, lastName, summary, desiredJobRole);
        this.repository.Add(resu.Value);
        this.repository.SaveChanges();
        return Result.Success();
    }

    public IEnumerable<QualifiedProfile> Get()
    {
        return this.repository.Find();
    }

    public QualifiedProfile Get(int id)
    {
        return this.repository.Find(id);
    }
}
