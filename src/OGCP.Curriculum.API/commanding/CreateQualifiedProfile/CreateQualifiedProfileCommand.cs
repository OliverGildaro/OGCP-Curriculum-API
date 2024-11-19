
using ArtForAll.Shared.Contracts.CQRS;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.commanding.CreateQualifiedProfile;

public class CreateQualifiedProfileCommand : ICommand
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Summary { get; set; }
    public ProfileTypes RequestType { get; set; } // Discriminator field
    public string DesiredJobRole { get; set; }
}
