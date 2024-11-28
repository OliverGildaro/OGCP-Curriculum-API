using ArtForAll.Shared.Contracts.CQRS;
using OGCP.Curriculum.API.DAL.Queries.interfaces;
using OGCP.Curriculum.API.DAL.Queries.Models;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.Querying;

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
        return this.repository.Find(query.Parameters);
    }
}
