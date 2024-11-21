using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.commanding.commands.EditLanguageFromProfile;

public class EditLanguageFromProfileCommandHandler : ICommandHandler<EditLangueFromProfileCommand, Result>
{
    private readonly IProfileService profileService;

    public EditLanguageFromProfileCommandHandler(IProfileService profileService)
    {
        this.profileService = profileService;
    }

    public Task<Result> HandleAsync(EditLangueFromProfileCommand command)
    {
        var language = Language.Hidrate(command.Name, command.Level, command.LanguageId);
        return profileService.EdiLanguage(command.Id, language);
    }
}
