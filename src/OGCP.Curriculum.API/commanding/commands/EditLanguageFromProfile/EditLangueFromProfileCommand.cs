using ArtForAll.Shared.Contracts.CQRS;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.commanding.commands.EditLanguageFromProfile
{
    public class EditLangueFromProfileCommand : ICommand
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public Languages Name { get; set; }
        public ProficiencyLevel Level { get; set; }
    }
}
