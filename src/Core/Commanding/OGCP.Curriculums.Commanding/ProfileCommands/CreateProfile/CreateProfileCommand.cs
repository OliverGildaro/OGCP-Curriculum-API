using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.Contracts.DDD;
using ArtForAll.Shared.ErrorHandler;
using ArtForAll.Shared.ErrorHandler.Results;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculums.Core.DomainModel;
using CustomResult = ArtForAll.Shared.ErrorHandler.Results;
namespace OGCP.Curriculum.API.commanding.commands.CreateQualifiedProfile;

public abstract class CreateProfileCommand : ICommand
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Summary { get; set; }
    public abstract CustomResult.IResult<Profile, Error> MapTo();
}

public class CreateQualifiedProfileCommand : CreateProfileCommand
{
    public string DesiredJobRole { get; set; }

    public void Deconstruct(out string firstName, out string lastName, out string summary, out string desiredJobRole)
    {
        firstName = FirstName;  // Assuming FirstName is a property in ProfileRequest
        lastName = LastName;    // Assuming LastName is a property in ProfileRequest
        summary = Summary;      // Assuming Summary is a property in ProfileRequest
        desiredJobRole = DesiredJobRole;
    }

    public override IResult<Profile, Error> MapTo()
    {
        return QualifiedProfile.Create(FirstName, LastName, Summary, DesiredJobRole);
    }
}

public class CreateGeneralProfileCommand : CreateProfileCommand
{
    public string[] PersonalGoals { get; set; }

    public void Deconstruct(out string firstName, out string lastName, out string summary, out string[] personalGoals)
    {
        firstName = FirstName;  // Assuming FirstName is a property in ProfileRequest
        lastName = LastName;    // Assuming LastName is a property in ProfileRequest
        summary = Summary;      // Assuming Summary is a property in ProfileRequest
        personalGoals = PersonalGoals;
    }

    public override IResult<Profile, Error> MapTo()
    {
        return GeneralProfile.Create(FirstName, LastName, Summary, PersonalGoals);
    }
}

public class CreateStudentProfileCommand : CreateProfileCommand
{
    public string Major { get; set; }
    public string CareerGoals { get; set; }

    public void Deconstruct(out string firstName, out string lastName, out string summary, out string major, out string careerGoals)
    {
        firstName = FirstName;  // Assuming FirstName is a property in ProfileRequest
        lastName = LastName;    // Assuming LastName is a property in ProfileRequest
        summary = Summary;      // Assuming Summary is a property in ProfileRequest
        major = Major;
        careerGoals = CareerGoals;
    }

    public override IResult<Profile, Error> MapTo()
    {
        return StudentProfile.Create(FirstName, LastName, Summary, Major, CareerGoals);
    }
}
