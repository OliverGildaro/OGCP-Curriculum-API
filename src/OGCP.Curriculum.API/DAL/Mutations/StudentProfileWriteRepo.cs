using ArtForAll.Shared.ErrorHandler;
using ArtForAll.Shared.ErrorHandler.Maybe;
using Microsoft.EntityFrameworkCore;
using OGCP.Curriculum.API.DAL.Mutations.Interfaces;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.repositories;

namespace OGCP.Curriculum.API.DAL.Mutations;

public class StudentProfileWriteRepo : IStudentProfileWriteRepo
{
    private readonly DbProfileContext context;

    public StudentProfileWriteRepo(DbProfileContext context)
    {
        this.context = context;
    }

    public Result Add(StudentProfile entity)
    {
        throw new NotImplementedException();
    }

    public Task<Maybe<StudentProfile>> Find(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> RemoveOrphanEducations(string removeEducation)
    {
        return this.context.Database.ExecuteSqlRawAsync(removeEducation);
    }

    public Task<int> SaveChanges()
    {
        throw new NotImplementedException();
    }
}
