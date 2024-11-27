using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.services.interfaces;

public interface IStudentProfileService : IService<StudentProfile, int>
{
    Task<Result> AddEducationAsync(int id, ResearchEducation request);
    Task<Result> RemoveEducationAsync(int profileId, int educationId);
}
