using OGCP.Curriculums.Reads.ProfileRepository.Models;

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
    public string? Discriminator { get; set; }
    public IReadOnlyList<ProfileLanguageReadModel> ProfileLanguages { get; } = [];
    public IReadOnlyList<ProfileEducationReadModel> ProfileEducations { get; } = [];
    //public IReadOnlyList<WorkExpReadModel> WorkExp { get; set; }
}
