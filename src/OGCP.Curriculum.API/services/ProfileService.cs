using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.factories.interfaces;
using OGCP.Curriculum.API.models;
using OGCP.Curriculum.API.repositories.interfaces;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.services;

public class ProfileService : GenericService<Profile, int, ProfileRequest>, IProfileService
{
    public ProfileService(IProfileRepository repository, IProfileFactory factory)
        :base(repository, factory)
    {
    }

    public void AddEducation(int id, CreateEducationRequest request)
    {
        Profile profile = base.Get(id);
        if (profile is null)
        {
            return;
        }
        QualifiedProfile generalProfile = (QualifiedProfile)profile;

        (string institution, DegreeEnum degree, DateTime startDate, DateTime? endDate ) = request;
        Education education = new Education(institution, degree, startDate, endDate);

        generalProfile.AddEducation(education);

        this.repository.SaveChanges();
    }

    public void AddLanguage(int id, CreateLanguageRequest languageRequest)
    {
        var profile = base.Get(id);

        Language language = new Language(languageRequest.Name, languageRequest.Level);
        bool result = profile.AddLanguage(language);

        base.repository.SaveChanges();
    }
}
