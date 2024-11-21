using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.repositories.interfaces;
using OGCP.Curriculum.API.services.interfaces;
using System.Linq.Expressions;

namespace OGCP.Curriculum.API.services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository repository;

        public ProfileService(IProfileRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result> AddLangue(int id, Language language)
        {
            Profile profile = await this.repository.Find(id, this.GetQueryExpression());

            if (profile is null)
            {

            }
            Language languageFound = await this.repository.FindLanguageByNameAndLevel(language.Name, language.Level);
            var languageToAdd = languageFound ?? language;
            var langAdded = profile.AddLanguage(languageToAdd);

            if (langAdded.IsFailure)
            {
                return langAdded;
            }
            await this.repository.SaveChanges();
            return langAdded;
        }

        public Task<int> Create(Profile request)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> EdiLanguage(int id, Language language)
        {
            Profile profile = await this.repository.Find(id, this.GetQueryExpression());

            if (profile is null)
            {
                return Result.Failure($"The profile id: {id}, not found");
            }
            Result result = profile.EditLanguage(language);

            if (result.IsFailure)
            {
                return result;
            }
            await this.repository.SaveChanges();
            return result;
        }

        public Task<IReadOnlyList<Profile>> Get()
        {
            return this.repository.Find();
        }

        public Task<Profile> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> RemoveLanguage(int id, int languageId)
        {
            Profile profile = await this.repository.Find(id, this.GetQueryExpression());

            Result result = profile.RemoveLanguage(languageId);

            if (result.IsFailure)
            {
                return result;
            }
            await this.repository.SaveChanges();
            return result;
        }

        private Expression<Func<Profile, object>>[] GetQueryExpression()
        {
            return new Expression<Func<Profile, object>>[]
            {
                x => x.LanguagesSpoken,
                x => x.PersonalInfo,
            };
        }
    }
}
