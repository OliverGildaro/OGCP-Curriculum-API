using OGCP.Curriculum.API.factories.interfaces;
using OGCP.Curriculum.API.repositories.interfaces;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.services;

public abstract class Service<TEntity, TRequest> : IService<TEntity, TRequest>
    where TEntity : class
    where TRequest : class
{
    private IRepository<TEntity> repository;
    private IFactory<TEntity, TRequest> factory;

    public Service(IRepository<TEntity> repository, IFactory<TEntity, TRequest> factory)
    {
        this.repository = repository;
        this.factory = factory;
    }

    public void Create(TRequest request)
    {
        throw new NotImplementedException();
    }

    public TEntity Get()
    {
        throw new NotImplementedException();
    }
}
