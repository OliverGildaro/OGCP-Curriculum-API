using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.ErrorHandler.Maybe;
using OGCP.Curriculum.API.DAL.Queries.interfaces;
using OGCP.Curriculum.API.DAL.Queries.Models;

namespace OGCP.Curriculum.API.Querying.GetProfileById;

public class GetProfileByIdQueryHandler : IQueryHandler<GetProfileByIdQuery, Maybe<ProfileReadModel>>
{
    private readonly IProfileReadModelRepository repository;

    public GetProfileByIdQueryHandler(IProfileReadModelRepository repository)
    {
        this.repository = repository;
    }
    public Task<Maybe<ProfileReadModel>> Handle(GetProfileByIdQuery query)
    {
        return this.repository.FindAsync(query.Id);
    }
}
