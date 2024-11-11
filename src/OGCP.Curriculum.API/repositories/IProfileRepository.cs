namespace OGCP.Curriculum.API.repositories;

public interface IProfileRepository<T> : IProfileReadRepository<T>, IProfileWriteRepository<T>
{
}