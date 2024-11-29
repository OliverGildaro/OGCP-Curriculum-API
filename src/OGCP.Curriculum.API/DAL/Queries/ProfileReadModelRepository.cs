using ArtForAll.Shared.ErrorHandler.Maybe;
using Microsoft.EntityFrameworkCore;
using OGCP.Curriculum.API.DAL.Queries.context;
using OGCP.Curriculum.API.DAL.Queries.interfaces;
using OGCP.Curriculum.API.DAL.Queries.Models;
using OGCP.Curriculum.API.DAL.Queries.utils;
using OGCP.Curriculum.API.DAL.Queries.utils.pagination;
using System.Linq;

namespace OGCP.Curriculum.API.DAL.Queries;

public class ProfileReadModelRepository : IProfileReadModelRepository
{
    private readonly DbReadProfileContext context;

    private readonly Dictionary<string, IOrderBy> OrderFunctions =
        new Dictionary<string, IOrderBy>
        {
                    { "FirstName", new OrderBy<string>(x => x.FirstName) },
                    { "LastName",  new OrderBy<string>(x => x.LastName) },
                    { "Summary",   new OrderBy<string>(x => x.Summary) },
                    { "CareerGoals",   new OrderBy<string>(x => x.CareerGoals) },
                    { "DesiredJobRole",   new OrderBy<string>(x => x.DesiredJobRole) },
                    { "Discriminator",   new OrderBy<string>(x => x.Discriminator) },
        };

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

            if (!string.IsNullOrWhiteSpace(parameters.OrderBy)
                && OrderFunctions.ContainsKey(parameters.OrderBy))
            {
                collection = parameters.Desc
                    ? collection.OrderByDescending(OrderFunctions[parameters.OrderBy])
	                : collection.OrderBy(OrderFunctions[parameters.OrderBy]);
            }

            return await PagedList<ProfileReadModel>.CreateAsync(collection,
                parameters.PageNumber, parameters.PageSize);
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
