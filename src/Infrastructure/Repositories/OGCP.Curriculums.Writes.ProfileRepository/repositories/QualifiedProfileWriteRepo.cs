using ArtForAll.Shared.ErrorHandler;
using ArtForAll.Shared.ErrorHandler.Maybe;
using Microsoft.EntityFrameworkCore;
using OGCP.Curriculum.API.DAL.Mutations.context;
using OGCP.Curriculum.API.DAL.Mutations.Interfaces;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.DAL.Mutations;

public class QualifiedProfileWriteRepo : ProfileWriteRepo, IQualifiedProfileWriteRepo
{
    private readonly ApplicationWriteDbContext context;

    public QualifiedProfileWriteRepo(ApplicationWriteDbContext context)
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

    //To have some other methods to retrieve different shapes of qualified profiles
    //Is not a good idea, them we will have a conbinatory explosion of methods
    //And will be difficult to know which one to use and when
    public async Task<Maybe<QualifiedProfile>> FindAsync(int id)
    {
        //In order to protect the invariants of an agreggate we need to always load the full aggregate
        //Partial loading is not a good idea
        //return await this.context.QualifiedProfiles
        //    .Include(p => p.LanguagesSpoken)
        //    .Include(p => p.Educations)
        //    .AsSplitQuery()
        //    .FirstOrDefaultAsync(p => p.Id.Equals(id));

        //We can enable lazy loading for writes
        //And avoid including nav properties here
        return await this.context.QualifiedProfiles
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

    public async Task<Maybe<DegreeEducation>> FindDegreeEducation(DegreeEducation education)
    {
        return await this.context.DegreeEducations
            .FirstOrDefaultAsync(
                p => p.Institution.Equals(education.Institution)
                && p.Degree.Equals(education.Degree)
                && p.StartDate.Equals(education.StartDate)
                && p.EndDate.Equals(education.EndDate));
    }

    public async Task<Maybe<ResearchEducation>> FindResearchEducation(ResearchEducation education)
    {
        return await this.context.ResearchEducations
            .FirstOrDefaultAsync(
                p => p.Institution.Equals(education.Institution)
                && p.ProjectTitle.Equals(education.ProjectTitle)
                && p.Supervisor.Equals(education.Supervisor)
                && p.StartDate.Equals(education.StartDate)
                && p.EndDate.Equals(education.EndDate));
    }
}

