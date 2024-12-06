using OGCP.Curriculum.API.DAL.Queries.Models;
using OGCP.Curriculums.Reads.ProfileRepository.DTOs;

namespace OGCP.Curriculum.API.DAL.Queries.interfaces;

public interface IProfileReadModelRepository : IReadRepository<ProfileReadModel, int>
{
    Task<IReadOnlyList<ProfileEducationDto>> FindEducationsAsync();
    Task<IReadOnlyList<EducationReadModel>> FindEducationsFromProfile(int id);
    Task<IReadOnlyList<LanguageReadModel>> FindLanguagesFromProfile(int id);
    Task FindLanguagesGrouped();
}
