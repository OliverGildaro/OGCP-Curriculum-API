using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.Contracts.DDD;
using ArtForAll.Shared.ErrorHandler.Results;
using OGCP.Curriculum.API.domainmodel;
namespace OGCP.Curriculum.API.Commanding.commands.UpdateProfile;

public abstract class UpdateProfileCommand : ICommand
{
    public int  Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Summary { get; set; }
    public abstract IResult<Profile, Error> MapTo();
}

public class UpdateQualifiedProfileCommand : UpdateProfileCommand
{
    public string DesiredJobRole { get; set; }

    public void Deconstruct(out int id, out string firstName, out string lastName, out string summary, out string desiredJobRole)
    {
        id = Id;
        firstName = FirstName;  // Assuming FirstName is a property in ProfileRequest
        lastName = LastName;    // Assuming LastName is a property in ProfileRequest
        summary = Summary;      // Assuming Summary is a property in ProfileRequest
        desiredJobRole = DesiredJobRole;
    }

    public override IResult<Profile, Error> MapTo()
    {
        return QualifiedProfile.Hidrate(Id, FirstName, LastName, Summary, DesiredJobRole);
    }
}

public class UpdateGeneralProfileCommand : UpdateProfileCommand
{
    public string[] PersonalGoals { get; set; }

    public void Deconstruct(out int id, out string firstName, out string lastName, out string summary, out string[] personalGoals)
    {
        id = Id;
        firstName = FirstName;  // Assuming FirstName is a property in ProfileRequest
        lastName = LastName;    // Assuming LastName is a property in ProfileRequest
        summary = Summary;      // Assuming Summary is a property in ProfileRequest
        personalGoals = PersonalGoals;
    }

    public override IResult<Profile, Error> MapTo()
    {
        return GeneralProfile.Hidrate(Id, FirstName, LastName, Summary, PersonalGoals);
    }
}

public class UpdateStudentProfileCommand : UpdateProfileCommand
{
    public string Major { get; set; }
    public string CareerGoals { get; set; }

    public void Deconstruct(out int id, out string firstName, out string lastName, out string summary, out string major, out string careerGoals)
    {
        id = Id;
        firstName = FirstName;  // Assuming FirstName is a property in ProfileRequest
        lastName = LastName;    // Assuming LastName is a property in ProfileRequest
        summary = Summary;      // Assuming Summary is a property in ProfileRequest
        major = Major;
        careerGoals = CareerGoals;
    }

    public override IResult<Profile, Error> MapTo()
    {
        return StudentProfile.Hidrate(Id, FirstName, LastName, Summary, Major, CareerGoals);
    }
}
