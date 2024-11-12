using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.factories.interfaces;
using OGCP.Curriculum.API.models;

namespace OGCP.Curriculum.API.factories
{
    public class GeneralProfileFactory : IProfileFactory
    {
        public Profile Get(ProfileRequest request)
        {
            if (request is CreateGeneralProfileRequest generalRequest)
            {
                (string firstName, string lastName, string summary, string[] personalGoals) = generalRequest;
                return new GeneralProfile(firstName, lastName, summary, personalGoals);
            } else if(request is CreateQualifiedProfileRequest qualifiedProfile)
            {
                (string firstName, string lastName, string summary, string desiredJobRole) = qualifiedProfile;
                return new QualifiedProfile(firstName, lastName, summary, desiredJobRole);
            } else if(request is CreateStudentProfileRequest studentProfile)
            {
                (string firstName, string lastName, string summary, string major, string careerGoals) = studentProfile;
                return new StudentProfile(firstName, lastName, summary, major, careerGoals);
            }

            //if (request is CreateQualifiedProfileRequest qualifiedRequest)
            //{
            //    return new QualifiedProfile
            //    {
            //        FirstName = qualifiedRequest.FirstName,
            //        LastName = qualifiedRequest.LastName,
            //        Summary = qualifiedRequest.Summary,
            //        Profiletype = qualifiedRequest.Profiletype,
            //        IsPublic = qualifiedRequest.IsPublic,
            //        DesiredJobRole = qualifiedRequest.DesiredJobRole
            //    };
            //}
            //if (request is CreateStudentProfileRequest studentRequest)
            //{
            //    return new QualifiedProfile
            //    {
            //        FirstName = studentRequest.FirstName,
            //        LastName = studentRequest.LastName,
            //        Summary = studentRequest.Summary,
            //        Profiletype = studentRequest.Profiletype,
            //        IsPublic = studentRequest.IsPublic,
            //    };
            //}

            return null;
        }
    }



}
