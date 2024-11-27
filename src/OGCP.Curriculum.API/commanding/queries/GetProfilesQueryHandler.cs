using ArtForAll.Shared.Contracts.CQRS;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.commanding.queries
{
    public class GetProfilesQueryHandler : IQueryHandler<GetProfilesQuery, IReadOnlyList<Profile>>
    {
        private readonly IProfileService profileService;

        public GetProfilesQueryHandler(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        public Task<IReadOnlyList<Profile>> Handle(GetProfilesQuery query)
        {
            return this.profileService.GetAsync();
        }
    }
}
