using ArtForAll.Shared.Contracts.DDD;
using ArtForAll.Shared.ErrorHandler.Results;
using OGCP.Curriculum.API.commanding.commands.AddEducationDegree;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.commanding.commands.AddEducationResearch;

public class AddEducationToStudentProfileCommand : AddEducationToProfileCommand
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
        id = this.ProfileId;
        institution = this.Institution;
        startDate = this.StartDate;
        endDate = this.EndDate;
        projectTitle = this.ProjectTitle;
        supervisor = this.Supervisor;
        summary = this.Summary;
    }
}
