using OGCP.Curriculum.API.DAL.Queries.Models;

namespace OGCP.Curriculums.Reads.ProfileRepository.Models;

public class ProfileLanguageReadModel
{
    public int ProfileId { get; set; }
    public int LanguageId { get; set; }

    public ProfileReadModel Profile { get; set; }
    public LanguageReadModel Language { get; set; }
}
