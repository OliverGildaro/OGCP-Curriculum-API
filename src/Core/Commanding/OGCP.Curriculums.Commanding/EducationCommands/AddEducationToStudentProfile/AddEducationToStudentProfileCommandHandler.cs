using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.commanding.commands.AddEducationResearch;

public class AddEducationToStudentProfileCommandHandler : ICommandHandler<AddEducationToStudentProfileCommand, Result>
{
    private readonly IStudentProfileService profileService;

    public AddEducationToStudentProfileCommandHandler(IStudentProfileService profileService)
    {
        this.profileService = profileService;
    }
    public Task<Result> HandleAsync(AddEducationToStudentProfileCommand command)
    {
        (int id, string institution, DateOnly startDate, DateOnly? endDate, string projectTitle, string supervisor, string summary)
            = command;

        ResearchEducation education = ResearchEducation
            .Create(institution, startDate, endDate, projectTitle, supervisor, summary).Value;

        return this.profileService.AddEducationAsync(id, education);
    }
}
