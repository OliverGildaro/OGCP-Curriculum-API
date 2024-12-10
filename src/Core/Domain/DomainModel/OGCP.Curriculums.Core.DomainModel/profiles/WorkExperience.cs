using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculums.Core.DomainModel;

namespace OGCP.Curriculum.API.domainmodel;

public class WorkExperience : JobExperience
{
    protected WorkExperience()
    {
        
    }
    private WorkExperience(string company, DateTime startDate, DateTime? endDate, string description, string position)
    {
        Company = company;
        StartDate = startDate;
        EndDate = endDate;
        Description = description;
        Position = position;
    }

    // Property
    public string Position { get; private set; }

    // Factory method
    public static Result<WorkExperience, Error> Create(string company, DateTime startDate, DateTime? endDate, string description, string position)
    {
        // Validations
        if (string.IsNullOrWhiteSpace(company))
            return new Error("Company", "Company name is required.");

        if (startDate == default)
            return new Error("StartDate", "Start date must be a valid date.");

        if (endDate.HasValue && endDate.Value < startDate)
            return new Error("EndDate", "End date cannot be earlier than start date.");

        if (string.IsNullOrWhiteSpace(description))
            return new Error("Description", "Description is required.");

        if (string.IsNullOrWhiteSpace(position))
            return new Error("Position", "Position is required.");

        // Create and return the result
        return new WorkExperience(company, startDate, endDate, description, position);
    }

    public override bool IsEquivalent(JobExperience jobExperience)
    {
        if (jobExperience is InternshipExperience internship)
        {
            return this.Company.Equals(internship.Company)
                && this.StartDate.Equals(internship.StartDate)
                && this.EndDate.Equals(internship.EndDate);
        }

        if (jobExperience is WorkExperience workExp)
        {
            return this.Company.Equals(workExp.Company)
                && this.StartDate.Equals(workExp.StartDate)
                && this.EndDate.Equals(workExp.EndDate);
        }

        return false;
    }
}
