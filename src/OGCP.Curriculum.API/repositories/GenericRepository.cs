using Microsoft.EntityFrameworkCore;
using OGCP.Curriculum.API.models;
using OGCP.Curriculum.API.repositories.interfaces;

namespace OGCP.Curriculum.API.repositories;

public abstract class GenericRepository<TEntity, TEntityId> : IRepository<TEntity, TEntityId>
    where TEntity : class, IEntity<TEntityId>
{
    private DbContext context;

    protected GenericRepository(DbContext context)
    {
        this.context = context;
    }

    public void Add(TEntity entity)
    {
        try
        {
            this.context.Add<TEntity>(entity);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public IEnumerable<TEntity> Find()
    {
        var result = this.context.Set<TEntity>()
            .AsNoTracking()
            .ToList();
        return result;
    }

    public TEntity Find(TEntityId entityId)
    {
        var result = this.context.Set<TEntity>()
            .FirstOrDefault(entity => entity.Id.Equals(entityId));
        return result;
    }

    public void SaveChanges()
    {
        try
        {
            this.context.SaveChanges();
        }
        catch (Exception ex)
        {

            throw;
        }

    }
}
