using ArtForAll.Shared.Contracts.CQRS;

namespace OGCP.Curriculum.API.Commanding.commands.DeleteProfile;

public class DeleteProfileCommand : ICommand
{
    public int Id { get; set; }
}
