using ArtForAll.Shared.ErrorHandler;
using ArtForAll.Shared.ErrorHandler.Maybe;
using Microsoft.EntityFrameworkCore;
using OGCP.Curriculum.API.DAL.Mutations.Interfaces;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.repositories;

namespace OGCP.Curriculum.API.DAL.Mutations;

public class GeneralProfileWriteRepo : IGeneralProfileWriteRepo
{
    private readonly DbProfileContext context;

    public GeneralProfileWriteRepo(DbProfileContext context)
    {
        this.context = context;
    }

    public Result Add(GeneralProfile entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Maybe<GeneralProfile>> Find(int id)
    {
        var profile = await this.context.GeneralProfiles
            .Include(p => p.LanguagesSpoken)
            .Include(p => p.Experiences)
            .AsSplitQuery()
            .FirstOrDefaultAsync(p => p.Id == id);
        return profile;
    }

    public Task<int> SaveChanges()
    {
        throw new NotImplementedException();
    }
}
