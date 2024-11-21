using ArtForAll.Shared.Contracts.CQRS;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.commanding.queries
{
    public class GetProfilesQuery : IQuery<IReadOnlyList<Profile>>
    {
    }
}
