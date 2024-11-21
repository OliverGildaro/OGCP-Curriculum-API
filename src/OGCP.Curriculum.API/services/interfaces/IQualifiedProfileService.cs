using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.services.interfaces
{
    public interface IQualifiedProfileService : IService<QualifiedProfile, int>
    {
        Task<Result> AddEducation(int id, DegreeEducation request);
        Task<Result> AddJobExperience<T>(int id, T request);
    }
}
