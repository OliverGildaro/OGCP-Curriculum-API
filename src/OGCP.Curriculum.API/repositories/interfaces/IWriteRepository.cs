namespace OGCP.Curriculum.API.repositories.interfaces;

public interface IWriteRepository<TEntity>
    where TEntity : class
{
    void Add(TEntity entity);
    void SaveChanges();

}
