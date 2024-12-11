using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.ErrorHandler.Results;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculums.Core.DomainModel;
using OGCP.Curriculums.Core.DomainModel.profiles;
using OGCP.Curriculums.Core.DomainModel.valueObjects;
using CustomResult = ArtForAll.Shared.ErrorHandler.Results;
namespace OGCP.Curriculum.API.commanding.commands.CreateQualifiedProfile;

public abstract class CreateProfileCommand : ICommand
{
    public Name Name { get; set; }
    public string Summary { get; set; }
    public PhoneNumber Phone { get; set; }
    public Email Email { get; set; }
    public abstract CustomResult.IResult<Profile, Error> MapTo();
}

public class CreateQualifiedProfileCommand : CreateProfileCommand
{
    public string DesiredJobRole { get; set; }

    //public void Deconstruct(out string firstName, out string lastName, out string summary, out string desiredJobRole)
    //{
    //    firstName = FirstName;  // Assuming FirstName is a property in ProfileRequest
    //    lastName = LastName;    // Assuming LastName is a property in ProfileRequest
    //    summary = Summary;      // Assuming Summary is a property in ProfileRequest
    //    desiredJobRole = DesiredJobRole;
    //}

    public override IResult<Profile, Error> MapTo()
    {
        return QualifiedProfile.Create(Name, Summary, DesiredJobRole, Phone, Email);
    }
}

public class CreateGeneralProfileCommand : CreateProfileCommand
{
    public string[] PersonalGoals { get; set; }

    //public void Deconstruct(out string firstName, out string lastName, out string summary, out string[] personalGoals)
    //{
    //    firstName = FirstName;  // Assuming FirstName is a property in ProfileRequest
    //    lastName = LastName;    // Assuming LastName is a property in ProfileRequest
    //    summary = Summary;      // Assuming Summary is a property in ProfileRequest
    //    personalGoals = PersonalGoals;
    //}

    public override IResult<Profile, Error> MapTo()
    {
        return GeneralProfile.Create(Name, Summary, PersonalGoals, Phone, Email);
    }
}

public class CreateStudentProfileCommand : CreateProfileCommand
{
    public string Major { get; set; }
    public string CareerGoals { get; set; }

    //public void Deconstruct(out string firstName, out string lastName, out string summary, out string major, out string careerGoals)
    //{
    //    firstName = FirstName;  // Assuming FirstName is a property in ProfileRequest
    //    lastName = LastName;    // Assuming LastName is a property in ProfileRequest
    //    summary = Summary;      // Assuming Summary is a property in ProfileRequest
    //    major = Major;
    //    careerGoals = CareerGoals;
    //}

    public override IResult<Profile, Error> MapTo()
    {
        return StudentProfile.Create(Name, Summary, Major, CareerGoals, Phone, Email);
    }
}
