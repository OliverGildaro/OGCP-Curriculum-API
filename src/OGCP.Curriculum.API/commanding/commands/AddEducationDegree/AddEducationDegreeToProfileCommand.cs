
using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.Contracts.DDD;
using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.domainmodel;
using CustomResult = ArtForAll.Shared.ErrorHandler.Results;


namespace OGCP.Curriculum.API.commanding.commands.AddEducationDegree;

public abstract class AddEducationToProfileCommand : ICommand
{
    public int Id { get; set; }
    public string Institution { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public abstract CustomResult.IResult<Education, Error> MapTo();
}

public class AddEducationDegreeToProfileCommand : AddEducationToProfileCommand
{

    public EducationLevel Degree { get; set; }

    public void Deconstruct(
        out int id,
        out string institution,
        out EducationLevel degree,
        out DateTime startDate,
        out DateTime? endDate)
    {
        id = base.Id;
        institution = base.Institution;
        degree = this.Degree;
        startDate = base.StartDate;
        endDate = base.EndDate;
    }

    public override CustomResult.IResult<Education, Error> MapTo()
    {
        return DegreeEducation.Create(Institution, Degree, StartDate, EndDate);
    }
}

public class AddEducationResearchToProfileCommand : AddEducationToProfileCommand
{
    //public int Id { get; set; }
    //public string Institution { get; set; }
    //public DateTime StartDate { get; set; }
    //public DateTime? EndDate { get; set; }
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
        id = base.Id;
        institution = base.Institution;
        startDate = base.StartDate;
        endDate = base.EndDate;
        projectTitle = this.ProjectTitle;
        supervisor = this.Supervisor;
        summary = this.Summary;
    }

    public override Result<Education, Error> MapTo()
    {
        throw new NotImplementedException();
    }
}
