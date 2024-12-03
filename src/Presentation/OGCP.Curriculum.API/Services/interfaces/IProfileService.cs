using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.services.interfaces
{
    public interface IProfileService : IService<Profile, int>
    {
        public Task<Result> AddLangueAsync(int id, Language language);
        Task<Result> EdiLanguageAsync(int id, Language language);
        Task<Result> RemoveLanguageAsync(int id, int languageId);
        Task<Result> CreateAsync(Profile request);
        Task<Result> UpdateAsync(Profile profile);
        Task<Result> DeleteProfile(int id);
    }
}
