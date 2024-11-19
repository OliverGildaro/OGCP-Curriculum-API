using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.repositories.interfaces;
using OGCP.Curriculum.API.services.interfaces;
using System.Linq.Expressions;

namespace OGCP.Curriculum.API.services;

public class GeneralProfileService : IGeneralProfileService
{
    private readonly IGeneralProfileRepository repository;

    public GeneralProfileService(IGeneralProfileRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result> AddLanguage(int id, CreateLanguageRequest languageRequest)
    {
        GeneralProfile profile = await this.repository.Find(id, GetQueryExpression());

        Language language = Language.Create(languageRequest.Name, languageRequest.Level);
        Result result = profile.AddLanguage(language);

        return await repository.SaveChanges();
    }

    private Expression<Func<GeneralProfile, object>>[] GetQueryExpression()
    {
        return
        [
            x => x.Experiences,
            x => x.LanguagesSpoken,
            x => x.PersonalInfo,
        ];
    }

    public Task<Result> Create(GeneralProfile request)
    {

        var result = this.repository.Add(request);

        if (result.IsFailure)
        {
            throw new ArgumentException();
        }

        return this.repository.SaveChanges();
    }

    public Task<IEnumerable<GeneralProfile>> Get()
    {
        return this.repository.Find();
    }

    public Task<GeneralProfile> Get(int id)
    {
        return this.repository.Find(id);
    }
}
