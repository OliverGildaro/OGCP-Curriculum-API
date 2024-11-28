namespace OGCP.Curriculum.API.DAL.Queries.Models;

public class ProfileReadModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Summary { get; set; }
    public string DesiredJobRole { get; set; }
    public string[]? PersonalGoals { get; set; } = new string[] { };
    public string? Major { get; set; }
    public string? CareerGoals { get; set; }
    public IReadOnlyList<LanguageReadModel> Languages { get; set; }
    public IReadOnlyList<EducationReadModel> Educations { get; set; }
    public IReadOnlyList<WorkExpReadModel> WorkExp { get; set; }
}
