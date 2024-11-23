using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.Contracts.DDD;
using OGCP.Curriculum.API.domainmodel;
using CustomResult = ArtForAll.Shared.ErrorHandler.Results;


namespace OGCP.Curriculum.API.commanding.commands.AddEducationDegree;

public abstract class AddEducationToQualifiedProfileCommand : ICommand
{
    public int ProfileId { get; set; }
    public string Institution { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public abstract CustomResult.IResult<Education, Error> MapTo();
}

public class AddDegreeEducationToQualifiedProfileCommand : AddEducationToQualifiedProfileCommand
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

    //Here we are using covariance on an interface
    public override CustomResult.IResult<Education, Error> MapTo()
    {
        return DegreeEducation.Create(Institution, Degree, StartDate, EndDate);
    }
}

public class AddResearchEducationToQualifiedProfileCommand : AddEducationToQualifiedProfileCommand
{
    public string ProjectTitle { get; set; }
    public string Supervisor { get; set; }
    public string Summary { get; set; }

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

    //Generics are invariant by default
    //Covariance and contravariance is supported only for interfaces and delegates
    public override CustomResult.IResult<ResearchEducation, Error> MapTo()
    {
        return ResearchEducation.Create(Institution, StartDate, EndDate, ProjectTitle, Supervisor, Summary);

    }
}
