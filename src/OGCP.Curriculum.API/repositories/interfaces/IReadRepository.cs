namespace OGCP.Curriculum.API.repositories.interfaces;

public interface IReadRepository<TEntity, TEntityId>
    where TEntity : class
{
    public IEnumerable<TEntity> Find();
    public TEntity Find(TEntityId id);

}
