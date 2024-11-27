using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.Commanding.commands.UpdateProfile;

public class UpdateProfileCommandHandler<TCommand, TResult> : ICommandHandler<TCommand, TResult>
    where TCommand : UpdateProfileCommand
    where TResult : Result
{
    private readonly IProfileService profileService;

    public UpdateProfileCommandHandler(IProfileService profileService)
    {
        this.profileService = profileService;
    }
    public async Task<TResult> HandleAsync(TCommand command)
    {
        var profileResult = command.MapTo();
        var result = await this.profileService.UpdateAsync(profileResult.Value);
        return (TResult)result;
    }
}
