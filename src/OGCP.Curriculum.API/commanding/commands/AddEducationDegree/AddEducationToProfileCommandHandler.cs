using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.commanding.commands.AddEducationDegree;

public class AddEducationToProfileCommandHandler<TCommand, TResult>
    : ICommandHandler<TCommand, TResult>
    where TCommand : AddEducationToProfileCommand
    where TResult : Result
{
    private readonly IQualifiedProfileService profileService;

    public AddEducationToProfileCommandHandler(IQualifiedProfileService profileService)
    {
        this.profileService = profileService;
    }

    public async Task<TResult> HandleAsync(TCommand command)
    {
        var result = command.MapTo();
        var addEducationResult = await this.profileService.AddEducation(command.Id, result.Value);

        // Return TResult if needed (you might need to cast or map it here)
        return (TResult)(object)addEducationResult; // Explicit cast if TResult is Result
    }
}
