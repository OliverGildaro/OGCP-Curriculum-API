using ArtForAll.Shared.ErrorHandler;

namespace OGCP.Curriculum.API.services.interfaces;

public interface IService<TEntity, TEntityId>
    where TEntity : class
{
    public Task<IReadOnlyList<TEntity>> Get();
    public Task<TEntity> Get(TEntityId id);
    public Task<Result> Create(TEntity request);
}

