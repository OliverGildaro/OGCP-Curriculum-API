namespace OGCP.Curriculum.API.domainmodel;

public class Language
{
    public int Id { get; private set; }
    public Languages Name { get; private set; }
    public ProficiencyLevel Level { get; private set; }
    protected Language()
    {
    }
    private Language(Languages name, ProficiencyLevel level)
    {
        Name = name;
        Level = level;
    }

    public Language(Languages name, ProficiencyLevel level, int id)
        : this(name, level)
    {
        Id = id;
    }

    // Properties


    // Factory method for controlled instantiation
    public static Language Create(Languages name, ProficiencyLevel level)
    {
        return new Language(name, level);
    }

    public static Language Hidrate(Languages name, ProficiencyLevel level, int id)
    {
        return new Language(name, level, id);
    }

    // Domain method for updating proficiency level
    public void UpdateProficiencyLevel(ProficiencyLevel newLevel)
    {
        Level = newLevel;
    }

    public bool IsEquivalent(Language language)
    {
        return this.Name.Equals(language.Name)
            && this.Level.Equals(language.Level);
    }

    public void Update(Language language)
    {
        this.Name = language.Name;
        this.Level = language.Level;
    }
}
