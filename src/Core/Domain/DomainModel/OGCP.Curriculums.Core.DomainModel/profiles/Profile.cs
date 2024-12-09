using ArtForAll.Shared.Contracts.DDD;
using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculums.Core.DomainModel;

namespace OGCP.Curriculum.API.domainmodel;

public interface IEntity<TEntityId>
{
    public TEntityId Id { get; }
}

//Always make non leaf clases in the herarchy abstract
//If we makes this one abstract we do not have any other choice than instantiate a leaf class
//It helps to see the full picture
//Ad an specific profile help us to see what kind of profiles we can create
public abstract class Profile : IEntity<int>
{
    protected int _id;
    protected string _firstName;
    protected string _lastName;
    protected string _summary;
    protected bool _isPublic;
    protected string _visibility;
    protected ProfileDetailLevel _detailLevel;
    protected DateTime _createdAt;

    //TODO: DateOnly
    //public DateOnly StartDate { get; set; }
    //public TimeOnly StartTime { get; set; }
    protected DateTime _updatedAt;
    protected DetailInfo _personalInfo;//One to one
    protected List<Language> _languagesSpoken = new List<Language>();//Many to many

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
    public virtual DetailInfo PersonalInfo => _personalInfo;
    public virtual IReadOnlyList<Language> LanguagesSpoken => this._languagesSpoken;
    
    public virtual Result AddLanguage(Language language)
    {
        if (language is null)
        {
            return Result.Failure("Language can not be null");
        }

        var currentLanguage = this.LanguagesSpoken.FirstOrDefault(l => l.Name == language.Name);
        if (currentLanguage != null)
        {
            return Result.Failure($"{language.Name} can not be added twice");
        }
        
        this._languagesSpoken.Add(language);

        UpdateTimestamp();
        return Result.Success();
    }

    protected void UpdateTimestamp()
    {
        _updatedAt = DateTime.UtcNow;
    }

    public Result EditLanguage(int languageId, Language language)
    {
        var languageToUpdate = this.LanguagesSpoken.FirstOrDefault(l => l.Id == languageId);

        if (languageToUpdate == null)
        {
            return Result.Failure($"A language with this id: {language.Id}, not found");
        }

        this._languagesSpoken.Remove(languageToUpdate);
        this._languagesSpoken.Add(language);
        UpdateTimestamp();
        return Result.Success();
    }

    private bool IsValidToRemoveLanguage(int languageId)
    {
        return this._languagesSpoken.Any(lang => lang.Id == languageId);
    }

    public Result RemoveLanguage(int languageId)
    {
        if(this.IsValidToRemoveLanguage(languageId))
        {
            var language = this._languagesSpoken.Find(lang => lang.Id == languageId);
            this._languagesSpoken.Remove(language);
            return Result.Success();
        }

        return Result.Failure($"The profile id: {languageId}, not found");

    }

    public abstract Result UpdateProfile(Profile profile);
}

public interface IQualifiedProfile
{
    public Result AddEducation(Education education);
    public Result AddJobExperience(JobExperience experience);
}

public class QualifiedProfile : Profile, IQualifiedProfile
{
    private string _desiredJobRole;
    private readonly List<Education> _educations = new();
    private List<JobExperience> _experiences = new();
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

    public QualifiedProfile(int id, string firstName, string lastName, string summary, string desiredJobRole)
        :this(firstName, lastName, summary, desiredJobRole)
    {
        this._id = id;
    }

    public string DesiredJobRole => _desiredJobRole;

    //public EducationList Educations => _educations;
    public virtual IReadOnlyList<Education> Educations => _educations;
    public virtual IReadOnlyList<JobExperience> Experiences => _experiences;

    //ENCAPSULATION is about protect data integrity
    //Avoid classes enter in an invalid state
    //ENCAPSULATION and ABSTRACTION goes together
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

    //ABSTRACTION is about focus on a single application concern
    //ABSTRACTION is related to the single responsability
    //ABSTRACTION can be move a set of lines of code to its own method
    //SO how the code works is irrelevant for the caller method
    //ABSTRACTION is about code simplification
    //ABSTRACTION is to Hide implementation details
    public Result AddEducation(Education education)
    {
        if (_educations.Any(educ => educ.IsEquivalent(education)))
        {
            return Result.Failure($"{education.Institution} can not be added twice");
        }

        this._educations.Add(education);

        UpdateTimestamp();
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

    public Result UpdateEducation(int educationId, Education education)
    {
        var currentEducation = this.Educations.FirstOrDefault(e => e.Id.Equals(educationId));
        if (currentEducation == null) 
        {
            return Result.Failure("");
        }

        if (currentEducation == education)
        {
            return Result.Success("Education already updated");
        }

        if (_educations.Any(educ => educ.IsEquivalent(education)))
        {
            return Result.Failure("Not possible to do this update, conflict with an existing degree");
        }

        //Si education ya tiene un id habria que remover en el join table la relacion y agregar una nueva
        //Relacion a la instancie existente en la tabla de educaciones

        _educations.Remove(currentEducation);
        _educations.Add(education);
        //currentEducation.Update(education);
        return Result.Success();
    }

    public Result RemoveEducation(int educationId)
    {
        if (this.Educations.Any(edu => edu.Id == educationId))
        {
            var educationToRemove = this._educations.Find(edu => edu.Id == educationId);
            this._educations.Remove(educationToRemove);
            return Result.Success();
        }

        return Result.Failure($"The profile id: {educationId}, not found");
    }

    public override Result UpdateProfile(Profile profile)
    {
        QualifiedProfile studentProfile = (QualifiedProfile)profile;
        base._firstName = studentProfile.FirstName;
        base._summary = studentProfile.Summary;
        this._desiredJobRole = studentProfile.DesiredJobRole;
        return Result.Success();
    }

    public static Result<Profile, Error> Hidrate(int id, string firstName, string lastName, string summary, string desiredJobRole)
    {
        return new QualifiedProfile(id, firstName, lastName, summary, desiredJobRole);
    }
}

public interface IGeneralProfile
{
    public void AddJobExperience(WorkExperience experience);
}

public class GeneralProfile : Profile, IGeneralProfile
{
    private string[] _personalGoals = Array.Empty<string>();
    private List<WorkExperience> _experiences = new();
    protected GeneralProfile() { }
    private GeneralProfile(string firstName, string lastName, string summary, string[] personalGoals)
        : base(firstName, lastName, summary)
    {
        _personalGoals = personalGoals ?? Array.Empty<string>();
    }

