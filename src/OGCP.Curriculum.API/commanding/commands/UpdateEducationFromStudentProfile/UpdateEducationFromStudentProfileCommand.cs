
using ArtForAll.Shared.Contracts.CQRS;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.Commanding.commands.UpdateEducationFromStudentProfile;

public class UpdateEducationFromStudentProfileCommand : ICommand
{
    public int ProfileId { get; set; }
    public int EducationId { get; set; }
    public string Institution { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
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
        id = ProfileId;
        institution = Institution;
        startDate = StartDate;
        endDate = EndDate;
        projectTitle = ProjectTitle;
        supervisor = Supervisor;
        summary = Summary;
    }
}
