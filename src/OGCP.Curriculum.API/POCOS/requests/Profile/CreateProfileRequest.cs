using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.POCOS.requests.Profile;
public abstract class ProfileRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Summary { get; set; }
    public ProfileTypes RequestType { get; set; } // Discriminator field
}


public class CreateGeneralProfileRequest : ProfileRequest
{
    //public Profiletype Profiletype { get; set; }
    public string[] PersonalGoals { get; set; } = new string[] { };

    public void Deconstruct(out string firstName, out string lastName, out string summary, out string[] personalGoals)
    {
        firstName = FirstName;  // Assuming FirstName is a property in ProfileRequest
        lastName = LastName;    // Assuming LastName is a property in ProfileRequest
        summary = Summary;      // Assuming Summary is a property in ProfileRequest
        personalGoals = PersonalGoals;
    }
}

public class CreateQualifiedProfileRequest : ProfileRequest
{
    //public Profiletype Profiletype { get; set; }
    public void Deconstruct(out string firstName, out string lastName, out string summary, out string desiredJobRole)
    {
        firstName = FirstName;  // Assuming FirstName is a property in ProfileRequest
        lastName = LastName;    // Assuming LastName is a property in ProfileRequest
        summary = Summary;      // Assuming Summary is a property in ProfileRequest
        desiredJobRole = DesiredJobRole;
    }
    public string DesiredJobRole { get; set; }
}

public class CreateStudentProfileRequest : ProfileRequest
{
    //public Profiletype Profiletype { get; set; }
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
}
