using ArtForAll.Shared.ErrorHandler;
using ArtForAll.Shared.ErrorHandler.Maybe;
using Microsoft.EntityFrameworkCore;
using OGCP.Curriculum.API.DAL.Mutations.context;
using OGCP.Curriculum.API.DAL.Mutations.Interfaces;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.DAL.Mutations;

public class StudentProfileWriteRepo : IStudentProfileWriteRepo
{
    private readonly ApplicationWriteDbContext context;

    public StudentProfileWriteRepo(ApplicationWriteDbContext context)
    {
        this.context = context;
    }

    public Result Add(StudentProfile entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Maybe<StudentProfile>> FindAsync(int id)
    {
        //We need always load all nav properties
        //Because if we only load the required nav property for a business logic escenario
        //We add business complexity about if all the required data has been loaded
        //Increase chances of bugs and data corruption
        //return await this.context.StudentProfiles
        //    .Include(p => p.Educations)
        //    .Include(p => p.LanguagesSpoken)
        //    .AsSplitQuery()
        //    .FirstOrDefaultAsync(p => p.Id.Equals(id));

        return await this.context.StudentProfiles
            .FirstOrDefaultAsync(p => p.Id.Equals(id));
    }

    public Task<int> RemoveOrphanEducationsAsync(string removeEducation)
    {
        return this.context.Database.ExecuteSqlRawAsync(removeEducation);
    }

    public async Task<Maybe<ResearchEducation>> FindResearchEducation(string institution, string projectTitle)
    {
        return await this.context.ResearchEducations
            .FirstOrDefaultAsync(p => p.Institution.Equals(institution) && p.ProjectTitle.Equals(projectTitle));
    }

    public Task<int> SaveChangesAsync()
    {
        try
        {
            return this.context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return Task.FromResult(-1);
        }
    }
}
