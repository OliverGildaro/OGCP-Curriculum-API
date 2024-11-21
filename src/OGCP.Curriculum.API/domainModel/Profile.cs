using ArtForAll.Shared.Contracts.DDD;
using ArtForAll.Shared.ErrorHandler;

namespace OGCP.Curriculum.API.domainmodel;

public interface IEntity<TEntityId>
{
    public TEntityId Id { get; }
}

public interface IProfile : IEntity<int>
{
    public Result AddLanguage(Language language);
}

public class Profile : IProfile
{
    private int _id;
    private string _firstName;
    private string _lastName;
    private string _summary;
    private bool _isPublic;
    private string _visibility;
    private ProfileDetailLevel _detailLevel;
    private DateTime _createdAt;
    private DateTime _updatedAt;
    private DetailInfo _personalInfo;//One to one
    private readonly List<Language> _languagesSpoken;//Many to many

    protected Profile() {}

    public Profile(string firstName, string lastName, string summary)
    {
        this._firstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        this._lastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        this._summary = summary;
        this._isPublic = true;
        this._visibility = string.Empty;
        _createdAt = DateTime.UtcNow;
        _updatedAt = DateTime.UtcNow;
        _languagesSpoken = new List<Language>();
    }

    public int Id => _id;
    public string FirstName => _firstName;
    public string LastName => _lastName;
    public string Summary => _summary;
    public bool IsPublic => _isPublic;
    public string Visibility => _visibility;
    public ProfileDetailLevel DetailLevel => _detailLevel;
    public DateTime CreatedAt => _createdAt;
    public DateTime UpdatedAt => _updatedAt;
    public DetailInfo PersonalInfo => _personalInfo;
    public List<Language> LanguagesSpoken => _languagesSpoken;
    
    public virtual Result AddLanguage(Language language)
    {
        //if (_languagesSpoken.Any(l => l.IsEquivalent(language)))
        //{
        //    return Result.Failure($"{language.Name} can not be added twice");
        //}

        var currentLanguage = this.LanguagesSpoken.FirstOrDefault(l => l.Name == language.Name);
        if (currentLanguage != null)
        {
            return Result.Failure($"Invalid operation: two same languages can not be added");
        }
        
        this._languagesSpoken.Add(language);

        UpdateTimestamp();
        return Result.Success();
    }

    private void UpdateTimestamp()
    {
        _updatedAt = DateTime.UtcNow;
    }

    internal Result EditLanguage(Language language)
    {
        if (this.LanguagesSpoken.Any(lang => lang.Id != language.Id && lang.Name == language.Name))
        {
            return Result.Failure($"Invalid operation: two same languages can not be added");
        }

        var languageToUpdate = this.LanguagesSpoken.FirstOrDefault(l => l.Id == language.Id);

        if (languageToUpdate == null)
        {
            return Result.Failure($"A language with this id: {language.Id}, not found");
        }

        languageToUpdate.Update(language);
        UpdateTimestamp();
        return Result.Success();
    }

    private bool IsValidToRemoveLanguage(int languageId)
    {
        return this._languagesSpoken.Any(lang => lang.Id == languageId);
    }

    internal Result RemoveLanguage(int languageId)
    {
        if(this.IsValidToRemoveLanguage(languageId))
        {
            var language = this._languagesSpoken.Find(lang => lang.Id == languageId);
            this._languagesSpoken.Remove(language);
            return Result.Success();
        }

        return Result.Failure($"The profile id: {languageId}, not found");

    }
}

public interface IQualifiedProfile
{
    public Result AddEducation(Education education);
    public Result AddJobExperience(JobExperience experience);
}

public class QualifiedProfile : Profile, IQualifiedProfile
{
    private string _desiredJobRole;
    private readonly List<Education> _educations;//many to many
    private List<JobExperience> _experiences;//one to many
    protected QualifiedProfile() { }
    private QualifiedProfile(
        string firstName,
        string lastName,
        string summary,
        string desiredJobRole)
        : base(firstName, lastName, summary)
    {
        _desiredJobRole = desiredJobRole;
        this._educations = new List<Education>();
        _experiences = new List<JobExperience>();
    }

    public string DesiredJobRole => _desiredJobRole;

    //public EducationList Educations => _educations;
    public List<Education> Educations => _educations;
    public List<JobExperience> Experiences => _experiences;

    public static Result<QualifiedProfile, Error> Create(
        string firstName,
        string lastName,
        string summary,
        string desiredJobRole)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            return new Error("First name is required.", "InvalidFirstName");
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            return new Error("Last name is required.", "InvalidLastName");
        }

        if (string.IsNullOrWhiteSpace(desiredJobRole))
        {
            return new Error("Desired job role is required.", "InvalidDesiredJobRole");
        }

        return new QualifiedProfile(firstName, lastName, summary, desiredJobRole);
    }

    public Result AddEducation(Education education)
    {
        if (_educations.Any(educ => educ.IsEquivalent(education)))
        {
            return Result.Failure($"{education.Institution} can not be added twice");
        }

        var currentLanguage = this._educations.FirstOrDefault(currentEduc => currentEduc == education);

        if (currentLanguage != null)
        {
            var index = _educations.IndexOf(currentLanguage);
            _educations[index] = education;
        }
        else
        {
            this._educations.Add(education);
        }

        //UpdateTimestamp();
        return Result.Success();
    }

    public Result AddJobExperience(JobExperience workExperience)
    {
        if (this.Experiences.Any(e => e.IsEquivalent(workExperience)))
        {
            return Result.Failure("This work experience can not be added twice");
        }

        this._experiences.Add(workExperience);

        return Result.Success();
    }
}

public interface IGeneralProfile
{
    public void AddJobExperience(WorkExperience experience);
}

public class GeneralProfile : Profile, IGeneralProfile
{
    private string[] _personalGoals = Array.Empty<string>();
    private List<WorkExperience> _experiences;//One to many
    protected GeneralProfile() { }
    private GeneralProfile(string firstName, string lastName, string summary, string[] personalGoals)
        : base(firstName, lastName, summary)
    {
        _personalGoals = personalGoals ?? Array.Empty<string>();
        this._experiences = new List<WorkExperience>();
    }

    public string[] PersonalGoals => _personalGoals;
    public IReadOnlyList<WorkExperience> Experiences => _experiences;

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

    public void AddJobExperience(WorkExperience experience)
    {
        if (experience == null)
        {
            throw new ArgumentNullException(nameof(experience), "Job experience cannot be null.");
        }

        this._experiences.Add(experience);
    }
}

public interface IStudentProfile
{
    public void AddEducation(ResearchEducation education);
    public void AddJobExperience(InternshipExperience experience);
}

public class StudentProfile : Profile, IStudentProfile
{
    private string _major;
    private string _careerGoals;
    private readonly List<ResearchEducation> _educations = new();//many to many
    private readonly List<InternshipExperience> _experience = new();//one to many

    protected StudentProfile()
    {
        this._educations = new List<ResearchEducation>();
        this._experience = new List<InternshipExperience>();
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
    public IReadOnlyCollection<ResearchEducation> Educations => _educations.AsReadOnly();
    public IReadOnlyCollection<InternshipExperience> Experiences => _experience.AsReadOnly();

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

        _educations.Add(education);
    }

    public void AddJobExperience(InternshipExperience experience)
    {
        this._experience.Add(experience);
    }
}
