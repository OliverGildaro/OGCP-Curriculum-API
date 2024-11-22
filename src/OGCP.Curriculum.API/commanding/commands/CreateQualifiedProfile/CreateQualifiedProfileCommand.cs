
using ArtForAll.Shared.Contracts.CQRS;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.DTOs;

namespace OGCP.Curriculum.API.commanding.commands.CreateQualifiedProfile;

public class CreateQualifiedProfileCommand : ICommand
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Summary { get; set; }
    public ProfileRequests RequestType { get; set; } // Discriminator field
    public string DesiredJobRole { get; set; }

    public void Deconstruct(out string firstName, out string lastName, out string summary, out string desiredJobRole)
    {
        firstName = FirstName;  // Assuming FirstName is a property in ProfileRequest
        lastName = LastName;    // Assuming LastName is a property in ProfileRequest
        summary = Summary;      // Assuming Summary is a property in ProfileRequest
        desiredJobRole = DesiredJobRole;
    }
}
