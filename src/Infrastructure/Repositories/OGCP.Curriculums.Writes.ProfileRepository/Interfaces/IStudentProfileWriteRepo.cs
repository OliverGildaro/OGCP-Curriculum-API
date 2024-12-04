using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.DAL.Mutations.Interfaces;

public interface IStudentProfileWriteRepo : IWriteRepository<StudentProfile, int>
{
    Task<int> RemoveOrphanEducationsAsync(string removeEducation);
}
