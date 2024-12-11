using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculums.Core.DomainModel.valueObjects;
using System.Runtime.CompilerServices;

namespace OGCP.Curriculums.Core.DomainModel.profiles;

public class ProfileLanguage
{
    public int ProfileId { get; private set; }
    public int LanguageId { get; private set; }
    public virtual Language Language { get; private set; }
    public virtual Profile Profile { get; private set; }

    private List<LanguageSkill> _languageSkills = new();

    protected ProfileLanguage() { }
    private ProfileLanguage(Language language, List<LanguageSkill> skills)
    {
        Language = language;
    }

    public IReadOnlyList<LanguageSkill> LanguageSkills  => _languageSkills;

    public Result<LanguageSkill, Error> AddNewLangSkill(LanguageSkill skill)
    {
        if (LanguageSkills.Any(s => s.Skill == skill.Skill))
            return Errors.Validation.DuplicateValue("Skill already exists.");

        _languageSkills.Add(skill);
        return skill;
    }

    internal static Result<ProfileLanguage, Error> CreateNew(Language language, List<LanguageSkill> skills = null)
    {
        skills ??= Enumerable.Empty<LanguageSkill>().ToList();

        if (language == null)
        {
            return Errors.General.NotFound(1);
        }

        var profileLanguage = new ProfileLanguage(language, skills);

        return Result<ProfileLanguage, Error>.Success(profileLanguage);
    }


    internal void UpdateLanguage(Language language)
    {
    }

    internal void UpdateSkills(List<LanguageSkill> newSkills)
    {
        throw new NotImplementedException();
    }
}

