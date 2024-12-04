using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.Contracts.DDD;
using ArtForAll.Shared.ErrorHandler;
using ArtForAll.Shared.ErrorHandler.Maybe;
using OGCP.Curriculum.API.DAL.Mutations.Interfaces;
using OGCP.Curriculum.API.domainmodel;
using CustomResult = ArtForAll.Shared.ErrorHandler.Results;


namespace OGCP.Curriculum.API.commanding.commands.AddEducationDegree;

public abstract class AddEducationToProfileCommand : ICommand
{
    public int ProfileId { get; set; }
    public string Institution { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class AddDegreeEducationToQualifiedProfileCommand : AddEducationToProfileCommand
{
    public EducationLevel Degree { get; set; }

    public void Deconstruct(
        out int id,
        out string institution,
        out EducationLevel degree,
        out DateTime startDate,
        out DateTime? endDate)
    {
        id = base.ProfileId;
        institution = base.Institution;
        degree = this.Degree;
        startDate = base.StartDate;
        endDate = base.EndDate;
    }
}

public class AddResearchEducationToQualifiedProfileCommand : AddEducationToProfileCommand
{
    public string ProjectTitle { get; set; }
    public string Supervisor { get; set; }
    public string Summary { get; set; }
    private readonly IQualifiedProfileWriteRepo profileWriteRepo;
    public AddResearchEducationToQualifiedProfileCommand() {}
    public AddResearchEducationToQualifiedProfileCommand(IQualifiedProfileWriteRepo profileWriteRepo)
    {
        this.profileWriteRepo = profileWriteRepo;
    }
    public void Deconstruct(
        out int id,
        out string institution,
        out DateTime startDate,
        out DateTime? endDate,
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
