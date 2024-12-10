using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculums.Core.DomainModel;

namespace OGCP.Curriculum.API.domainmodel;

public class InternshipExperience : JobExperience
{
    protected InternshipExperience()
    {
        
    }
    private InternshipExperience(string company, DateTime startDate, DateTime? endDate, string description, string role)
    {
        Company = company;
        StartDate = startDate;
        EndDate = endDate;
        Description = description;
        Role = role;
    }

    // Property
    public string Role { get; private set; }

    // Factory method
    public static Result<InternshipExperience, Error> Create(string company, DateTime startDate, DateTime? endDate, string description, string role)
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

        if (string.IsNullOrWhiteSpace(role))
            return new Error("Role", "Role is required.");

        // Create and return the result
        return new InternshipExperience(company, startDate, endDate, description, role);
    }

    public override bool IsEquivalent(JobExperience jobExperience)
    {
        if (jobExperience is InternshipExperience workExp)
        {
            return this.Company.Equals(workExp.Company)
                && this.StartDate.Equals(workExp.StartDate)
                && this.EndDate.Equals(workExp.EndDate);
        }

        return false;
    }
}
