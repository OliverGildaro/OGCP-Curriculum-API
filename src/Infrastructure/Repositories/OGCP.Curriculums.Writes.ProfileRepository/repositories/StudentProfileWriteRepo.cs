using ArtForAll.Shared.ErrorHandler;
using ArtForAll.Shared.ErrorHandler.Maybe;
using Microsoft.EntityFrameworkCore;
using OGCP.Curriculum.API.DAL.Mutations.context;
using OGCP.Curriculum.API.DAL.Mutations.Interfaces;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.DAL.Mutations;

public class StudentProfileWriteRepo : IStudentProfileWriteRepo
{
    private readonly DbWriteProfileContext context;

    public StudentProfileWriteRepo(DbWriteProfileContext context)
    {
        this.context = context;
    }

    public Result Add(StudentProfile entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Maybe<StudentProfile>> FindAsync(int id)
    {
        return await this.context.StudentProfiles.FindAsync(id);
    }

    public Task<int> RemoveOrphanEducationsAsync(string removeEducation)
    {
        return this.context.Database.ExecuteSqlRawAsync(removeEducation);
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
