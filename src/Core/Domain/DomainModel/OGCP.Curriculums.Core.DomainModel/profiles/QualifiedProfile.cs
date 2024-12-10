using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculums.Core.DomainModel;
using OGCP.Curriculums.Core.DomainModel.profiles;
using static OGCP.Curriculums.Core.DomainModel.Errors;

namespace OGCP.Curriculum.API.domainmodel;

public class QualifiedProfile : Profile
{
    private string _desiredJobRole;
    private readonly List<Education> _educations = new();
    private List<JobExperience> _experiences = new();
    protected QualifiedProfile() { }
    private QualifiedProfile(
        string firstName,
        string lastName,
        string summary,
        string desiredJobRole,
        PhoneNumber phone,
        string email)
        : base(firstName, lastName, summary, phone, email)
    {
        _desiredJobRole = desiredJobRole;
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
        string desiredJobRole,
        PhoneNumber phone,
        string email)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            return Errors.Validation.ValueIsRequired(nameof(firstName));
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            return Errors.Validation.ValueIsRequired(nameof(lastName));
        }

        return new QualifiedProfile(firstName, lastName, summary, desiredJobRole, phone, email);
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
            throw new ArgumentNullException(nameof(education), "Education cannot be null.");
        }

        if (currentEducation == education)
        {
            return Result.Success("Education already updated");
        }

        if (_educations.Any(educ => educ.IsEquivalent(education)))
        {
            return Result.Failure(General.EntityCanNotBeAddedTwice("EDUCATION", "Education").Message);
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

}
