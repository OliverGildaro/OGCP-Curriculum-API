using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.repositories.interfaces
{
    public interface IProfileRepository : IRepository<Profile, int>
    {
        Task<Language?> FindLanguageByNameAndLevel(Languages name, ProficiencyLevel level);
    }
}
