using OGCP.Curriculum.API.DTOs.responses;
using OGCP.Curriculums.Reads.ProfileRepository.Models;

namespace OGCP.Curriculum.API.POCOS.responses;

public class ProfileResponse
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
    public IReadOnlyList<LanguageResponse> Languages { get; } = new List<LanguageResponse>();
    public IReadOnlyList<EducationResponse> Educations { get; } = new List<EducationResponse>();
}
