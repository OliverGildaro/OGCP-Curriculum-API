using ArtForAll.Shared.ErrorHandler;
using ArtForAll.Shared.ErrorHandler.Maybe;
using Microsoft.EntityFrameworkCore;
using OGCP.Curriculum.API.DAL.Mutations.Interfaces;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.repositories;

namespace OGCP.Curriculum.API.DAL.Mutations;

public class QualifiedProfileWriteRepo : ProfileWriteRepo, IQualifiedProfileWriteRepo
{
    private readonly DbProfileContext context;

    public QualifiedProfileWriteRepo(DbProfileContext context)
        :base(context)
    {
        this.context = context;
    }

    public async Task<Result> RemoveOrphanEducationsAsync(string removeEducation)
    {
        var isSaved = await this.context.Database.ExecuteSqlRawAsync(removeEducation);

        if (isSaved < 0)
        {
            return Result.Failure("");
        }
        return Result.Success();
    }

    public async Task<Maybe<QualifiedProfile>> FindAsync(int id)
    {
        return await this.context.QualifiedProfiles
            .Include(p => p.LanguagesSpoken)
            .Include(p => p.Educations)
            .FirstOrDefaultAsync(p => p.Id.Equals(id));
    }

    public Task<int> SaveChangesAsync()
    {
        return base.SaveChangesAsync();
    }

    public Result Add(QualifiedProfile entity)
    {
        return base.Add(entity);
    }
}

