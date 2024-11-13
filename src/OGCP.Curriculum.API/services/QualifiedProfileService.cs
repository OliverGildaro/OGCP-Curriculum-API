using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.factories.interfaces;
using OGCP.Curriculum.API.models;
using OGCP.Curriculum.API.repositories.interfaces;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.services;

public class QualifiedProfileService : IQualifiedProfileService
{
    private readonly IQualifiedProfileRepository repository;

    public QualifiedProfileService(IQualifiedProfileRepository repository, IProfileFactory factory)
    {
        this.repository = repository;
    }

    public void AddEducation(int id, CreateEducationRequest request)
    {
        QualifiedProfile profile = this.repository.Find(id);
        if (profile is null)
        {
            return;
        }

        (string institution, DegreeEnum degree, DateTime startDate, DateTime? endDate ) = request;
        Education education = new Education(institution, degree, startDate, endDate);

        profile.AddEducation(education);

        this.repository.SaveChanges();
    }

    public void AddLanguage(int id, CreateLanguageRequest languageRequest)
    {
        QualifiedProfile profile = this.repository.Find(id);

        Language language = new Language(languageRequest.Name, languageRequest.Level);
        bool result = profile.AddLanguage(language);

        repository.SaveChanges();
    }

    public void Create(ProfileRequest request)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<QualifiedProfile> Get()
    {
        throw new NotImplementedException();
    }

    public QualifiedProfile Get(int id)
    {
        throw new NotImplementedException();
    }
}
