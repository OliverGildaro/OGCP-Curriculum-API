using ArtForAll.Shared.ErrorHandler;

namespace OGCP.Curriculum.API.repositories.interfaces;

public interface IWriteRepository<TEntity>
    where TEntity : class
{
    Result Add(TEntity entity);
    Task<int> SaveChanges();
}
