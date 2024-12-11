using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculums.Core.DomainModel;
using OGCP.Curriculums.Core.DomainModel.profiles;
using OGCP.Curriculums.Core.DomainModel.valueObjects;

namespace OGCP.Curriculum.API.domainmodel;

public class GeneralProfile : Profile
{
    private string[] _personalGoals = Array.Empty<string>();
    private List<WorkExperience> _experiences = new();
    protected GeneralProfile() { }
    private GeneralProfile(
        Name name,
        string summary, 
        string[] personalGoals,
        PhoneNumber phone,
        Email email)
        : base(name, summary, phone, email)
    {
        _personalGoals = personalGoals ?? Array.Empty<string>();
    }
    //private GeneralProfile(int id, string firstName, string lastName, string summary, string[] personalGoals)
    //    :this(firstName, lastName, summary, personalGoals)
    //{
    //    this._id = id;
    //}

    public string[] PersonalGoals => _personalGoals;
    public virtual IReadOnlyList<WorkExperience> Experiences => _experiences;

    public static Result<GeneralProfile, Error> Create(
        Name name,
        string summary,
        string[] personalGoals,
        PhoneNumber phone,
        Email email)
    {
        if (string.IsNullOrWhiteSpace(summary))
        {
            return new Error("InvalidSummary", "Summary is required.");
        }

        return new GeneralProfile(name, summary, personalGoals, phone, email);
    }

    public void UpdatePersonalGoals(string[] personalGoals)
    {
        _personalGoals = personalGoals ?? Array.Empty<string>();
    }

    public void AddJobExperience(WorkExperience experience)
    {
        if (experience == null)
        {
            throw new ArgumentNullException(nameof(experience), "Job experience cannot be null.");
        }

        this._experiences.Add(experience);
    }

    public override Result UpdateProfile(Profile profile)
    {
        GeneralProfile generalProfile = (GeneralProfile)profile;
        base._name = generalProfile.Name;
        base._email = generalProfile.Email;
        base._summary = generalProfile.Summary;
        this._personalGoals = generalProfile.PersonalGoals;
        return Result.Success();
    }
}
