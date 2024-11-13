using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.factories.interfaces;
using OGCP.Curriculum.API.models;
using OGCP.Curriculum.API.repositories.interfaces;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.services;

public class GeneralProfileService : IGeneralProfileService
{
    private readonly IGeneralProfileRepository repository;

    public GeneralProfileService(IGeneralProfileRepository repository, IProfileFactory factory)
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

    public void Create(ProfileRequest request)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<GeneralProfile> Get()
    {
        throw new NotImplementedException();
    }

    public GeneralProfile Get(int id)
    {
        throw new NotImplementedException();
    }
}
