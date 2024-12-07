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

    public async Task<Result> HandleAsync(UpdateLanguageFromProfileCommand command)
    {
        var language = Language.Create(command.Name, command.Level);

        var maybeLang = await this.profileService.FindByLanguageAsync(language);

        if (maybeLang.HasValue)
        {
            language = maybeLang.Value;
        }

        return await profileService.EdiLanguageAsync(command.Id, command.LanguageId, language);
    }
}
