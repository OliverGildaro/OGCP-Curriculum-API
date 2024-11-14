using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.dtos.requests;
using OGCP.Curriculum.API.models;

namespace OGCP.Curriculum.API.services.interfaces
{
    public interface IQualifiedProfileService : IService<QualifiedProfile, int, CreateQualifiedProfileRequest>
    {
        void AddEducation(int id, CreateDegreeEducationRequest request);
        void AddJobExperience<T>(int id, T request);
        void AddLanguage(int id, CreateLanguageRequest languageRequest);
    }
}
