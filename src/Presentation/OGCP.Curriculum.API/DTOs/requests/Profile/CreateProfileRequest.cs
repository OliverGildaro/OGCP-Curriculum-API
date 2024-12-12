using OGCP.Curriculum.API.DTOs;
using OGCP.Curriculum.API.DTOs.requests.Profile;
using OGCP.Curriculums.Core.DomainModel.profiles;
using OGCP.Curriculums.Core.DomainModel.valueObjects;

namespace OGCP.Curriculum.API.POCOS.requests.Profile;
public abstract class CreateProfileRequest
{
    public NameRequest Name { get; set; }
    public PhoneRequest Phone { get; set; }
    public string Email { get; set; }
    public string Summary { get; set; }
    public ProfileRequests RequestType { get; set; } // Discriminator field
}


public class CreateGeneralProfileRequest : CreateProfileRequest
{
    //public Profiletype Profiletype { get; set; }
    public string[] PersonalGoals { get; set; } = new string[] { };

    //public void Deconstruct(out NameRequest name, out string summary, out string[] personalGoals)
    //{
    //    name = Name;    // Assuming LastName is a property in ProfileRequest
    //    summary = Summary;      // Assuming Summary is a property in ProfileRequest
    //    personalGoals = PersonalGoals;
    //}
}

public class CreateQualifiedProfileRequest : CreateProfileRequest
{
    //public Profiletype Profiletype { get; set; }
    //public void Deconstruct(out NameRequest name, out string summary, out string desiredJobRole)
    //{
    //    name = Name;    // Assuming LastName is a property in ProfileRequest
    //    summary = Summary;      // Assuming Summary is a property in ProfileRequest
    //    desiredJobRole = DesiredJobRole;
    //}
    public string DesiredJobRole { get; set; }
}

public class CreateStudentProfileRequest : CreateProfileRequest
{
    //public Profiletype Profiletype { get; set; }
    public string Major { get; set; }
    public string CareerGoals { get; set; }

    //public void Deconstruct(out NameRequest name, out string summary, out string major, out string careerGoals)
    //{
    //    name = Name;    // Assuming LastName is a property in ProfileRequest
    //    summary = Summary;      // Assuming Summary is a property in ProfileRequest
    //    major = Major;
    //    careerGoals = CareerGoals;
    //}
}
