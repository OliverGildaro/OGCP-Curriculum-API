using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.commanding.commands.EditLanguageFromProfile;

public class UpdateLanguageFromProfileCommandHandler : ICommandHandler<UpdateLanguageFromProfileCommand, Result>
{
    private readonly IProfileService profileService;

    public UpdateLanguageFromProfileCommandHandler(IProfileService profileService)
    {
        this.profileService = profileService;
    }

    public Task<Result> HandleAsync(UpdateLanguageFromProfileCommand command)
    {
        var language = Language.Hidrate(command.Name, command.Level, command.LanguageId);
        return profileService.EdiLanguageAsync(command.Id, language);
    }
}
