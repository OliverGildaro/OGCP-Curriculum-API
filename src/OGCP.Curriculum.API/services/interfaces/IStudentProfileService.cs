using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.models;

namespace OGCP.Curriculum.API.services.interfaces
{
    public interface IStudentProfileService : IService<StudentProfile, int, ProfileRequest>
    {
        void AddEducation(int id, CreateEducationRequest request);
        void AddLanguage(int id, CreateLanguageRequest languageRequest);
    }
}
