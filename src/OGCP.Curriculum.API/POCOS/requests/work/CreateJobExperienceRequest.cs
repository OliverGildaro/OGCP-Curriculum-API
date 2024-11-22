using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.POCOS.requests.work;
public abstract class CreateJobExperienceRequest
{
    public string Company { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Description { get; set; }
    public WorkExperienceCategory ExperiencesType { get; set; } // Discriminator field

    public void Deconstruct(out string company, out DateTime startDate, out DateTime? endDate, out string description)
    {
        company = Company;
        startDate = StartDate;
        endDate = EndDate;
        description = Description;
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
        Deconstruct(out company, out startDate, out endDate, out description);
        position = Position;
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
        Deconstruct(out company, out startDate, out endDate, out description);
        role = Role;
    }
}

