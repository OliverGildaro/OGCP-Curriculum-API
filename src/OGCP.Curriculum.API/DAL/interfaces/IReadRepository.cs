using ArtForAll.Shared.ErrorHandler.Maybe;
using OGCP.Curriculum.API.Helpers;

namespace OGCP.Curriculum.API.repositories.interfaces;

public interface IReadRepository<TEntity, TEntityId>
    where TEntity : class
{
    public Task<IReadOnlyList<TEntity>> Find(QueryParameters parameters);
    public Task<Maybe<TEntity>> Find(TEntityId id);
}
