using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.ErrorHandler;
using ArtForAll.Shared.ErrorHandler.Maybe;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.commanding.commands.AddEducationDegree;

//Generics are invariant by default
//Covariance and contravariance is supported only for interfaces and delegates
public class AddResearchEducationToQualifiedProfileCommandHandler
    : ICommandHandler<AddResearchEducationToQualifiedProfileCommand, Result>
{
    private readonly IQualifiedProfileService qualifiedService;

    public AddResearchEducationToQualifiedProfileCommandHandler(IQualifiedProfileService qualifiedService)
    {
        this.qualifiedService = qualifiedService;
    }

    public async Task<Result> HandleAsync(AddResearchEducationToQualifiedProfileCommand command)
    {

        (int id, string institution, DateOnly startDate, DateOnly? endDate, string projectTitle, string supervisor, string summary)
            = command;
        var researchResult = ResearchEducation.Create(institution, startDate, endDate, projectTitle, supervisor, summary);

        if (researchResult.IsFailure)
        {
            return Result.Failure("");
        }
        var researchToAdd = researchResult.Value;
        Maybe<ResearchEducation> maybeResearch = await this.qualifiedService
            .FindResearchEducation(researchToAdd);

        if (maybeResearch.HasValue)
        {
            researchToAdd = maybeResearch.Value;
        }


        return await this.qualifiedService.AddEducationAsync(command.ProfileId, researchToAdd);
    }
}
