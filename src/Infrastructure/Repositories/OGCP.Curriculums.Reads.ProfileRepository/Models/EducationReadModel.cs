using OGCP.Curriculums.Reads.ProfileRepository.Models;

namespace OGCP.Curriculum.API.DAL.Queries.Models;

public class EducationReadModel
{
    public int Id { get; set; }
    //public int ProfileId { get; set; }
    public string Institution { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Degree { get; set; }
    public string ProjectTitle { get; set; }
    public string Supervisor { get; set; }
    public string Summary { get; set; }
    public IReadOnlyList<ProfileEducationReadModel> ProfileEducations { get; } = [];
}
