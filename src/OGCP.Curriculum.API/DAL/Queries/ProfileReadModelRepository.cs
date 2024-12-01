using ArtForAll.Shared.ErrorHandler.Maybe;
using Microsoft.EntityFrameworkCore;
using OGCP.Curriculum.API.DAL.Queries.context;
using OGCP.Curriculum.API.DAL.Queries.interfaces;
using OGCP.Curriculum.API.DAL.Queries.Models;
using OGCP.Curriculum.API.DAL.Queries.utils;
using OGCP.Curriculum.API.DAL.Queries.utils.expand;
using OGCP.Curriculum.API.DAL.Queries.utils.pagination;
using System.Linq.Expressions;

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
            //IQUERYABLE
            //IQueryable works with expression trees
            //The LINQ expressin will be translated to sql expressions
            //We can builde the expressions trees dynamically in runtime
            //IENUMERABLE
            //IEnumerable works with delegates
            //That is compiled code
            var collection = context.Profiles as IQueryable<ProfileReadModel>;

            //Filtering
            if (!string.IsNullOrEmpty(parameters.FilterBy))
            {
                collection = collection.Where(c => c.Discriminator == parameters.FilterBy.Trim());
            }

            //Searching
            if (!string.IsNullOrEmpty(parameters.SearchBy))
            {
                collection = collection.Where(c => c.FirstName.Contains(parameters.SearchBy.Trim())
                    || c.LastName.Contains(parameters.SearchBy.Trim()));
            }

            //Ordering
            if (!string.IsNullOrWhiteSpace(parameters.OrderBy)
                && OrderFunctions.ContainsKey(parameters.OrderBy))
            {
                collection = parameters.Desc
                    ? collection.OrderByDescending(OrderFunctions[parameters.OrderBy])
	                : collection.OrderBy(OrderFunctions[parameters.OrderBy]);
            }

            if (parameters.Fields != null && parameters.Fields.Length != 0)
            {
                var projection = Expand.BuildProjection3(parameters.SelectFields);
                collection = collection.Select(projection);
            }

            //Paging
            var profiles = await PagedList<ProfileReadModel>.CreateAsync(collection,
                parameters.PageNumber, parameters.PageSize);

            return profiles;
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public async Task<Maybe<ProfileReadModel>> Find(int id)
    {
        try
        {
            var result = await this.context.Profiles
                .Include(p => p.Educations)
                //.Include(p => p.WorkExp)
                .Include(p => p.Languages)
                .AsSplitQuery()
                .FirstOrDefaultAsync(p => p.Id.Equals(id));
            return result;
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}
