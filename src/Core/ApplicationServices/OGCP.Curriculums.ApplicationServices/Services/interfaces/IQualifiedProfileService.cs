using ArtForAll.Shared.ErrorHandler;
using ArtForAll.Shared.ErrorHandler.Maybe;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.services.interfaces
{
    public interface IQualifiedProfileService : IService<QualifiedProfile, int>
    {
        Task<Result> AddEducationAsync(int id, Education education);
        Task<Maybe<DegreeEducation>> FindDegreeEducation(string institution, EducationLevel degree);
        Task<Maybe<DegreeEducation>> FindDegreeEducation(DegreeEducation degreeToUpdate);
        Task<Maybe<ResearchEducation>> FindResearchEducation(string institution, string projectTitle);
        Task<Result> RemoveEducationAsync(int id, int languageId);
        Task<Result> UpdateEducationAsync(int profileId, int educationId, Education education);
        //Task<Result> AddJobExperience<T>(int id, T request);
    }
}
