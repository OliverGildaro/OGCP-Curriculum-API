using OGCP.Curriculum.API.DAL.Queries.Models;

namespace OGCP.Curriculum.API.DAL.Queries.interfaces;

public interface IProfileReadModelRepository : IReadRepository<ProfileReadModel, int>
{
    Task<IReadOnlyList<EducationReadModel>> FindEducationsFromProfile(int id);
    Task<IReadOnlyList<LanguageReadModel>> FindLanguagesFromProfile(int id);
}
