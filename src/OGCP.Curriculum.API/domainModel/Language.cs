using OGCP.Curriculum.API.domainModel;

namespace OGCP.Curriculum.API.domainmodel;

public class Language
{
    protected Language()
    {
    }
    private Language(Languages name, ProficiencyLevel level)
    {
        Name = name;
        Level = level;
    }

    // Properties
    public int Id { get; private set; }
    public Languages Name { get; private set; }
    public ProficiencyLevel Level { get; private set; }

    // Factory method for controlled instantiation
    public static Language Create(Languages name, ProficiencyLevel level)
    {
        return new Language(name, level);
    }

    // Domain method for updating proficiency level
    public void UpdateProficiencyLevel(ProficiencyLevel newLevel)
    {
        Level = newLevel;
    }
}
