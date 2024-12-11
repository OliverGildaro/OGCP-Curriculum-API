using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculums.Core.DomainModel.valueObjects;

namespace OGCP.Curriculums.Core.DomainModel.profiles;

public class ProfileLanguage
{
    public Language Language { get; set; }

    private List<LanguageSkill> _languageSkills = new();
    public IReadOnlyList<LanguageSkill> LanguageSkills  => _languageSkills;

    public Result<LanguageSkill, Error> AddNewLangSkill(LanguageSkill skill)
    {
        if (LanguageSkills.Any(s => s.Skill == skill.Skill))
            return Errors.Validation.DuplicateValue("Skill already exists.");

        _languageSkills.Add(skill);
        return skill;
    }
}

