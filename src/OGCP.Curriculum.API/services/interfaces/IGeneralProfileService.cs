using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.dtos;

namespace OGCP.Curriculum.API.services.interfaces
{
    public interface IGeneralProfileService : IService<GeneralProfile, int>
    {
        Task<Result> AddLanguage(int id, CreateLanguageRequest languageRequest);
    }
}
