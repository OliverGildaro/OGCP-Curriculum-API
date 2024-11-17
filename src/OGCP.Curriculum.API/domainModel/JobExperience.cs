using ArtForAll.Shared.Contracts.DDD;
using ArtForAll.Shared.ErrorHandler;

namespace OGCP.Curriculum.API.domainmodel;

public class JobExperience
{
    protected JobExperience()
    {
        
    }
    protected JobExperience(string company, DateTime startDate, DateTime? endDate, string description)
    {
        Company = company;
        StartDate = startDate;
        EndDate = endDate;
        Description = description;
    }

    // Properties
    public int Id { get; private set; }
    public string Company { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public string Description { get; private set; }

    // Static factory method
    public static Result<JobExperience, Error> Create(string company, DateTime startDate, DateTime? endDate, string description)
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

        // Create and return the result
        return new JobExperience(company, startDate, endDate, description);
    }
}


public class InternshipExperience : JobExperience
{
    private InternshipExperience(string company, DateTime startDate, DateTime? endDate, string description, string role)
        : base(company, startDate, endDate, description)
    {
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
}

public class WorkExperience : JobExperience
{
    private WorkExperience(string company, DateTime startDate, DateTime? endDate, string description, string position)
        : base(company, startDate, endDate, description)
    {
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
}
