namespace OGCP.Curriculum.API.repositories;

public interface IWriteRepository<T>
{
    void Add(T entity);
}
