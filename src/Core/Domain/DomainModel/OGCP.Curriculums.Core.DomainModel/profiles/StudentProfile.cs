using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculums.Core.DomainModel;
using OGCP.Curriculums.Core.DomainModel.profiles;

namespace OGCP.Curriculum.API.domainmodel;

public class StudentProfile : Profile
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
        string careerGoals,
        PhoneNumber phone,
        string email)
        : base(firstName, lastName, summary, phone, email)
    {
        _major = major;
        _careerGoals = careerGoals;
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
        string careerGoals,
        PhoneNumber phone,
        string email)
    {
        return new StudentProfile(firstName, lastName, summary, major, careerGoals, phone, email);
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

}
