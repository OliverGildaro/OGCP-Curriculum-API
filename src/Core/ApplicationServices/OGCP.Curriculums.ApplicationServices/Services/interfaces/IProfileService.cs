using ArtForAll.Shared.ErrorHandler;
using ArtForAll.Shared.ErrorHandler.Maybe;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculums.Core.DomainModel;
using OGCP.Curriculums.Core.DomainModel.valueObjects;

namespace OGCP.Curriculum.API.services.interfaces
{
    public interface IProfileService : IService<Profile, int>
    {
        public Task<Result> AddLangueAsync(int id, Language language);
        Task<Result> EdiLanguageAsync(int id, int languageId, Language language);
        Task<Result> RemoveLanguageAsync(int id, int languageId);
        Task<Result> CreateAsync(Profile request);
        Task<Result> UpdateAsync(Profile profile);
        Task<Result> DeleteProfile(int id);
        Task<Maybe<Language>> FindByLanguageAsync(Language language);
        Task<Result> AddLanguageSkillAsync(int profileId, int educationId, LanguageSkill skill);
    }
}
