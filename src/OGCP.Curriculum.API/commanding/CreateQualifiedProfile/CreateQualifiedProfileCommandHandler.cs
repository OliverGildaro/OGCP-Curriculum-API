using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.ErrorHandler;
using Azure.Core;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.commanding.CreateQualifiedProfile
{
    public class CreateQualifiedProfileCommandHandler : ICommandHandler<CreateQualifiedProfileCommand, Result>
    {
        private readonly IQualifiedProfileService profileService;

        public CreateQualifiedProfileCommandHandler(IQualifiedProfileService profileService)
        {
            this.profileService = profileService;
        }
        public Task<Result> HandleAsync(CreateQualifiedProfileCommand command)
        {
            (string firstName, string lastName, string summary, string desiredJobRole) = command;
            var qualified = QualifiedProfile.Create(firstName, lastName, summary, desiredJobRole);
            return this.profileService.Create(qualified.Value);
        }
    }
}
