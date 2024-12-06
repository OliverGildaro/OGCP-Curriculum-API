using OGCP.Curriculum.API.DAL.Queries.Models;
using System.Text.Json.Serialization;

namespace OGCP.Curriculums.Reads.ProfileRepository.Models;

public class ProfileEducationReadModel
{
    public int ProfileId { get; set; }
    public int EducationId { get; set; }
    [JsonIgnore]
    public ProfileReadModel Profile { get; set; } = null;
    public EducationReadModel Education { get; set; } = null;
}
