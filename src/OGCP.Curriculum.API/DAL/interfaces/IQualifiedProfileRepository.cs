using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.repositories.interfaces
{
    public interface IQualifiedProfileRepository : IRepository<QualifiedProfile, int>
    {
        Task<Result> RemoveOrphanEducations(string removeEducation);
    }
}
