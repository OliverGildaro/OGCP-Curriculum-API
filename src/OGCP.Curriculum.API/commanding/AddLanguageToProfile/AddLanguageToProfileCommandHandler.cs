using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.commanding.AddLanguageToProfile;

public class AddLanguageToProfileCommandHandler : ICommandHandler<AddLangueToProfileCommand, Result>
{
    private readonly IProfileService profileService;

    public AddLanguageToProfileCommandHandler(IProfileService profileService)
    {
        this.profileService = profileService;
    }

    public Task<Result> HandleAsync(AddLangueToProfileCommand command)
    {
        var language = Language.Create(command.Name, command.Level);
        return this.profileService.AddLangue(command.Id, language);
    }
}
