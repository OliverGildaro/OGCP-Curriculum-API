using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.ErrorHandler;
using ArtForAll.Shared.ErrorHandler.Maybe;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.commanding.commands.UpdateEducationToQualifiedProfile;

public class UpdateResearchEducationFromQualifiedProfileCommandHandler
    : ICommandHandler<UpdateResearchEducationFromQualifiedProfileCommand, Result>
{
    private readonly IQualifiedProfileService qualifiedService;

    public UpdateResearchEducationFromQualifiedProfileCommandHandler(IQualifiedProfileService qualifiedService)
    {
        this.qualifiedService = qualifiedService;
    }

    public async Task<Result> HandleAsync(UpdateResearchEducationFromQualifiedProfileCommand command)
    {
        (int id, string institution, DateOnly startDate, DateOnly? endDate, string projectTitle, string supervisor, string summary)
            = command;
        var reserachEduResult = ResearchEducation
            .Hidrate(id, institution, startDate, endDate, projectTitle, supervisor, summary);

        if (reserachEduResult.IsFailure)
        {
            return Result.Failure("");
        }
        var researchToUpdate = reserachEduResult.Value;

        Maybe<ResearchEducation> maybeResearch = await this.qualifiedService.FindResearchEducation(researchToUpdate);

        if (maybeResearch.HasValue && (maybeResearch.Value.IsEquivalent(researchToUpdate)))
        {
            researchToUpdate = maybeResearch.Value;
        }

        return await this.qualifiedService
            .UpdateEducationAsync(command.ProfileId, command.EducationId, researchToUpdate);
    }
}
