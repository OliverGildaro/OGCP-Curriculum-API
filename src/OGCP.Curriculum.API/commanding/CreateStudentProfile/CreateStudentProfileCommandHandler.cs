using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.ErrorHandler;
using Azure.Core;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.commanding.CreateStudentProfile;

public class CreateStudentProfileCommandHandler : ICommandHandler<CreateStudentProfileCommand, Result>
{
    private readonly IStudentProfileService profileService;

    public CreateStudentProfileCommandHandler(IStudentProfileService profileService)
    {
        this.profileService = profileService;
    }

    public async Task<Result> HandleAsync(CreateStudentProfileCommand command)
    {
        (string firstName, string lastName, string summary, string major, string careerGoals) = command;
        var studentProfile = StudentProfile.Create(firstName, lastName, summary, major, careerGoals);
        return await this.profileService.Create(studentProfile.Value);
    }
}
