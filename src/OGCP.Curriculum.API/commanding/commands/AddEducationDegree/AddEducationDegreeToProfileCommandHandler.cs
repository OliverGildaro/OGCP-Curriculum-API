using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.commanding.commands.AddEducationDegree;

public class AddEducationDegreeToProfileCommandHandler : ICommandHandler<AddEducationDegreeToProfileCommand, Result>
{
    private readonly IQualifiedProfileService profileService;

    public AddEducationDegreeToProfileCommandHandler(IQualifiedProfileService profileService)
    {
        this.profileService = profileService;
    }

    public Task<Result> HandleAsync(AddEducationDegreeToProfileCommand command)
    {
        (int id, string institution, EducationLevel degree, DateTime startDate, DateTime? endDate)
            = command;

        DegreeEducation education = DegreeEducation
            .Create(institution, degree, startDate, endDate).Value;

        return this.profileService.AddEducation(id, education);
    }
}
