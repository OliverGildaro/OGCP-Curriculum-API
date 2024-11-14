using OGCP.Curriculum.API.domainModel;

namespace OGCP.Curriculum.API.dtos.requests;
public abstract class CreateJobExperienceRequest
{
    public string Company { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Description { get; set; }
    public WorkExperiences ExperiencesType { get; set; } // Discriminator field

    public void Deconstruct(out string company, out DateTime startDate, out DateTime? endDate, out string description)
    {
        company = this.Company;
        startDate = this.StartDate;
        endDate = this.EndDate;
        description = this.Description;
    }
}


public class CreateWorkExperienceRequest : CreateJobExperienceRequest
{
    public string Position { get; set; }

    public void Deconstruct(
        out string company,
        out DateTime startDate,
        out DateTime? endDate,
        out string description,
        out string position)
    {
        base.Deconstruct(out company, out startDate, out endDate, out description);
        position = this.Position;
    }
}

public class CreateInternshipExperienceRequest : CreateJobExperienceRequest
{
    public string Role { get; set; }

    public void Deconstruct(
        out string company,
        out DateTime startDate,
        out DateTime? endDate,
        out string description,
        out string role)
    {
        base.Deconstruct(out company, out startDate, out endDate, out description);
        role = this.Role;
    }
}

