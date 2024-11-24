using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.Commanding.commands.RemoveEducationFromQualifiedProfile
{
    public class RemoveEducationFromQualifiedProfileCommandHandler : ICommandHandler<RemoveEducationFromQualifiedProfileCommand, Result>
    {
        private readonly IQualifiedProfileService profileService;

        public RemoveEducationFromQualifiedProfileCommandHandler(IQualifiedProfileService profileService)
        {
            this.profileService = profileService;
        }

        public Task<Result> HandleAsync(RemoveEducationFromQualifiedProfileCommand command)
        {
            return profileService.RemoveEducation(command.Id, command.EducationId);
        }
    }
}
