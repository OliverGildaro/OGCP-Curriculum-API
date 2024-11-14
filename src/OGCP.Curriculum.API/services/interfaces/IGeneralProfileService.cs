using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.models;

namespace OGCP.Curriculum.API.services.interfaces
{
    public interface IGeneralProfileService : IService<GeneralProfile, int, CreateGeneralProfileRequest>
    {
        void AddLanguage(int id, CreateLanguageRequest languageRequest);
    }
}
