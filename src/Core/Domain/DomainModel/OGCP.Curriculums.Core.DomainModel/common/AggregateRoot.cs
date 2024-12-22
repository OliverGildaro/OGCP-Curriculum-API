using OGCP.Curriculums.Core.DomainModel.interfaces;

namespace OGCP.Curriculums.Core.DomainModel.common;
public abstract class AggregateRoot
{
    private List<IDomainEvent> domainEvents = new List<IDomainEvent>();
    public virtual IReadOnlyList<IDomainEvent> DomainEvents => domainEvents;

    protected virtual void AddDomainEvent(IDomainEvent eventItem)
    {
        domainEvents = domainEvents ?? new List<IDomainEvent>();
        domainEvents.Add(eventItem);
    }

    public virtual void RemoveDomainEvent()
    {
        domainEvents?.Clear();
    }
}

