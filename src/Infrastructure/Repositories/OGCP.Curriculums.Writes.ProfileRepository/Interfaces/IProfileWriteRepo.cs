using ArtForAll.Shared.ErrorHandler;
using ArtForAll.Shared.ErrorHandler.Maybe;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.DAL.Mutations.Interfaces;

public interface IProfileWriteRepo : IWriteRepository<Profile, int>
{
    Result Add(Profile entity);
    Result DeleteProfileAsync(Profile value);
    Task<bool> ExistProfileAsync(int id);
    Task<Maybe<Language>> FindAsync(Language language);
}
