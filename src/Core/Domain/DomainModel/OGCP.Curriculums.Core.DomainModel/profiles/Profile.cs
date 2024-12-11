using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculums.Core.DomainModel.profiles;
using OGCP.Curriculums.Core.DomainModel.valueObjects;

namespace OGCP.Curriculum.API.domainmodel;

//Always make non leaf clases in the herarchy abstract
//If we makes this one abstract we do not have any other choice than instantiate a leaf class
//It helps to see the full picture
//Ad an specific profile help us to see what kind of profiles we can create
public abstract class Profile : IEntity<int>
{
    protected int _id;
    protected Name _name;
    protected string _summary;
    protected Email _email;
    protected PhoneNumber _phone;
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

    protected Profile(
        Name name,
        string summary, 
        PhoneNumber phoneNumber,
        Email email)
    {
        this._name = name ?? throw new ArgumentNullException(nameof(name));
        this._summary = summary;
        this._isPublic = true;
        this._visibility = string.Empty;
        this._phone = phoneNumber;
        this._email = email;
        _createdAt = DateTime.UtcNow;
        _updatedAt = DateTime.UtcNow;
    }

    public int Id => _id;
    public Name Name => _name;
    public string Summary => _summary;
    public PhoneNumber Phone => _phone;
    public Email Email => _email;
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
            throw new ArgumentNullException(nameof(language), "Language cannot be null.");
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
