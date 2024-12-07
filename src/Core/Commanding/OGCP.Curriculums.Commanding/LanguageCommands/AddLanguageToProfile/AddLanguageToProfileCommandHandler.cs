using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.ErrorHandler;
using ArtForAll.Shared.ErrorHandler.Maybe;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.commanding.commands.AddLanguageToProfile;

public class AddLanguageToProfileCommandHandler : ICommandHandler<AddLangueToProfileCommand, Result>
{
    private readonly IProfileService profileService;

    public AddLanguageToProfileCommandHandler(IProfileService profileService)
    {
        this.profileService = profileService;
    }

    public async Task<Result> HandleAsync(AddLangueToProfileCommand command)
    {
        var language = Language.Create(command.Name, command.Level);
        Maybe<Language> maybeLang = await this.profileService.FindByLanguageAsync(language);

        if (maybeLang.HasValue)
        {
            language = maybeLang.Value;
        }
        return await profileService.AddLangueAsync(command.Id, language);
    }
}
