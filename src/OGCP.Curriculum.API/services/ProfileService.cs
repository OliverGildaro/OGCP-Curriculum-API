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

            var langAdded = profile.AddLanguage(language);
            await this.repository.SaveChanges();
            return langAdded;
        }

        public Task<int> Create(Profile request)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Profile>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<Profile> Get(int id)
        {
            throw new NotImplementedException();
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
