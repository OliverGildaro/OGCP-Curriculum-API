namespace OGCP.Curriculum.API.models;

public class SkillSet
{
    public List<ISkill> _skills;
    public virtual IReadOnlyList<ISkill> Skills => _skills;

    public virtual int Id => Id;
    public int _id;

    public virtual void AddSkill(ISkill skill)
    {
        throw new NotImplementedException();
    }
}
