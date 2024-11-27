using ArtForAll.Shared.ErrorHandler;
using ArtForAll.Shared.ErrorHandler.Maybe;
using OGCP.Curriculum.API.DAL.Mutations.Interfaces;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.services.interfaces;
using System.Linq.Expressions;

namespace OGCP.Curriculum.API.services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileWriteRepo writeRepo;

        public ProfileService(IProfileWriteRepo writeRepo)
        {
            this.writeRepo = writeRepo;
        }

        public async Task<Result> AddLangueAsync(int id, Language language)
        {
            Maybe<Profile> profile = await this.writeRepo.FindAsync(id);

            if (profile.HasValue)
            {

            }
            var langAdded = profile.Value.AddLanguage(language);

            if (langAdded.IsFailure)
            {
                return langAdded;
            }
            await this.writeRepo.SaveChangesAsync();
            return langAdded;
        }

        public async Task<Result> CreateAsync(Profile request)
        {
            var result = this.writeRepo.Add(request);

            if (result.IsFailure)
            {
                throw new ArgumentException();
            }

            var resultSave = await writeRepo.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<Result> EdiLanguageAsync(int id, Language language)
        {
            Maybe<Profile> profile = await this.writeRepo.FindAsync(id);

            if (profile.HasNoValue)
            {
                return Result.Failure($"The profile id: {id}, not found");
            }
            Result result = profile.Value.EditLanguage(language);

            if (result.IsFailure)
            {
                return result;
            }
            await this.writeRepo.SaveChangesAsync();
            return result;
        }

        public Task<IReadOnlyList<Profile>> GetAsync()
        {
            //return this.writeRepo.Find();
            return null;
        }

        public Task<Profile> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> RemoveLanguageAsync(int id, int languageId)
        {
            Maybe<Profile> profile = await this.writeRepo.FindAsync(id);

            Result result = profile.Value.RemoveLanguage(languageId);

            if (result.IsFailure)
            {
                return result;
            }
            await this.writeRepo.SaveChangesAsync();

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
