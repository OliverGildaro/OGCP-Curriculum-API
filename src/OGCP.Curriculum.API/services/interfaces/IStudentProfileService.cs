using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.dtos;

namespace OGCP.Curriculum.API.services.interfaces
{
    public interface IStudentProfileService : IService<StudentProfile, int, CreateStudentProfileRequest>
    {
        Result AddEducation(int id, CreateResearchEducationRequest request);
        void AddLanguage(int id, CreateLanguageRequest languageRequest);
    }
}
