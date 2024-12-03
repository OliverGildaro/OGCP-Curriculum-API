using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.repositories.interfaces;

namespace OGCP.Curriculum.API.DAL.Mutations.Interfaces;

public interface IProfileWriteRepo : IWriteRepository<Profile, int>
{
    Result Add(Profile entity);
    Result DeleteProfileAsync(Profile value);
    Task<bool> ExistProfileAsync(int id);
}
