using ArtForAll.Shared.Contracts.CQRS;

namespace OGCP.Curriculum.API.Commanding.commands.RemoveEducationFromQualifiedProfile;

public class RemoveEducationFromQualifiedProfileCommand : ICommand
{
    public int Id { get; set; }
    public int EducationId { get; set; }
}
