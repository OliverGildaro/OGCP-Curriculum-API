using OGCP.Curriculum.API.factories.interfaces;
using OGCP.Curriculum.API.repositories.interfaces;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.services;

public abstract class GenericService<TEntity, TEntityId, TRequest> : IService<TEntity, TEntityId, TRequest>
    where TEntity : class
    where TRequest : class
{
    protected IRepository<TEntity, TEntityId> repository;
    protected IFactory<TEntity, TRequest> factory;

    public GenericService(IRepository<TEntity, TEntityId> repository, IFactory<TEntity, TRequest> factory)
    {
        this.repository = repository;
        this.factory = factory;
    }

    public void Create(TRequest request)
    {
        var entity = this.factory.Get(request);
        this.repository.Add(entity);
        this.repository.SaveChanges();
    }

    public TEntity Get(TEntityId id)
    {
        return this.repository.Find(id);
    }

    public IEnumerable<TEntity> Get()
    {
        return this.repository.Find();
    }
}
