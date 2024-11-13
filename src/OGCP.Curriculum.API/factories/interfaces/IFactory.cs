namespace OGCP.Curriculum.API.factories.interfaces;

public interface IFactory<out TEntity, TRequest>
{
    TEntity Get(TRequest request);
}
