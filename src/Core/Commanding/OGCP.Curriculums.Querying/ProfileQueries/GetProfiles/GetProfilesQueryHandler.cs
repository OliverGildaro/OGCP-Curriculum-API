using ArtForAll.Shared.Contracts.CQRS;
using OGCP.Curriculum.API.DAL.Queries.interfaces;
using OGCP.Curriculum.API.DAL.Queries.Models;

namespace OGCP.Curriculum.API.Querying.GetProfiles;

public class GetProfilesQueryHandler
    : IQueryHandler<GetProfilesQuery, IReadOnlyList<ProfileReadModel>>
{
    private readonly IProfileReadModelRepository repository;

    public GetProfilesQueryHandler(IProfileReadModelRepository repository)
    {
        this.repository = repository;
    }

    public Task<IReadOnlyList<ProfileReadModel>> Handle(GetProfilesQuery query)
    {
        return repository.FindAsync(query.Parameters);
    }
}
