using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.dtos;

namespace OGCP.Curriculum.API.services.interfaces
{
    public interface IQualifiedProfileService : IService<QualifiedProfile, int>
    {
        Task<Result> AddEducation(int id, CreateDegreeEducationRequest request);
        Task<Result> AddJobExperience<T>(int id, T request);
    }
}
