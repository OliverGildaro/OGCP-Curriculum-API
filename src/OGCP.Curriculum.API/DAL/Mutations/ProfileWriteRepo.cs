using ArtForAll.Shared.ErrorHandler;
using ArtForAll.Shared.ErrorHandler.Maybe;
using Microsoft.EntityFrameworkCore;
using OGCP.Curriculum.API.DAL.Mutations.Interfaces;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.repositories;

namespace OGCP.Curriculum.API.DAL.Mutations;

public class ProfileWriteRepo : IProfileWriteRepo
{
    protected readonly DbProfileContext context;

    public ProfileWriteRepo(DbProfileContext context)
    {
        this.context = context;
    }
    public Result Add(Profile entity)
    {
        try
        {
            //this.context.Add() //the DbContext will discover the type to be added
            var result = this.context.Add<Profile>(entity);

            if (result.State == EntityState.Added)
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

    public Task<Maybe<Profile>> FindAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveChangesAsync()
    {
        try
        {
            //SaveChanges() method calls tge DetectChanges internally
            //So only after DetectChanges() is called the entities has state updated
            //this.context.ChangeTracker.DetectChanges();
            return this.context.SaveChangesAsync();
            //if (isSaved is > 0)
            //{
            //    return Result.Success();
            //}

            //return Result.Failure("");
        }
        catch (Exception ex)
        {
            return Task.FromResult(-1);
        }
    }

    public Task<Language?> FindLanguageByNameAndLevel(Languages name, ProficiencyLevel level)
    {
        return context.Set<Language>()
            .FirstOrDefaultAsync(l => l.Name == name && l.Level == level);
    }
}
