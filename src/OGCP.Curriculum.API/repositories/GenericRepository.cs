using ArtForAll.Shared.ErrorHandler;
using Microsoft.EntityFrameworkCore;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.repositories.interfaces;
using System.Linq.Expressions;

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
            //this.context.Add() //the DbContext will discover the type to be added
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

    public async Task<IEnumerable<TEntity>> Find()
    {
        return await this.context.Set<TEntity>()
            .AsNoTracking()
            .ToListAsync();
    }

    public virtual Task<TEntity> Find(TEntityId id, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = context.Set<TEntity>();

        // Include specified navigation properties
        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return query.FirstOrDefaultAsync(entity => EF.Property<TEntityId>(entity, "Id").Equals(id));
    }

    public async Task<Result> SaveChanges()
    {
        try
        {
            //SaveChanges() method calls tge DetectChanges internally
            //So only after DetectChanges() is called the entities has state updated
            //this.context.ChangeTracker.DetectChanges();
            var isSaved = await this.context.SaveChangesAsync();
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
