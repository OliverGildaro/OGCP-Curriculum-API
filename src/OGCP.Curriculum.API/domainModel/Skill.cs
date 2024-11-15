using OGCP.Curriculum.API.domainModel;

namespace OGCP.Curriculum.API.models;

public class ProfessionSkill : IEntity<int>
{
    private int _id;
    private ProfessionTypes _name;
    private List<SkillSet> _skillSet;

    protected ProfessionSkill()
    {
        
    }

    public int Id => this._id;
    public List<Skill> SkillSet => this.SkillSet;
    public ProfessionTypes Name => this._name;
}

public class SkillSet : IEntity<int>
{
    private int _id;
    public CategorySkill _category;
    private List<SkillSet> _professionSkill;

    public SkillSet()
    {
        
    }
    public int Id => this._id;
    public CategorySkill CategorySkill => this._category;
    public List<SkillSet> ProfessionSkill  => this._professionSkill;
}

public class Skill : IEntity<int>
{
    private int _id;
    private ProficiencyLevel _level;
    private List<SourceSkill> _sources;
    protected Skill() 
    {
    }
    public Skill(ProficiencyLevel level)
    {
        this._level = level;
        this._sources = new List<SourceSkill>();

    }
    public ProficiencyLevel Level => this.Level;
    public int Id => _id;
    public List<SourceSkill> Sources => this._sources;
}

public class SourceSkill
{
    private string _url;

    public SourceSkill()
    {
        
    }
    public string Url => this._url;
}