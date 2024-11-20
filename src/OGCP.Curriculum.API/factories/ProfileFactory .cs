using ArtForAll.Shared.Contracts.CQRS;
using OGCP.Curriculum.API.commanding.CreateGeneralProfile;
using OGCP.Curriculum.API.commanding.CreateQualifiedProfile;
using OGCP.Curriculum.API.commanding.CreateStudentProfile;
using OGCP.Curriculum.API.dtos;

namespace OGCP.Curriculum.API.factories;

public class ProfileFactory
{
    public static ICommand Get(ProfileRequest request)
    {
        if (request is CreateGeneralProfileRequest generalRequest)
        {
            (string firstName, string lastName, string summary, string[] personalGoals) = generalRequest;
            var result = new CreateGeneralProfileCommand
            {
                FirstName = firstName,
                LastName = lastName,
                Summary = summary,
                PersonalGoals = personalGoals
            };
            return result;
        }
        else if(request is CreateQualifiedProfileRequest qualifiedProfile)
        {
            (string firstName, string lastName, string summary, string desiredJobRole) = qualifiedProfile;
            return new CreateQualifiedProfileCommand
            {
                FirstName = firstName,
                LastName = lastName,
                Summary = summary,
                DesiredJobRole = desiredJobRole,
            };
        } else if(request is CreateStudentProfileRequest studentProfile)
        {
            (string firstName, string lastName, string summary, string major, string careerGoals) = studentProfile;
            return new CreateStudentProfileCommand
            {
                FirstName = firstName,
                LastName = lastName,
                Summary = summary,
                Major = major,
                CareerGoals = careerGoals,
            };
        }

        return null;
    }
}
