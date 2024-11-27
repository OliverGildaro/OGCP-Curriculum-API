//using ArtForAll.Shared.ErrorHandler;
//using Microsoft.EntityFrameworkCore;
//using OGCP.Curriculum.API.domainmodel;
//using OGCP.Curriculum.API.repositories.interfaces;
//using System.Linq.Expressions;

//namespace OGCP.Curriculum.API.DAL.Mutations;

//public abstract class GenericRepository<TEntity, TEntityId> : IWriteRepository<TEntity, TEntityId>
//    , IReadRepository<TEntity, TEntityId>
//    where TEntity : class, IEntity<TEntityId>
//{
//    protected DbContext context;

//    protected GenericRepository(DbContext context)
//    {
//        this.context = context;
//    }

//    public Result Add(TEntity entity)
//    {
//        try
//        {
//            //this.context.Add() //the DbContext will discover the type to be added
//            var result = context.Add(entity);

//            if (result.State == EntityState.Added)
//            {
//                return Result.Success();
//            }
//            return Result.Failure("");
//        }
//        catch (Exception ex)
//        {
//            return Result.Failure("");
//        }
//    }

//    public async Task<IReadOnlyList<TEntity>> Find()
//    {
//        return await context.Set<TEntity>()
//            .AsNoTracking()
//            .ToListAsync();
//    }

//    public virtual Task<TEntity> Find(TEntityId id, params Expression<Func<TEntity, object>>[] includes)
//    {
//        IQueryable<TEntity> query = context.Set<TEntity>();

//        // Include specified navigation properties
//        foreach (var include in includes)
//        {
//            query = query.Include(include);
//            //.ThenInclude(b => b.??)Is to get the children on an include nav property
//        }

//        return query.FirstOrDefaultAsync(entity => EF.Property<TEntityId>(entity, "Id").Equals(id));
//    }

//    public Task<TEntity> Find(TEntityId id)
//    {
//        return context.Set<TEntity>()
//            .FirstOrDefaultAsync(p => p.Id.Equals(id));
//    }

//    public Task<int> SaveChanges()
//    {
//        try
//        {
//            //SaveChanges() method calls tge DetectChanges internally
//            //So only after DetectChanges() is called the entities has state updated
//            //this.context.ChangeTracker.DetectChanges();
//            return context.SaveChangesAsync();
//            //if (isSaved is > 0)
//            //{
//            //    return Result.Success();
//            //}

//            //return Result.Failure("");
//        }
//        catch (Exception ex)
//        {
//            return Task.FromResult(-1);
//        }
//    }
//}
