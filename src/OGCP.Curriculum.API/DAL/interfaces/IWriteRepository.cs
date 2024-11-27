using ArtForAll.Shared.ErrorHandler;
using ArtForAll.Shared.ErrorHandler.Maybe;
using System.Linq.Expressions;

namespace OGCP.Curriculum.API.repositories.interfaces;

public interface IWriteRepository<TEntity, TEntityId>
    where TEntity : class
{
    Result Add(TEntity entity);
    Task<int> SaveChanges();
    Task<Maybe<TEntity>> Find(TEntityId id);
}
