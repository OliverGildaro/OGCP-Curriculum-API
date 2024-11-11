namespace OGCP.Curriculum.API.repositories;

public interface IProfileWriteRepository<t>
{
    void Add(t entity);
}
