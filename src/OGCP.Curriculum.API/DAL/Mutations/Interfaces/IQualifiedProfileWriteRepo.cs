using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.repositories.interfaces;

namespace OGCP.Curriculum.API.DAL.Mutations.Interfaces;

public interface IQualifiedProfileWriteRepo : IWriteRepository<QualifiedProfile, int>
{
    Task<Result> RemoveOrphanEducations(string removeEducation);

}
