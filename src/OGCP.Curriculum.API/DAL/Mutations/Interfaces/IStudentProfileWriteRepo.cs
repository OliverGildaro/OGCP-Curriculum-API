using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.repositories.interfaces;

namespace OGCP.Curriculum.API.DAL.Mutations.Interfaces;

public interface IStudentProfileWriteRepo : IWriteRepository<StudentProfile, int>
{
    Task<int> RemoveOrphanEducations(string removeEducation);
}
