using ArtForAll.Shared.ErrorHandler;
using Microsoft.EntityFrameworkCore;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.repositories.interfaces;

namespace OGCP.Curriculum.API.repositories
{
    public class StudentProfileRepository : GenericRepository<StudentProfile, int>, IStudentProfileRepository
    {
        public StudentProfileRepository(DbProfileContext context)
            :base(context)
        {
            
        }

        public Task<int> RemoveOrphanEducations(string removeEducation)
        {
            return this.context.Database.ExecuteSqlRawAsync(removeEducation);
        }
    }
}
