using ArtForAll.Shared.ErrorHandler.Maybe;
using OGCP.Curriculum.API.DAL.Queries.utils;

namespace OGCP.Curriculum.API.DAL.Queries;

public interface IReadRepository<TEntity, TEntityId>
    where TEntity : class
{
    public Task<IReadOnlyList<TEntity>> FindAsync(QueryParameters parameters);
    public Task<Maybe<TEntity>> FindAsync(TEntityId id);
}
