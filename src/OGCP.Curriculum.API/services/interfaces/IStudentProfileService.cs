using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.services.interfaces
{
    public interface IStudentProfileService : IService<StudentProfile, int>
    {
        Task<Result> AddEducation(int id, ResearchEducation request);
    }
}
