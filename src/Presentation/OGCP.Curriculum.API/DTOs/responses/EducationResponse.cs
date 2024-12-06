namespace OGCP.Curriculum.API.DTOs.responses;

public class EducationResponse
{
    public int Id { get; set; }
    public string Institution { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Degree { get; set; }
    public string ProjectTitle { get; set; }
    public string Supervisor { get; set; }
    public string Summary { get; set; }
}
