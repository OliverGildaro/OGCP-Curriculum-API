using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.models;

namespace OGCP.Curriculum.API.factories
{
    public class ProfileFactory
    {
        public Profile Get(ProfileRequest request)
        {
            if (request is CreateGeneralProfileRequest generalRequest)
            {
                (string firstName, string lastName, string summary, string[] personalGoals) = generalRequest;
                var result = GeneralProfile.Create(firstName, lastName, summary, personalGoals);
                return result.Value;
            }
            else if(request is CreateQualifiedProfileRequest qualifiedProfile)
            {
                (string firstName, string lastName, string summary, string desiredJobRole) = qualifiedProfile;
                var result = QualifiedProfile.Create(firstName, lastName, summary, desiredJobRole);
                return result.Value;
            } else if(request is CreateStudentProfileRequest studentProfile)
            {
                (string firstName, string lastName, string summary, string major, string careerGoals) = studentProfile;
                var result = StudentProfile.Create(firstName, lastName, summary, major, careerGoals);
                return result.Value;
            }

            return null;
        }
    }
}