    private GeneralProfile(int id, string firstName, string lastName, string summary, string[] personalGoals)
        :this(firstName, lastName, summary, personalGoals)
    {
        this._id = id;
    }

    public string[] PersonalGoals => _personalGoals;
    public virtual IReadOnlyList<WorkExperience> Experiences => _experiences;

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

    public override Result UpdateProfile(Profile profile)
    {
        GeneralProfile studentProfile = (GeneralProfile)profile;
        base._firstName = studentProfile.FirstName;
        base._lastName = studentProfile.LastName;
        base._summary = studentProfile.Summary;
        this._personalGoals = studentProfile.PersonalGoals;
        return Result.Success();
    }

    public static Result<Profile, Error> Hidrate(int id, string firstName, string lastName, string summary, string[] personalGoals)
    {
        return new GeneralProfile(id, firstName, lastName, summary, personalGoals);
    }
}

public interface IStudentProfile
{
    public Result AddEducation(ResearchEducation education);
    public void AddJobExperience(InternshipExperience experience);
}

public class StudentProfile : Profile, IStudentProfile
{
    private string _major;
    private string _careerGoals;
    private List<ResearchEducation> _educations = new();
    private List<InternshipExperience> _experiences = new();

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

    public StudentProfile(int id, string firstName, string lastName, string summary, string major, string careerGoals)
        : this(firstName, lastName, summary, major, careerGoals)
    {
        this._id = id;
    }

    // Public properties
    public string Major => _major;
    public string CareerGoals => _careerGoals;
    //we need to mark as virtual to enable lazy loading
    public virtual IReadOnlyList<ResearchEducation> Educations => _educations;
    public virtual IReadOnlyList<InternshipExperience> Experiences => _experiences;

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

    public Result AddEducation(ResearchEducation education)
    {
        if (_educations.Any(educ => educ.IsEquivalent(education)))
        {
            return Result.Failure($"{education.Institution} can not be added twice");
        }

        //var currentLanguage = this._educations.FirstOrDefault(currentEduc => currentEduc == education);

        //if (currentLanguage != null)
        //{
        //    var index = _educations.IndexOf(currentLanguage);
        //    _educations[index] = education;
        //}
        //else
        //{
        //}
        this._educations.Add(education);

        return Result.Success();
    }

    public void AddJobExperience(InternshipExperience experience)
    {
        this._experiences.Add(experience);
    }

    public Result RemoveEducation(int educationId)
    {
        if (this.Educations.Any(edu => edu.Id == educationId))
        {
            var educationToRemove = this._educations.Find(edu => edu.Id == educationId);
            this._educations.Remove(educationToRemove);
            return Result.Success();
        }

        return Result.Failure($"The profile id: {educationId}, not found");
    }

    public Result UpdateEducation(int educationId, ResearchEducation education)
    {
        var currentEducation = this.Educations.FirstOrDefault(e => e.Id.Equals(educationId));
        if (currentEducation == null)
        {
            return Result.Failure("");
        }

        if (currentEducation == education)
        {
            return Result.Success("Education already updated");
        }

        if (_educations.Any(educ => educ.IsEquivalent(education)))
        {
            return Result.Failure("Not possible to do this update, conflict with an existing degree");
        }

        _educations.Remove(currentEducation);
        _educations.Add(education);
        return Result.Success();
    }

    public override Result UpdateProfile(Profile profile)
    {
        StudentProfile studentProfile = (StudentProfile)profile;
        base._firstName = studentProfile.FirstName;
        base._lastName = studentProfile.LastName;
        base._summary = studentProfile.Summary;
        this._major = studentProfile.Major;
        this._careerGoals = studentProfile.CareerGoals;
        return Result.Success();
    }

    public static Result<Profile, Error> Hidrate(int id, string firstName, string lastName, string summary, string major, string careerGoals)
    {
        return new StudentProfile(id, firstName, lastName, summary, major,  careerGoals);
    }
}
