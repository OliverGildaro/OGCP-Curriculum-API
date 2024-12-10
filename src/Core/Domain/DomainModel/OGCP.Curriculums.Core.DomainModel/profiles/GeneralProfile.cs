using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculums.Core.DomainModel;
using OGCP.Curriculums.Core.DomainModel.profiles;

namespace OGCP.Curriculum.API.domainmodel;

public class GeneralProfile : Profile
{
    private string[] _personalGoals = Array.Empty<string>();
    private List<WorkExperience> _experiences = new();
    protected GeneralProfile() { }
    private GeneralProfile(
        string firstName,
        string lastName,
        string summary, 
        string[] personalGoals,
        PhoneNumber phone,
        string email)
        : base(firstName, lastName, summary, phone, email)
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
        string firstName,
        string lastName,
        string summary,
        string[] personalGoals,
        PhoneNumber phone,
        string email)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            return new Error("InvalidFirstName", "First name is required.");
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            return new Error("InvalidLastName", "Last name is required.");
        }

        if (string.IsNullOrWhiteSpace(summary))
        {
            return new Error("InvalidSummary", "Summary is required.");
        }

        return new GeneralProfile(firstName, lastName, summary, personalGoals, phone, email);
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
        GeneralProfile studentProfile = (GeneralProfile)profile;
        base._firstName = studentProfile.FirstName;
        base._lastName = studentProfile.LastName;
        base._summary = studentProfile.Summary;
        this._personalGoals = studentProfile.PersonalGoals;
        return Result.Success();
    }
}
