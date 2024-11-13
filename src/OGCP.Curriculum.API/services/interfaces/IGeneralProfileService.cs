using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.models;

namespace OGCP.Curriculum.API.services.interfaces
{
    public interface IGeneralProfileService : IService<GeneralProfile, int, ProfileRequest>
    {
        void AddLanguage(int id, CreateLanguageRequest languageRequest);
    }
}
