using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.repositories.interfaces
{
    public interface IStudentProfileRepository : IRepository<StudentProfile, int>
    {
        Task<int> RemoveOrphanEducations(string removeEducation);
    }
}
