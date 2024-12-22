namespace OGCP.Curriculums.Core.DomainModel.interfaces;
public interface IDomainEvent
{
    Task Accept();
}