using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.DAL.Mutations.Interfaces;
using OGCP.Curriculum.API.domainmodel;
using System.Linq.Expressions;

namespace OGCP.Curriculum.API.DAL.Mutations;

public class ProfileWriteRepo : IProfileWriteRepo
{
    public Result Add(Profile entity)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Profile>> Find()
    {
        throw new NotImplementedException();
    }

    public Task<Profile> Find(int id, params Expression<Func<Profile, object>>[] includes)
    {
        throw new NotImplementedException();
    }

    public Task<Profile> Find(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveChanges()
    {
        throw new NotImplementedException();
    }
}
