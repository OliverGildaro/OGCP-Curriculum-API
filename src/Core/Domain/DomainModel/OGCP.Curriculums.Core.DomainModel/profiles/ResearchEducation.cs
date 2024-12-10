using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculums.Core.DomainModel;
//using CustomResult = ArtForAll.Shared.ErrorHandler.Results;

namespace OGCP.Curriculum.API.domainmodel;

public class ResearchEducation : Education
{
    private string _projectTitle;
    private string _supervisor;
    private string _summary;

    protected ResearchEducation()
    {

    }
    private ResearchEducation(string institution, DateOnly startDate, DateOnly? endDate, string projectTitle, string supervisor, string summary)
    {
        _institution = institution;
        _startDate = startDate;
        _endDate = endDate;
        _projectTitle = projectTitle;
        _supervisor = supervisor;
        _summary = summary;
    }

    private ResearchEducation(int educationId,
        string institution,
        DateOnly startDate,
        DateOnly? endDate,
        string projectTitle,
        string supervisor,
        string summary)
        : this(institution, startDate, endDate, projectTitle, supervisor, summary)
    {
        base._id = educationId;
    }

    public string ProjectTitle => _projectTitle;
    public string Supervisor => _supervisor;
    public string Summary => _summary;

    public static Result<ResearchEducation, Error> Create(
        string institution,
        DateOnly startDate,
        DateOnly? endDate,
        string projectTitle,
        string supervisor,
        string summary)
    {
        if (string.IsNullOrWhiteSpace(institution))
        {
            return new Error("Institution is required.", "InvalidInstitution");
        }

        if (startDate > DateOnly.FromDateTime(DateTime.UtcNow))
        {
            return new Error("Start date cannot be in the future.", "InvalidStartDate");
        }

        if (string.IsNullOrWhiteSpace(projectTitle))
        {
            return new Error("Project title is required.", "InvalidProjectTitle");
        }

        if (string.IsNullOrWhiteSpace(supervisor))
        {
            return new Error("Supervisor is required.", "InvalidSupervisor");
        }

        return new ResearchEducation(institution, startDate, endDate, projectTitle, supervisor, summary);
    }

    public static Result<ResearchEducation, Error> Hidrate(
        int educationId,
        string institution,
        DateOnly startDate,
        DateOnly? endDate,
        string projectTitle,
        string supervisor,
        string summary)
    {
        return new ResearchEducation(educationId, institution, startDate, endDate, projectTitle, supervisor, summary);
    }

    public override bool IsEquivalent(Education other)
    {
        if (other is ResearchEducation degree)
        {
            return this.Institution == degree.Institution
                && this.ProjectTitle == degree.ProjectTitle;

        }
        return false;
    }

    public override Result Update(Education other)
    {
        var researchEduc = (ResearchEducation)other;
        _projectTitle = researchEduc.ProjectTitle;
        _supervisor = researchEduc.Supervisor;
        _summary = researchEduc.Summary;
        _institution = researchEduc.Institution;
        _startDate = researchEduc.StartDate;
        _endDate = researchEduc.EndDate;

        return Result.Success();
    }

    public void UpdateId(int id)
    {
        this._id = id;
    }
}
