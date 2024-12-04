using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.ErrorHandler.Maybe;
using OGCP.Curriculum.API.DAL.Queries.Models;

namespace OGCP.Curriculum.API.Querying.GetProfileById;

public class GetProfileByIdQuery : IQuery<Maybe<ProfileReadModel>>
{
    public int Id { get; set; }
}
