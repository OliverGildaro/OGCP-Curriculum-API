using OGCP.Curriculum.API.domainModel;
using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.dtos.requests;
using OGCP.Curriculum.API.models;
using OGCP.Curriculum.API.repositories.interfaces;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.services;

public class QualifiedProfileService : IQualifiedProfileService
{
    private readonly IQualifiedProfileRepository repository;

    public QualifiedProfileService(IQualifiedProfileRepository repository)
    {
        this.repository = repository;
    }

    public void AddEducation(int id, CreateDegreeEducationRequest request)
    {
        QualifiedProfile profile = this.repository.Find(id);
        if (profile is null)
        {
            return;
        }

        (string institution, EducationLevel degree, DateTime startDate, DateTime? endDate ) = request;
        Education education = new DegreeEducation(institution, degree, startDate, endDate);

        profile.AddEducation(education);

        this.repository.SaveChanges();
    }

    public void AddJobExperience<T>(int id, T request)
    {
        var profile = this.repository.Find(id);
        JobExperience jobExperince = FactoryJob.Get(request);
        profile.AddJobExperience(jobExperince);

        this.repository.SaveChanges();
    }

    public void AddLanguage(int id, CreateLanguageRequest languageRequest)
    {
        QualifiedProfile profile = this.repository.Find(id);

        Language language = new Language(languageRequest.Name, languageRequest.Level);
        bool result = profile.AddLanguage(language);

        repository.SaveChanges();
    }

    public void Create(CreateQualifiedProfileRequest request)
    {
        (string firstName, string lastName, string summary, string desiredJobRole) = request;
        var resu = new QualifiedProfile(firstName, lastName, summary, desiredJobRole);
        this.repository.Add(resu);
        this.repository.SaveChanges();
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
