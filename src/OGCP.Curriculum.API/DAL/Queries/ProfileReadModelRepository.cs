using ArtForAll.Shared.ErrorHandler.Maybe;
using Microsoft.EntityFrameworkCore;
using OGCP.Curriculum.API.DAL.Queries.context;
using OGCP.Curriculum.API.DAL.Queries.interfaces;
using OGCP.Curriculum.API.DAL.Queries.Models;

namespace OGCP.Curriculum.API.DAL.Queries;

public class ProfileReadModelRepository : IProfileReadModelRepository
{
    private readonly DbReadProfileContext context;

    public ProfileReadModelRepository(DbReadProfileContext profileContext)
    {
        this.context = profileContext;
    }
    public async Task<IReadOnlyList<ProfileReadModel>> Find()
    {
        try
        {
            return await this.context.Profiles.ToListAsync();
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public async Task<Maybe<ProfileReadModel>> Find(int id)
    {
        return await this.context.Profiles
            .FindAsync(id);
    }
}
