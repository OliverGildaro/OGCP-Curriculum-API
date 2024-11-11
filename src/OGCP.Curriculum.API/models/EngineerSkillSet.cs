namespace OGCP.Curriculum.API.models;

public class EngineerSkillSet
{
    private List<EngineerSkill> _skills;
    private readonly int _id;

    public IReadOnlyList<EngineerSkill> Skills => _skills;
    public int Id => _id;

    public void AddSkill(EngineerSkill set)
    {
        throw new NotImplementedException();
    }
}
