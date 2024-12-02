using ArtForAll.Shared.ErrorHandler.Maybe;
using Microsoft.EntityFrameworkCore;
using OGCP.Curriculum.API.DAL.Queries.interfaces;
using OGCP.Curriculum.API.DAL.Queries.Models;
using OGCP.Curriculum.API.DAL.Queries.utils;
using OGCP.Curriculum.API.DAL.Queries.utils.expand;

namespace OGCP.CSharpFeatures.Expressions;

internal class FakeRepo : IProfileReadModelRepository
{
    IQueryable<ProfileReadModel> profileReadList = new List<ProfileReadModel>
        {
            new ProfileReadModel
            {
                FirstName="Oliver",
                LastName="Castro",
                Summary="Backend"
            },
            new ProfileReadModel
            {
                FirstName="Alvaro",
                LastName="Castro",
                Summary="Backend"
            },
            new ProfileReadModel
            {
                FirstName="Ivan",
                LastName="Castro",
                Summary="Backend"
            },
        }.AsQueryable();

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

    public FakeRepo()
    {
    }

    public async Task<IReadOnlyList<ProfileReadModel>> Find(QueryParameters parameters)
    {
        try
        {

            if (!string.IsNullOrWhiteSpace(parameters.OrderBy))
            {
                profileReadList = profileReadList
                            .OrderBy(OrderFunctions[parameters.OrderBy]);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Fields))
            {
                var projection = Expand.BuildProjection3(parameters.SelectFields);
                profileReadList = profileReadList.Select(projection);
            }

            var result = profileReadList.ToList();

            return await Task.FromResult(result);
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public Task<Maybe<ProfileReadModel>> Find(int id)
    {
        throw new NotImplementedException();
    }
}