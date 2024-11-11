namespace OGCP.Curriculum.API.repositories.interfaces;

public interface IWriteRepository<T>
{
    void Add(T entity);
}
