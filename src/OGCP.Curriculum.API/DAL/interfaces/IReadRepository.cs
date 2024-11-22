using System.Linq.Expressions;

namespace OGCP.Curriculum.API.repositories.interfaces;

public interface IReadRepository<TEntity, TEntityId>
    where TEntity : class
{
    public Task<IReadOnlyList<TEntity>> Find();
    public Task<TEntity> Find(TEntityId id, params Expression<Func<TEntity, object>>[] includes);
    public Task<TEntity> Find(TEntityId id);

}
