namespace OGCP.Curriculum.API.domainmodel;

public interface IEntity<TEntityId>
{
    public TEntityId Id { get; }
}
