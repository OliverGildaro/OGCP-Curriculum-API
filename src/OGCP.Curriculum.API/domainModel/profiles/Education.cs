using ArtForAll.Shared.Contracts.DDD;
using ArtForAll.Shared.ErrorHandler;
using ArtForAll.Shared.ErrorHandler.Results;
using System.Runtime.CompilerServices;
//using CustomResult = ArtForAll.Shared.ErrorHandler.Results;

namespace OGCP.Curriculum.API.domainmodel;

public abstract class Education
{
    protected int _id;
    protected string _institution;
    protected DateTime _startDate;
    protected DateTime? _endDate;

    public int Id => _id;
    public string Institution => _institution;
    public DateTime StartDate => _startDate;
    public DateTime? EndDate => _endDate;

    public abstract bool IsEquivalent(Education other);

    internal Result Update(Education education)
    {
        throw new NotImplementedException();
    }
}

public class DegreeEducation : Education
{
    private EducationLevel _degree;

    private DegreeEducation(string institution, EducationLevel degree, DateTime startDate, DateTime? endDate)
    {
        _degree = degree;
        _institution = institution;
        _startDate = startDate;
        _endDate = endDate;
    }

    public EducationLevel Degree => _degree;

    public static Result<DegreeEducation, Error> Create(string institution, EducationLevel degree, DateTime startDate, DateTime? endDate)
    {
        if (string.IsNullOrWhiteSpace(institution))
        {
            return new Error("Institution is required.", "InvalidInstitution");
        }

        if (startDate > DateTime.Now)
        {
            return new Error("Start date cannot be in the future.", "InvalidStartDate");
        }

        if (!Enum.IsDefined(typeof(EducationLevel), degree))
        {
            return new Error("Invalid degree value.", "InvalidDegree");
        }

        return new DegreeEducation(institution, degree, startDate, endDate);
    }

    public override bool IsEquivalent(Education other)
    {
        if (other is DegreeEducation degree)
        {
            return this.Institution.Equals(degree.Institution)
                && this.Degree.Equals(degree.Degree);
        }

        return false;
    }
}

public class ResearchEducation : Education
{
    private string _projectTitle;
    private string _supervisor;
    private string _summary;

    private ResearchEducation(string institution, DateTime startDate, DateTime? endDate, string projectTitle, string supervisor, string summary)
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
        DateTime startDate,
        DateTime? endDate,
        string projectTitle,
        string supervisor,
        string summary)
        :this(institution, startDate, endDate, projectTitle, supervisor, summary)
    {
        base._id = educationId;
    }

    public string ProjectTitle => _projectTitle;
    public string Supervisor => _supervisor;
    public string Summary => _summary;

    public static Result<ResearchEducation, Error> Create(
        string institution,
        DateTime startDate,
        DateTime? endDate,
        string projectTitle,
        string supervisor,
        string summary)
    {
        if (string.IsNullOrWhiteSpace(institution))
        {
            return new Error("Institution is required.", "InvalidInstitution");
        }

        if (startDate > DateTime.Now)
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

    internal static Result<ResearchEducation, Error> Hidrate(
        int educationId,
        string institution,
        DateTime startDate,
        DateTime? endDate,
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
}
