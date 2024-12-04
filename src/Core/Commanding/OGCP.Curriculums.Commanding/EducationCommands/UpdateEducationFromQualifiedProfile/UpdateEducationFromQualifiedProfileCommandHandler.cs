using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.Contracts.DDD;
using ArtForAll.Shared.ErrorHandler;
using ArtForAll.Shared.ErrorHandler.Results;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.commanding.commands.UpdateEducationToQualifiedProfile;

public class UpdateEducationFromQualifiedProfileCommandHandler<TCommand, TResult>
    : ICommandHandler<TCommand, TResult>
    where TCommand : UpdateEducationFromQualifiedProfileCommand
    where TResult : Result
{
    private readonly IQualifiedProfileService qualifiedService;

    public UpdateEducationFromQualifiedProfileCommandHandler(IQualifiedProfileService qualifiedService)
    {
        this.qualifiedService = qualifiedService;
    }

    public async Task<TResult> HandleAsync(TCommand command)
    {
        IResult<Education, Error> educationResult = command.MapTo();
        var education = educationResult.Value;

        Result addEducationResult = await this.qualifiedService.UpdateEducationAsync(command.ProfileId, education);

        return (TResult)addEducationResult;
    }
}
