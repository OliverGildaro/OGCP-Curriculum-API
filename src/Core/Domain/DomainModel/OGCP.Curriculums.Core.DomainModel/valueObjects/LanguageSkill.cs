using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculums.Core.DomainModel.valueObjects;

public class LanguageSkill
{
    public LangSkill Skill { get; private set; }
    public ProficiencyLevel Level { get; private set; }

    private LanguageSkill(LangSkill skill, ProficiencyLevel level)
    {
        Skill = skill;
        Level = level;
    }

    protected LanguageSkill()
    {
        
    }

    public static Result<LanguageSkill, Error> CreateNew(LangSkill skill, ProficiencyLevel level)
    {
        return new LanguageSkill(skill, level);
    }

    public enum LangSkill
    {
        WRITING = 1,
        READING,
        LISTENING,
        SPEAKING
    }
}
