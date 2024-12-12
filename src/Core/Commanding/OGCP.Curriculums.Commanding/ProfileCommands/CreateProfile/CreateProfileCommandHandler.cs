using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.ErrorHandler;
using ArtForAll.Shared.ErrorHandler.Results;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.services.interfaces;
using OGCP.Curriculums.Core.DomainModel;

namespace OGCP.Curriculum.API.commanding.commands.CreateQualifiedProfile;

public class CreateProfileCommandHandler<TCommand, TResult> : ICommandHandler<TCommand, TResult>
    where TCommand : CreateProfileCommand
    where TResult : Result
{
    private readonly IProfileService profileService;

    public CreateProfileCommandHandler(IProfileService profileService)
    {
        this.profileService = profileService;
    }

    public async Task<TResult> HandleAsync(TCommand command)
    {
        IResult<Profile, Error> profileResult = command.MapTo();
        var profile = profileResult.Value;

        Result addEducationResult = await this.profileService.CreateAsync(profile);

        return (TResult)addEducationResult;
    }
}
