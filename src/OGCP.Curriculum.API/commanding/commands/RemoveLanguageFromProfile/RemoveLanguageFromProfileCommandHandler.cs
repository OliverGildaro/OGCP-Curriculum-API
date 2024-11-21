using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.commanding.commands.AddLanguageToProfile;

public class RemoveLanguageFromProfileCommandHandler : ICommandHandler<RemoveLangueFromProfileCommand, Result>
{
    private readonly IProfileService profileService;

    public RemoveLanguageFromProfileCommandHandler(IProfileService profileService)
    {
        this.profileService = profileService;
    }

    public Task<Result> HandleAsync(RemoveLangueFromProfileCommand command)
    {
        return profileService.RemoveLanguage(command.Id, command.LanguageId);
    }
}
