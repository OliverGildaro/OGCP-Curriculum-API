using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.ErrorHandler;
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
    public Task<Result> HandleAsync(UpdateEducationFromStudentProfileCommand command)
    {
        (int id,
        string institution,
        DateTime startDate,
        DateTime? endDate,
        string projectTitle,
        string supervisor,
        string summary) = command;

        var reserachEduResult = ResearchEducation
            .Hidrate(id, institution, startDate, endDate, projectTitle, supervisor, summary);
        return this.profileService.UpdateEducationAsync(command.ProfileId, reserachEduResult.Value);
    }
}
