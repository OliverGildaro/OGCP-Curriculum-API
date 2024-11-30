using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.Commanding.commands.DeleteProfile;

public class DeleteProfileCommandHandler : ICommandHandler<DeleteProfileCommand, Result>
{
    private readonly IProfileService profileService;

    public DeleteProfileCommandHandler(IProfileService profileService)
    {
        this.profileService = profileService;
    }
    public Task<Result> HandleAsync(DeleteProfileCommand command)
    {
        return this.profileService.DeleteProfile(command.Id);
    }
}
