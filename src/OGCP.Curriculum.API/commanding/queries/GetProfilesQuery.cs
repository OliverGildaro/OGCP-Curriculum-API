using ArtForAll.Shared.Contracts.CQRS;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.commanding.queries
{
    //We do not need to load a Profile from the domain model
    //The domain model is only for commands
    //For queries we should map to a different kind of entities
    //Not domain model entities
    public class GetProfilesQuery : IQuery<IReadOnlyList<Profile>>
    {
    }
}
