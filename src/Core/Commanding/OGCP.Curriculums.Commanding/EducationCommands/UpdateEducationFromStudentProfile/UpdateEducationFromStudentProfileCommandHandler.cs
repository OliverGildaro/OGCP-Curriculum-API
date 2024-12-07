using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.ErrorHandler;
using ArtForAll.Shared.ErrorHandler.Maybe;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.Commanding.commands.UpdateEducationFromStudentProfile;

public class UpdateEducationFromStudentProfileCommandHandler : ICommandHandler<UpdateEducationFromStudentProfileCommand, Result>
{
    private readonly IStudentProfileService profileService;

    public UpdateEducationFromStudentProfileCommandHandler(IStudentProfileService profileService)
    {
        this.profileService = profileService;
    }
    public async Task<Result> HandleAsync(UpdateEducationFromStudentProfileCommand command)
    {
        (int id,
        string institution,
        DateOnly startDate,
        DateOnly? endDate,
        string projectTitle,
        string supervisor,
        string summary) = command;

        var reserachEduResult = ResearchEducation
            .Create(institution, startDate, endDate, projectTitle, supervisor, summary);
        if (reserachEduResult.IsFailure)
        {
            return Result.Failure("");
        }
        var researchToUpdate = reserachEduResult.Value;
        Maybe<ResearchEducation> maybeResearch = await this.profileService.FindResearchEducation(institution, projectTitle);

        if (maybeResearch.HasValue && (maybeResearch.Value.IsEquivalent(researchToUpdate)))
        {
            researchToUpdate.UpdateId(maybeResearch.Value.Id);
        }
        return await this.profileService.UpdateEducationAsync(command.ProfileId, command.EducationId, researchToUpdate);
    }
}
