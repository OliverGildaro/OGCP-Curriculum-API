namespace OGCP.Curriculum.API.factories;

public interface IFactory<TEntity, TRequest>
{
    TEntity Get(TRequest request);
}
