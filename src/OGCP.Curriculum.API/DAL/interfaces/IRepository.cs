using ArtForAll.Shared.ErrorHandler;

namespace OGCP.Curriculum.API.repositories.interfaces;

public interface IRepository<TEntity, TEntityId>
    : IReadRepository<TEntity, TEntityId>, IWriteRepository<TEntity>
    where TEntity : class
{
}