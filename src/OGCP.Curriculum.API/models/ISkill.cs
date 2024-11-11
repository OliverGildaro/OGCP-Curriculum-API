namespace OGCP.Curriculum.API.models;


public interface IEntity<tId>
{
    public tId Id { get; }
}

public interface IWorker<tId, tSkillSet> : IEntity<tId>
    where tSkillSet : SkillSet
{
    public IReadOnlyList<tSkillSet> Skills { get; }
}

public interface ISkillSet<tId, tSkill> : IEntity<tId>
{
    public IReadOnlyList<tSkill> Skills { get; }
    public void AddSkill(tSkill skill);
}

public interface ISkill : IEntity<int>
{
    public string text { get; }
    public string Value { get; }
}

public class DevSkill : EngineerSkill
{
    public DevSkill(string text, string value) : base(text, value)
    {
    }
}

public enum EnumSkill
{
    MONGODB = 1,
    MICROSOFt_SQL_SERVER,
    DYNAMO_DB,
    ASP,
    ENtItYFRAMEWORK,
    CSHARP,
    JAVASCRIPt,
    REACt,
    VUE
}
