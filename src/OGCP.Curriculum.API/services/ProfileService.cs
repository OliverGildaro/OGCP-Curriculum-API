using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.factories.interfaces;
using OGCP.Curriculum.API.models;
using OGCP.Curriculum.API.repositories.interfaces;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.services;

public class ProfileService : Service<Profile, int, ProfileRequest>, IProfileService
{
    public ProfileService(IGeneralProfileRepository repository, IProfileFactory factory)
        :base(repository, factory)
    {
    }

    public void AddLanguage(int id, CreateLanguageRequest languageRequest)
    {
        var profile = base.Get(id);

        Language language = new Language(languageRequest.Name, languageRequest.Level);
        bool result = profile.AddLanguage(language);

        base.repository.SaveChanges();
    }
}
