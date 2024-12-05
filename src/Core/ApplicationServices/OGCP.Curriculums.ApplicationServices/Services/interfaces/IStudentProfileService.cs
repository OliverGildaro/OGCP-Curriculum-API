using ArtForAll.Shared.ErrorHandler;
using ArtForAll.Shared.ErrorHandler.Maybe;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.services.interfaces;

public interface IStudentProfileService : IService<StudentProfile, int>
{
    Task<Result> AddEducationAsync(int id, ResearchEducation request);
    Task<Result> UpdateEducationAsync(int profileId, int educationId, ResearchEducation education);
    Task<Result> RemoveEducationAsync(int profileId, int educationId);
    Task<Maybe<ResearchEducation>> FindResearchEducation(string institution, string projectTitle);
}
