using ArtForAll.Shared.ErrorHandler;
using ArtForAll.Shared.ErrorHandler.Maybe;
using Microsoft.EntityFrameworkCore;
using OGCP.Curriculum.API.DAL.Mutations.context;
using OGCP.Curriculum.API.DAL.Mutations.Interfaces;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.DAL.Mutations;

public class GeneralProfileWriteRepo : IGeneralProfileWriteRepo
{
    private readonly ApplicationWriteDbContext context;

    public GeneralProfileWriteRepo(ApplicationWriteDbContext context)
    {
        this.context = context;
    }

    public Result Add(GeneralProfile entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Maybe<GeneralProfile>> FindAsync(int id)
    {
        var profile = await this.context.GeneralProfiles
            .FirstOrDefaultAsync(p => p.Id == id);
        return profile;
    }

    public Task<int> SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}
