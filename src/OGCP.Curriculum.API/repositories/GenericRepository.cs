using ArtForAll.Shared.ErrorHandler;
using Microsoft.EntityFrameworkCore;
using OGCP.Curriculum.API.domainmodel;
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

    public Result Add(TEntity entity)
    {
        try
        {
            var result = this.context.Add<TEntity>(entity);

            if(result.State == EntityState.Added)
            {
                return Result.Success();
            }
            return Result.Failure("");
        }
        catch (Exception ex)
        {
            return Result.Failure("");
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

    public Result SaveChanges()
    {
        try
        {
            var isSaved = this.context.SaveChanges();
            if (isSaved is > 0)
            {
                return Result.Success();
            }

            return Result.Failure("");
        }
        catch (Exception ex)
        {
            return Result.Failure("");
        }
    }
}
