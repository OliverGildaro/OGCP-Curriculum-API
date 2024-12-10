using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculums.Core.DomainModel;
//using CustomResult = ArtForAll.Shared.ErrorHandler.Results;

namespace OGCP.Curriculum.API.domainmodel;

public class DegreeEducation : Education
{
    private EducationLevel _degree;

    protected DegreeEducation()
    {

    }

    private DegreeEducation(string institution, EducationLevel degree, DateOnly startDate, DateOnly? endDate)
    {
        _degree = degree;
        _institution = institution;
        _startDate = startDate;
        _endDate = endDate;
    }

    public DegreeEducation(int educationId, string institution, EducationLevel degree, DateOnly startDate, DateOnly? endDate)
        : this(institution, degree, startDate, endDate)
    {
        base._id = educationId;
    }

    public EducationLevel Degree => _degree;

    public static Result<DegreeEducation, Error> Create(string institution, EducationLevel degree, DateOnly startDate, DateOnly? endDate)
    {
        if (string.IsNullOrWhiteSpace(institution))
        {
            return new Error("Institution is required.", "InvalidInstitution");
        }

        if (startDate > DateOnly.FromDateTime(DateTime.UtcNow))
        {
            return new Error("Start date cannot be in the future.", "InvalidStartDate");
        }

        if (!Enum.IsDefined(typeof(EducationLevel), degree))
        {
            return new Error("Invalid degree value.", "InvalidDegree");
        }

        return new DegreeEducation(institution, degree, startDate, endDate);
    }

    public override Result Update(Education other)
    {
        var degreeEduc = (DegreeEducation)other;
        _id = degreeEduc.Id;
        _degree = degreeEduc.Degree;
        _institution = degreeEduc.Institution;
        _startDate = degreeEduc.StartDate;
        _endDate = degreeEduc.EndDate;

        return Result.Success();
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

    public static Result<DegreeEducation, Error> Hidrate(int educationId, string institution, EducationLevel degree, DateOnly startDate, DateOnly? endDate)
    {
        return new DegreeEducation(educationId, institution, degree, startDate, endDate);

    }

    public void UpdateId(int id)
    {
        this._id = id;
    }
}
