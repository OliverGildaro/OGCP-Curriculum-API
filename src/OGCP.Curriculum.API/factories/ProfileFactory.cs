using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.models;

namespace OGCP.Curriculum.API.factories
{
    public class ProfileFactory
    {
        public Profile CreateGeneral(CreateGeneralProfileRequest request)
        {
            return new GeneralProfile
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Summary = request.Summary,
                IsPublic = request.IsPublic,
                Profiletype = request.Profiletype,
            };
        }

        public Profile CreateQualified(CreateQualifiedProfileRequest request)
        {
            return new QualifiedProfile
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Summary = request.Summary,
                IsPublic = request.IsPublic,
                Profiletype = request.Profiletype,
                DesiredJobRole = request.DesiredJobRole,
            };
        }

        public Profile CreateStudent(CreateStudentProfileRequest request    )
        {
            return new StudentProfile
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Summary = request.Summary,
                IsPublic = request.IsPublic,
                Profiletype = request.Profiletype,
                CareerGoals = request.CareerGoals,
                Major = request.Major,
            };
        }
    }
}
