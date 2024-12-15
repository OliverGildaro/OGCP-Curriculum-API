using ArtForAll.Shared.Contracts.DDD;

namespace OGCP.Curriculum.API.domainmodel;

public abstract class JobExperience
{
    // Properties
    public int Id { get; protected set; }
    public string Company { get; protected set; }
    protected JobExperience()
    {
        
    }

    //TODO: DateOnly
    //public DateOnly StartDate { get; set; }
    public DateTime StartDate { get; protected set; }
    public DateTime? EndDate { get; protected set; }
    public string Description { get; protected set; }
    public abstract bool IsEquivalent(JobExperience jobExperience);
}
