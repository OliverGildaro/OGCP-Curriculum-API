using ArtForAll.Shared.Contracts.CQRS;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculums.Core.DomainModel.valueObjects;

namespace OGCP.Curriculum.API.commanding.commands.AddSkillToLanguage;

public class AddLangueSkillToLanguageCommand : ICommand
{
    public int ProfileId { get; set; }
    public int EducationId { get; set; }
    public ProficiencyLevel Level { get; set; }
    public LanguageSkill.LangSkill Skill { get; set; }
}