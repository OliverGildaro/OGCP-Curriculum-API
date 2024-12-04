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
        Maybe<ResearchEducation> maybeResearch = await this.qualifiedService
            .FindResearchEducation(command.Institution, command.ProjectTitle);

        if (maybeResearch.HasValue)
        {
            return await this.qualifiedService.AddEducationAsync(command.ProfileId, maybeResearch.Value);
        }

        (int id, string institution, DateTime startDate, DateTime? endDate, string projectTitle, string supervisor, string summary)
            = command;
        var researchResult = ResearchEducation.Create(institution, startDate, endDate, projectTitle, supervisor, summary);

        if (researchResult.IsFailure)
        {
            return Result.Failure("");
        }

        return await this.qualifiedService.AddEducationAsync(command.ProfileId, researchResult.Value);
    }
}
