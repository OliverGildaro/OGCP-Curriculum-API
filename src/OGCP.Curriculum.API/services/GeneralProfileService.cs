using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.DAL.Mutations.Interfaces;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.repositories.interfaces;
using OGCP.Curriculum.API.services.interfaces;
using System.Linq.Expressions;

namespace OGCP.Curriculum.API.services;

public class GeneralProfileService : IGeneralProfileService
{
    private readonly IGeneralProfileWriteRepo repository;

    public GeneralProfileService(IGeneralProfileWriteRepo repository)
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

    //public async Task<Result> Create(GeneralProfile request)
    //{

    //    var result = this.repository.Add(request);

    //    if (result.IsFailure)
    //    {
    //        throw new ArgumentException();
    //    }

    //    var resultSave = repository.SaveChanges();

    //    return Result.Success();
    //}

    public Task<IReadOnlyList<GeneralProfile>> Get()
    {
        //return this.repository.Find();
        return null;
    }

    public Task<GeneralProfile> Get(int id)
    {
        return null;
        //return this.repository.Find(id);
    }
}
