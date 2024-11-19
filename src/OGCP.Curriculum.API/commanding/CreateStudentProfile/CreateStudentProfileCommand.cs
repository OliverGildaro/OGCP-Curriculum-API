using ArtForAll.Shared.Contracts.CQRS;

namespace OGCP.Curriculum.API.commanding.CreateStudentProfile;

public class CreateStudentProfileCommand : ICommand
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Summary { get; set; }
    public string Major { get; set; }
    public string CareerGoals { get; set; }

    public void Deconstruct(out string firstName, out string lastName, out string summary, out string major, out string careerGoals)
    {
        firstName = this.FirstName;  // Assuming FirstName is a property in ProfileRequest
        lastName = this.LastName;    // Assuming LastName is a property in ProfileRequest
        summary = this.Summary;      // Assuming Summary is a property in ProfileRequest
        major = this.Major;
        careerGoals = this.CareerGoals;
    }
}
