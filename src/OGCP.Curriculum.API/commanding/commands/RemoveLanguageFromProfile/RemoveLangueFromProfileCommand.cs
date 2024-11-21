using ArtForAll.Shared.Contracts.CQRS;

namespace OGCP.Curriculum.API.commanding.commands.AddLanguageToProfile;

public class RemoveLangueFromProfileCommand : ICommand
{
    public int Id { get; set; }
    public int LanguageId { get; set; }
}
