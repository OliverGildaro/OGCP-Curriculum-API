namespace OGCP.Curriculum.API.repositories.interfaces;

public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T>
{
}