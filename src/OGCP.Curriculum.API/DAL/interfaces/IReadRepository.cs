using ArtForAll.Shared.ErrorHandler.Maybe;

namespace OGCP.Curriculum.API.repositories.interfaces;

public interface IReadRepository<TEntity, TEntityId>
    where TEntity : class
{
    public Task<IReadOnlyList<TEntity>> Find();
    public Task<Maybe<TEntity>> Find(TEntityId id);
}
