using ArtForAll.Shared.Contracts.CQRS;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.commanding.AddLanguageToProfile
{
    public class AddLangueToProfileCommand : ICommand
    {
        public int Id { get; set; }
        public Languages Name { get; set; }
        public ProficiencyLevel Level { get; set; }
    }
}
