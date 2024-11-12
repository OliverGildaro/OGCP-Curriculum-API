namespace OGCP.Curriculum.API.services.interfaces;

public interface IService<TEntity, TEntityId, TRequest>
    where TEntity : class
    where TRequest : class
{
    public IEnumerable<TEntity> Get();
    public TEntity Get(TEntityId id);
    public void Create(TRequest request);
}
