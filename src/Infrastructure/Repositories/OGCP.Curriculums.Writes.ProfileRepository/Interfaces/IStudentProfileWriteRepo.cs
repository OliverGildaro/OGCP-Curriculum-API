using ArtForAll.Shared.ErrorHandler.Maybe;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.DAL.Mutations.Interfaces;

public interface IStudentProfileWriteRepo : IWriteRepository<StudentProfile, int>
{
    Task<Maybe<ResearchEducation>> FindResearchEducation(string institution, string projectTitle);
    Task<int> RemoveOrphanEducationsAsync(string removeEducation);
}
