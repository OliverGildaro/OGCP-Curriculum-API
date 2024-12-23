using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculums.Core.DomainModel.common;
using OGCP.Curriculums.Core.DomainModel.Events;
using OGCP.Curriculums.Core.DomainModel.Images;
using OGCP.Curriculums.Core.DomainModel.profiles;
using OGCP.Curriculums.Core.DomainModel.valueObjects;

namespace OGCP.Curriculum.API.domainmodel;

//Always make non leaf clases in the herarchy abstract
//If we makes this one abstract we do not have any other choice than instantiate a leaf class
//It helps to see the full picture
//Ad an specific profile help us to see what kind of profiles we can create
public abstract class Profile : AggregateRoot, IEntity<int>
{
    protected List<ProfileLanguage> _languagesSpoken = new List<ProfileLanguage>();//Many to many

    protected int _id;
    protected Name _name;
    protected string _summary;
    protected Email _email;
    protected PhoneNumber _phone;
    private Image image;

    protected bool _isPublic;
    protected string _visibility;
    protected ProfileDetailLevel _detailLevel;
    protected DateTime _createdAt;

    //TODO: DateOnly
    //public DateOnly StartDate { get; set; }
    //public TimeOnly StartTime { get; set; }
    protected DateTime _updatedAt;
    protected DetailInfo _personalInfo;//One to one

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
    public virtual Image Image => this.image;
    public bool IsPublic => _isPublic;
    public string Visibility => _visibility;
    public ProfileDetailLevel DetailLevel => _detailLevel;
    public DateTime CreatedAt => _createdAt;
    public DateTime UpdatedAt => _updatedAt;
    public virtual DetailInfo PersonalInfo => _personalInfo;
    public virtual IReadOnlyList<ProfileLanguage> LanguagesSpoken => this._languagesSpoken;
    
    public virtual Result AddLanguage(Language language)
    {
        if (language is null)
        {
            throw new ArgumentNullException(nameof(language), "Language cannot be null.");
        }

        var currentLanguage = this.LanguagesSpoken.FirstOrDefault(l => l.Language == language);
        if (currentLanguage != null)
        {
            return Result.Failure($"{language.Name} can not be added twice");
        }
        var profiLang = ProfileLanguage.CreateNew(language);

        if (profiLang.IsFailure)
        {

        }
        this._languagesSpoken.Add(profiLang.Value);

        UpdateTimestamp();
        return Result.Success();
    }

    protected void UpdateTimestamp()
    {
        _updatedAt = DateTime.UtcNow;
    }

    public Result EditLanguage(int languageId, Language language)
    {
        var profileLang = this.LanguagesSpoken.FirstOrDefault(l => l.Language.Id == languageId);

        if (profileLang == null)
        {
            return Result.Failure($"A language with this id: {language.Id}, not found");
        }

        profileLang.UpdateLanguage(language);
        UpdateTimestamp();
        return Result.Success();
    }

    public Result UpdateLanguageSkills(Language language, List<LanguageSkill> newSkills)
    {
        var profileLanguage = _languagesSpoken.FirstOrDefault(pl => pl.Language.Name == language.Name);
        if (profileLanguage == null)
            return Result.Failure($"Language {language.Name} not found.");

        profileLanguage.UpdateSkills(newSkills); // Update the skills in the ProfileLanguage

        return Result.Success();
    }

    private bool IsValidToRemoveLanguage(int languageId)
    {
        return this._languagesSpoken.Any(lang => lang.Language.Id == languageId);
    }

    public Result RemoveLanguage(int languageId)
    {
        if(this.IsValidToRemoveLanguage(languageId))
        {
            var language = this._languagesSpoken.Find(lang => lang.Language.Id == languageId);
            this._languagesSpoken.Remove(language);
            return Result.Success();
        }

        return Result.Failure($"The profile id: {languageId}, not found");

    }

    public Result AddLAnguageSkill(int id, LanguageSkill langSki)
    {
        if (langSki.Level.Equals(ProficiencyLevel.Native))
        {
            return Result.Failure("");
        }
        if (!this.LanguagesSpoken.Any(lang => lang.LanguageId.Equals(id)))
        {

        }

        if (this.LanguagesSpoken.Any(lang => lang.Equals(langSki)))
        {

        }

        ProfileLanguage profLang = this._languagesSpoken.FirstOrDefault(lang => lang.LanguageId == id);

        var resultAddSkill = profLang.AddNewLangSkill(langSki);

        if (resultAddSkill.IsFailure)
        {
            Result.Failure(resultAddSkill.Error.Message);
        }

        return Result.Success();
    }

    public abstract Result UpdateProfile(Profile profile);

    public bool AllowAddImageIsSuccess()
    {
        var allowAddImage = true;
        //if (this.State.Equals(StateEvent.DELETED))
        //{
        //    allowAddImage = false;
        //}

        //if (this.StartDate <= DateTime.UtcNow)
        //{
        //    allowAddImage = false;
        //}

        return allowAddImage;
    }

    public Result AddImage(Image image)
    {
        if (this.Image != null)
        {
            return Result.Failure("The image alreaady exist");
        }
        var imageAdded = new ImageAdded
        {
            Id = image.Id,
            CreatedAt = this.CreatedAt.ToString("yyyy-MM-dd"),
            ProfileId = this.Id,
            contentType = image.ContentType,
            fileName = image.FileName
        };

        this.AddDomainEvent(imageAdded);

        this.image = image;
        return Result.Success();
    }
}
