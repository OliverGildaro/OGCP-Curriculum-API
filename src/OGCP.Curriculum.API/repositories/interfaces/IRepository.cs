using OGCP.Curriculum.API.models;

namespace OGCP.Curriculum.API.repositories.interfaces;

public interface IRepository<TEntity, TTEntityId>
    : IReadRepository<TEntity, TTEntityId>, IWriteRepository<TEntity>
    where TEntity : class
{
}