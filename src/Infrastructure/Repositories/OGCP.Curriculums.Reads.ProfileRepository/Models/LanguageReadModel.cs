using OGCP.Curriculums.Reads.ProfileRepository.Models;

namespace OGCP.Curriculum.API.DAL.Queries.Models;

public class LanguageReadModel
{
    public int Id { get; set; }
    //public int ProfileId { get; set; }
    public string Name { get; set; }
    public string Level { get; set; }
    public IReadOnlyList<ProfileLanguageReadModel> ProfileLanguages { get; } = [];
}
