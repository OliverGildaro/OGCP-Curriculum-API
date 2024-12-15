using OGCP.Curriculum.API.domainmodel;
using static OGCP.Curriculums.Core.DomainModel.valueObjects.LanguageSkill;

namespace OGCP.Curriculum.API.DTOs.requests.Language;

public class AddLanguageSkillRequest
{
    public LangSkill Skill { get; set; }
    public ProficiencyLevel Level { get; set; }
}
