using ArtForAll.Shared.Contracts.DDD;
using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.domainModel;

namespace OGCP.Curriculum.API.domainmodel;

public interface IEntity<TEntityId>
{
    public TEntityId Id { get; }
}

public class Profile : IEntity<int>
{
    private readonly List<Language> _languagesSpoken;
    //private readonly List<Skill> _skills;
    private DateTime _createdAt;
    private DateTime _updatedAt;

    private readonly List<JobExperience> _workExperience;
    public int Id { get; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Summary { get; private set; }
    public bool IsPublic { get; private set; }
    public string Visibility { get; private set; }
    public ProfileDetailLevel DetailLevel { get; private set; }
    public DateTime CreatedAt => _createdAt;
    public DateTime UpdatedAt => _updatedAt;
    public DetailInfo PersonalInfo { get; private set; }
    public List<Language> LanguagesSpoken => _languagesSpoken;
    public List<JobExperience> WorkExperience => _workExperience;

    //public List<Skill> Skills => _skills;

    // Protected or private constructors to enforce controlled object creation
    protected Profile()
    {
        _languagesSpoken = new List<Language>();
        //_skills = new List<Skill>();
        this._workExperience = new List<JobExperience>();

    }

    public Profile(string firstName, string lastName, string summary)
    {
        FirstName = firstName;
        LastName = lastName;
        Summary = summary;
        IsPublic = true;
        Visibility = string.Empty;
        _createdAt = DateTime.UtcNow;
        _updatedAt = DateTime.UtcNow;
        _languagesSpoken = new List<Language>();
        //_skills = new List<Skill>();
    }

    public Result AddLanguage(Language language)
    {
        if (language == null) return Result.Failure("");

        _languagesSpoken.Add(language);
        UpdateTimestamp();
        return Result.Success();
    }

    private void UpdateTimestamp()
    {
        _updatedAt = DateTime.UtcNow;
    }

    public void AddJobExperience(JobExperience workExperience)
    {
        if (workExperience == null)
        {
            throw new ArgumentNullException(nameof(workExperience), "Job experience cannot be null.");
        }

        _workExperience.Add(workExperience);
    }
}

public class QualifiedProfile : Profile
{
    private readonly EducationList _educations;

    private string _desiredJobRole;
    protected QualifiedProfile() { }
    private QualifiedProfile(
        string firstName,
        string lastName,
        string summary,
        string desiredJobRole)
        : base(firstName, lastName, summary)
    {
        _desiredJobRole = desiredJobRole;
    }

    // Public properties for encapsulated access
    public string DesiredJobRole => _desiredJobRole;

    //public EducationList Educations => _educations;
    public List<Education> Educations => _educations.Educations;

    // Factory method to create a QualifiedProfile
    public static Result<QualifiedProfile, Error> Create(
        string firstName,
        string lastName,
        string summary,
        string desiredJobRole)
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

        if (string.IsNullOrWhiteSpace(desiredJobRole))
        {
            return new Error("InvalidDesiredJobRole", "Desired job role is required.");
        }

        return new QualifiedProfile(firstName, lastName, summary, desiredJobRole);
    }

    // Domain methods to add related information
    public void AddEducation(Education education)
    {
        if (education == null)
        {
            throw new ArgumentNullException(nameof(education), "Education cannot be null.");
        }

        _educations.Add(education);
    }


}

public class GeneralProfile : Profile
{
    private string[] _personalGoals = Array.Empty<string>();

    protected GeneralProfile() { }
    private GeneralProfile(string firstName, string lastName, string summary, string[] personalGoals)
        : base(firstName, lastName, summary)
    {
        _personalGoals = personalGoals ?? Array.Empty<string>();
    }

    // Public properties
    public string[] PersonalGoals => _personalGoals;

    // Factory method for controlled creation
    public static Result<GeneralProfile, Error> Create(
        string firstName,
        string lastName,
        string summary,
        string[] personalGoals)
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

        return new GeneralProfile(firstName, lastName, summary, personalGoals);
    }

    public void UpdatePersonalGoals(string[] personalGoals)
    {
        _personalGoals = personalGoals ?? Array.Empty<string>();
    }
}

public class StudentProfile : Profile
{
    private readonly List<InternshipExperience> _internships = new();
    private readonly List<ResearchEducation> _researchEducation = new();
    private string _major;
    private string _careerGoals;

    protected StudentProfile()
    {
        
    }

    public StudentProfile(
        string firstName,
        string lastName,
        string summary,
        string major,
        string careerGoals)
        : base(firstName, lastName, summary)
    {
        _major = major;
        _careerGoals = careerGoals;
    }

    // Public properties
    public string Major => _major;
    public string CareerGoals => _careerGoals;
    public IReadOnlyCollection<InternshipExperience> Internships => _internships.AsReadOnly();
    public IReadOnlyCollection<ResearchEducation> ResearchEducation => _researchEducation.AsReadOnly();

    // Factory method for controlled creation
    public static Result<StudentProfile, Error> Create(
        string firstName,
        string lastName,
        string summary,
        string major,
        string careerGoals)
    {
        return new StudentProfile(firstName, lastName, summary, major, careerGoals);
    }

    public void AddEducation(ResearchEducation education)
    {
        if (education == null)
        {
            throw new ArgumentNullException(nameof(education), "Research education cannot be null.");
        }

        _researchEducation.Add(education);
    }
}
