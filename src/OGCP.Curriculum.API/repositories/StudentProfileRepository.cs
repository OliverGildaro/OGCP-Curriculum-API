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
    }
}
