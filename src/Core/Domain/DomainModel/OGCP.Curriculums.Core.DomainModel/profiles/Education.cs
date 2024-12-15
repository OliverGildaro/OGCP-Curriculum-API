using ArtForAll.Shared.ErrorHandler;
//using CustomResult = ArtForAll.Shared.ErrorHandler.Results;

namespace OGCP.Curriculum.API.domainmodel;

public abstract class Education
{
    protected int _id;
    protected string _institution;
    protected DateOnly _startDate;
    protected DateOnly? _endDate;
    protected Education()
    {
        
    }
    public int Id => _id;
    public string Institution => _institution;

    //TODO: DateOnly
    //public DateOnly StartDate { get; set; }
    public DateOnly StartDate => _startDate;
    public DateOnly? EndDate => _endDate;

    public abstract bool IsEquivalent(Education other);
    public abstract Result Update(Education education);
}
