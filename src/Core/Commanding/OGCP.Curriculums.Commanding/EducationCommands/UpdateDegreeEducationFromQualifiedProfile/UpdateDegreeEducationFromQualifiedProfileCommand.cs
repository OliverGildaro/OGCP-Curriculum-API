﻿using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.Contracts.DDD;
using OGCP.Curriculum.API.domainmodel;
using CustomResult = ArtForAll.Shared.ErrorHandler.Results;

namespace OGCP.Curriculum.API.commanding.commands.UpdateEducationToQualifiedProfile;

public abstract class UpdateEducationFromQualifiedProfileCommand : ICommand
{
    public int ProfileId { get; set; }
    public int EducationId { get; set; }
    public string Institution { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
}

public class UpdateDegreeEducationFromQualifiedProfileCommand : UpdateEducationFromQualifiedProfileCommand
{

    public EducationLevel Degree { get; set; }

    public void Deconstruct(
        out int id,
        out string institution,
        out EducationLevel degree,
        out DateOnly startDate,
        out DateOnly? endDate)
    {
        id = base.ProfileId;
        institution = base.Institution;
        degree = this.Degree;
        startDate = base.StartDate;
        endDate = base.EndDate;
    }
}

public class UpdateResearchEducationFromQualifiedProfileCommand : UpdateEducationFromQualifiedProfileCommand
{
    public string ProjectTitle { get; set; }
    public string Supervisor { get; set; }
    public string Summary { get; set; }

    public void Deconstruct(
        out int id,
        out string institution,
        out DateOnly startDate,
        out DateOnly? endDate,
        out string projectTitle,
        out string supervisor,
        out string summary)
    {
        id = base.ProfileId;
        institution = base.Institution;
        startDate = base.StartDate;
        endDate = base.EndDate;
        projectTitle = this.ProjectTitle;
        supervisor = this.Supervisor;
        summary = this.Summary;
    }
}
