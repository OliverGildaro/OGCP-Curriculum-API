using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.ErrorHandler;
using ArtForAll.Shared.ErrorHandler.Maybe;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.commanding.commands.AddEducationDegree;

//Generics are invariant by default
//Covariance and contravariance is supported only for interfaces and delegates
public class AddDegreeEducationToQualifiedProfileCommandHandler
    : ICommandHandler<AddDegreeEducationToQualifiedProfileCommand, Result>
{
    private readonly IQualifiedProfileService qualifiedService;

    public AddDegreeEducationToQualifiedProfileCommandHandler(IQualifiedProfileService qualifiedService)
    {
        this.qualifiedService = qualifiedService;
    }

    public async Task<Result> HandleAsync(AddDegreeEducationToQualifiedProfileCommand command)
    {
        Maybe<DegreeEducation> maybeDegree = await this.qualifiedService
            .FindDegreeEducation(command.Institution, command.Degree);

        if (maybeDegree.HasValue)
        {
            return await this.qualifiedService.AddEducationAsync(command.ProfileId, maybeDegree.Value);
        }

        (int id, string institution, EducationLevel degree, DateTime startDate, DateTime? endDate)
            = command;
        var degreeResult = DegreeEducation.Create(institution, degree, startDate, endDate);

        if (degreeResult.IsFailure)
        {
            return Result.Failure("");
        }

        return await this.qualifiedService.AddEducationAsync(command.ProfileId, degreeResult.Value);
    }
}
