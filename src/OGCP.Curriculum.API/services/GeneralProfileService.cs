using OGCP.Curriculum.API.domainmodel;
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

    private Expression<Func<GeneralProfile, object>>[] GetQueryExpression()
    {
        return
        [
            x => x.Experiences,
            x => x.LanguagesSpoken,
            x => x.PersonalInfo,
        ];
    }

    public Task<int> Create(GeneralProfile request)
    {

        var result = this.repository.Add(request);

        if (result.IsFailure)
        {
            throw new ArgumentException();
        }

        return repository.SaveChanges();
    }

    public Task<IReadOnlyList<GeneralProfile>> Get()
    {
        return this.repository.Find();
    }

    public Task<GeneralProfile> Get(int id)
    {
        return this.repository.Find(id);
    }
}
