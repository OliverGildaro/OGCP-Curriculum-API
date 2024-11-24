using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.Commanding.commands.RemoveEducationFromStudentProfile;

public class RemoveEducationFromStudentProfileCommandHandler : ICommandHandler<RemoveEducationFromStudentProfileCommand, Result>
{
    private readonly IStudentProfileService studentProfileService;

    public RemoveEducationFromStudentProfileCommandHandler(IStudentProfileService studentProfileService)
    {
        this.studentProfileService = studentProfileService;
    }
    public Task<Result> HandleAsync(RemoveEducationFromStudentProfileCommand command)
    {
        return this.studentProfileService.RemoveEducation(command.Id, command.EducationId);
    }
}
