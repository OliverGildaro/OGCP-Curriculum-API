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
        (int id, string institution, EducationLevel degree, DateOnly startDate, DateOnly? endDate)
            = command;
        var degreeResult = DegreeEducation.Create(institution, degree, startDate, endDate);

        if (degreeResult.IsFailure)
        {
            return Result.Failure("");
        }

        var degreeToAdd = degreeResult.Value;

        Maybe<DegreeEducation> maybeDegree = await this.qualifiedService
            .FindDegreeEducation(degreeToAdd);

        if (maybeDegree.HasValue)
        {
            degreeToAdd = maybeDegree.Value;
        }

        return await this.qualifiedService.AddEducationAsync(command.ProfileId, degreeToAdd);
    }
}
