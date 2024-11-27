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

        public async Task<Result> AddLangue(int id, Language language)
        {
            Maybe<Profile> profile = await this.writeRepo.Find(id);

            if (profile.HasValue)
            {

            }
            var langAdded = profile.Value.AddLanguage(language);

            if (langAdded.IsFailure)
            {
                return langAdded;
            }
            await this.writeRepo.SaveChanges();
            return langAdded;
        }

        public async Task<Result> Create(Profile request)
        {
            var result = this.writeRepo.Add(request);

            if (result.IsFailure)
            {
                throw new ArgumentException();
            }

            var resultSave = await writeRepo.SaveChanges();

            return Result.Success();
        }

        public async Task<Result> EdiLanguage(int id, Language language)
        {
            Maybe<Profile> profile = await this.writeRepo.Find(id);

            if (profile.HasValue)
            {
                return Result.Failure($"The profile id: {id}, not found");
            }
            Result result = profile.Value.EditLanguage(language);

            if (result.IsFailure)
            {
                return result;
            }
            await this.writeRepo.SaveChanges();
            return result;
        }

        public Task<IReadOnlyList<Profile>> Get()
        {
            //return this.writeRepo.Find();
            return null;
        }

        public Task<Profile> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> RemoveLanguage(int id, int languageId)
        {
            Maybe<Profile> profile = await this.writeRepo.Find(id);

            Result result = profile.Value.RemoveLanguage(languageId);

            if (result.IsFailure)
            {
                return result;
            }
            await this.writeRepo.SaveChanges();

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
