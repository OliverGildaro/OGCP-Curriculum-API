namespace OGCP.Curriculum.API.factories.interfaces;

public interface IFactory<TEntity, TRequest>
{
    TEntity Get(TRequest request);
}
