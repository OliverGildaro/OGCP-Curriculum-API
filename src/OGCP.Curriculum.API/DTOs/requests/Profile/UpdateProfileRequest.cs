namespace OGCP.Curriculum.API.DTOs.requests.Profile;

public abstract class UpdateProfileRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Summary { get; set; }
    public ProfileRequests RequestType { get; set; } // Discriminator field
}


public class UpdateGeneralProfileRequest : UpdateProfileRequest
{
    public string[] PersonalGoals { get; set; } = new string[] { };
}

public class UpdateQualifiedProfileRequest : UpdateProfileRequest
{
    public string DesiredJobRole { get; set; }
}

public class UpdateStudentProfileRequest : UpdateProfileRequest
{
    public string Major { get; set; }
    public string CareerGoals { get; set; }
}
