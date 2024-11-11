namespace OGCP.Curriculum.API.models;

public class EngineerSkill : ISkill
{
    private readonly string _text;
    private readonly string _value;
    private readonly int _id;
    public string Text => _text;
    public string Value => _value;
    public int Id => _id;

    public string text => throw new NotImplementedException();

    public EngineerSkill(string text, string value)
    {
        _text = text;
        _value = value;
    }
}
