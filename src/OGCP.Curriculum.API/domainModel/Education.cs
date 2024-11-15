using OGCP.Curriculum.API.domainModel;

namespace OGCP.Curriculum.API.models;

public class Education
{
    public Education()
    {
        
    }
    protected Education(string institution, DateTime startDate, DateTime? endDate)
    {
        Institution = institution;
        StartDate = startDate;
        EndDate = endDate;
    }

    public int Id { get; set; }
    public string Institution { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}


public class DegreeEducation : Education
{
    public DegreeEducation()
    {

    }
    public DegreeEducation(string institution, EducationLevel degree, DateTime startDate, DateTime? endDate)
        : base(institution, startDate, endDate)
    {
        Degree = degree;
    }

    public EducationLevel Degree { get; set; }//TODO enum to string ocnversion on FluentAPI
}

public class ResearchEducation : Education
{
    public ResearchEducation()
    {

    }
    public ResearchEducation(string institution, DateTime startDate, DateTime? endDate, string projectTitle, string supervisor, string summary)
        : base(institution, startDate, endDate)
    {
        ProjectTitle = projectTitle;
        Supervisor = supervisor;
        Summary = summary;
    }

    public string ProjectTitle { get; set; }
    public string Supervisor { get; set; }
    public string Summary { get; set; }
}
