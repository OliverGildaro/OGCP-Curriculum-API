using ArtForAll.Shared.ErrorHandler.Maybe;
using Microsoft.EntityFrameworkCore;
using OGCP.Curriculum.API.DAL.Queries.context;
using OGCP.Curriculum.API.DAL.Queries.interfaces;
using OGCP.Curriculum.API.DAL.Queries.Models;
using OGCP.Curriculum.API.Helpers;
using OGCP.Curriculum.API.Helpers.pagination;

namespace OGCP.Curriculum.API.DAL.Queries;

public class ProfileReadModelRepository : IProfileReadModelRepository
{
    private readonly DbReadProfileContext context;

    public ProfileReadModelRepository(DbReadProfileContext profileContext)
    {
        this.context = profileContext;
    }
    public async Task<IReadOnlyList<ProfileReadModel>> Find(QueryParameters parameters)
    {
        try
        {
            var collection = context.Profiles as IQueryable<ProfileReadModel>;
            if (!string.IsNullOrEmpty(parameters.Filter))
            {
                collection = collection.Where(c => c.Discriminator == parameters.Filter.Trim());
            }

            if (!string.IsNullOrEmpty(parameters.Filter))
            {
                collection = collection.Where(c => c.FirstName.Contains(parameters.SearchBy.Trim())
                    || c.LastName.Contains(parameters.SearchBy.Trim()));
            }

            return await PagedList<ProfileReadModel>.CreateAsync(collection,
                parameters.Page, parameters.Size);
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
