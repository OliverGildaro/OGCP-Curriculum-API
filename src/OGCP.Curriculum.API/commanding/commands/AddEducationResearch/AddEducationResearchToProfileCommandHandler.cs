using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.ErrorHandler;
using Azure.Core;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.commanding.commands.AddEducationResearch;

public class AddEducationResearchToProfileCommandHandler : ICommandHandler<AddEducationResearchToProfileCommand, Result>
{
    private readonly IStudentProfileService profileService;

    public AddEducationResearchToProfileCommandHandler(IStudentProfileService profileService)
    {
        this.profileService = profileService;
    }
    public Task<Result> HandleAsync(AddEducationResearchToProfileCommand command)
    {
        (int id, string institution, DateTime startDate, DateTime? endDate, string projectTitle, string supervisor, string summary)
            = command;

        ResearchEducation education = ResearchEducation
            .Create(institution, startDate, endDate, projectTitle, supervisor, summary).Value;

        return this.profileService.AddEducation(id, education);
    }
}
