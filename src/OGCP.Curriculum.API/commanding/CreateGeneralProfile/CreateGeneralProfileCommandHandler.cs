using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.ErrorHandler;
using Azure.Core;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.commanding.CreateGeneralProfile
{
    public class CreateGeneralProfileCommandHandler : ICommandHandler<CreateGeneralProfileCommand, Result>
    {
        private readonly IGeneralProfileService profileService;

        public CreateGeneralProfileCommandHandler(IGeneralProfileService profileService)
        {
            this.profileService = profileService;
        }
        public Task<Result> HandleAsync(CreateGeneralProfileCommand command)
        {
            (string firstName, string lastName, string summary, string[] personalGoals) = command;
            var generalProfile = GeneralProfile.Create(firstName, lastName, summary, personalGoals);
            return this.profileService.Create(generalProfile.Value);
        }
    }
}
