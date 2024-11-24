using ArtForAll.Shared.Contracts.CQRS;

namespace OGCP.Curriculum.API.Commanding.commands.RemoveEducationFromQualifiedProfile;

public class RemoveEducationFromProfileCommand : ICommand
{
    public int Id { get; set; }
    public int EducationId { get; set; }
}

public class RemoveEducationFromQualifiedProfileCommand : RemoveEducationFromProfileCommand
{
}
