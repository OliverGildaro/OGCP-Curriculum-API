using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.models;
using OGCP.Curriculum.API.repositories.interfaces;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.services;

public class GeneralProfileService : IGeneralProfileService
{
    private readonly IGeneralProfileRepository repository;

    public GeneralProfileService(IGeneralProfileRepository repository)
    {
        this.repository = repository;
    }

    public void AddLanguage(int id, CreateLanguageRequest languageRequest)
    {
        GeneralProfile profile = this.repository.Find(id);

        Language language = new Language(languageRequest.Name, languageRequest.Level);
        bool result = profile.AddLanguage(language);

        repository.SaveChanges();
    }

    public void Create(CreateGeneralProfileRequest request)
    {
        (string firstName, string lastName, string summary, string[] personalGoals) = request;
        var res = new GeneralProfile(firstName, lastName, summary, personalGoals);
        this.repository.Add(res);

        this.repository.SaveChanges();
    }

    public IEnumerable<GeneralProfile> Get()
    {
        return this.repository.Find();
    }

    public GeneralProfile Get(int id)
    {
        return this.repository.Find(id);
    }
}
