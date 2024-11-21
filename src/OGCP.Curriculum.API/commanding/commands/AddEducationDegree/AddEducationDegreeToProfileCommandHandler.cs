using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.commanding.commands.AddEducationDegree;

public class AddEducationDegreeToProfileCommandHandler : ICommandHandler<AddEducationToProfileCommand, Result>
{
    private readonly IQualifiedProfileService profileService;

    public AddEducationDegreeToProfileCommandHandler(IQualifiedProfileService profileService)
    {
        this.profileService = profileService;
    }

    public Task<Result> HandleAsync(AddEducationToProfileCommand command)
    {
        //(int id, string institution, EducationLevel degree, DateTime startDate, DateTime? endDate)
        //    = command;
        //command.
        var  aaas = command.MapTo();
        return this.profileService.AddEducation(command.Id, aaas.Value);
    }
}
