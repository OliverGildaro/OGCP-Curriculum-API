﻿using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.Contracts.DDD;
using ArtForAll.Shared.ErrorHandler;
using ArtForAll.Shared.ErrorHandler.Maybe;
using ArtForAll.Shared.ErrorHandler.Results;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.commanding.commands.UpdateEducationToQualifiedProfile;

public class UpdateDegreeEducationFromQualifiedProfileCommandHandler
    : ICommandHandler<UpdateDegreeEducationFromQualifiedProfileCommand, Result>
{
    private readonly IQualifiedProfileService qualifiedService;

    public UpdateDegreeEducationFromQualifiedProfileCommandHandler(IQualifiedProfileService qualifiedService)
    {
        this.qualifiedService = qualifiedService;
    }

    public async Task<Result> HandleAsync(UpdateDegreeEducationFromQualifiedProfileCommand command)
    {
        (int id, string institution, EducationLevel degree, DateOnly startDate, DateOnly? endDate)
            = command;
        var degreeResult = DegreeEducation.Create(institution, degree, startDate, endDate);

        if (degreeResult.IsFailure)
        {
            return Result.Failure("");
        }
        var degreeToUpdate = degreeResult.Value;

        Maybe<DegreeEducation> maybeDegree = await this.qualifiedService.FindDegreeEducation(degreeToUpdate);

        if (maybeDegree.HasValue)
        {
            degreeToUpdate= maybeDegree.Value;
        }

        return await this.qualifiedService
            .UpdateEducationAsync(command.ProfileId, command.EducationId, degreeToUpdate);
    }
}
