namespace OGCP.Curriculum.API.services;

public interface IService<tEntity, tRequest>
    where tEntity : class
    where tRequest : class
{
    public tEntity Get();
    public void Create(tRequest request);
}
