using OGCP.Curriculum.API.DAL.Queries.interfaces;
using OGCP.Curriculum.API.domainmodel;
using System.Linq.Expressions;

namespace OGCP.Curriculum.API.DAL.Queries;

public class ProfileReadRepo : IProfileReadRepo
{
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
}
