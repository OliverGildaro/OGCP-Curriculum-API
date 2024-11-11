namespace OGCP.Curriculum.API.repositories;

public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T>
{
}