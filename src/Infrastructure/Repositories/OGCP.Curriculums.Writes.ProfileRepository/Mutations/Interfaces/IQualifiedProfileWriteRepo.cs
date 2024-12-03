using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.DAL.Mutations.Interfaces;

public interface IQualifiedProfileWriteRepo : IWriteRepository<QualifiedProfile, int>
{
    Task<Result> RemoveOrphanEducationsAsync(string removeEducation);

}
