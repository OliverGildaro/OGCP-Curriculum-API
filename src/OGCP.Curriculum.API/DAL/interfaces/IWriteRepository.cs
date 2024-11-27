using ArtForAll.Shared.ErrorHandler.Maybe;

namespace OGCP.Curriculum.API.repositories.interfaces;

public interface IWriteRepository<TEntity, TEntityId>
    where TEntity : class
{
    //Result Add(TEntity entity);
    Task<int> SaveChangesAsync();
    Task<Maybe<TEntity>> FindAsync(TEntityId id);
}
