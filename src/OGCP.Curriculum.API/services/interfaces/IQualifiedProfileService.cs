using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.services.interfaces
{
    public interface IQualifiedProfileService : IService<QualifiedProfile, int>
    {
        Task<Result> AddEducationAsync(int id, Education education);
        Task<Result> RemoveEducationAsync(int id, int languageId);
        Task<Result> UpdateEducationAsync(int profileId, Education education);
        //Task<Result> AddJobExperience<T>(int id, T request);
    }
}
