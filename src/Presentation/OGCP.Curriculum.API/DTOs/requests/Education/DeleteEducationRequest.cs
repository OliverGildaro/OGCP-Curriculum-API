using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.POCOS.requests.Education;

namespace OGCP.Curriculum.API.DTOs.requests.Education
{
    public class DeleteEducationRequest
    {
        public EducationRequests EducationType { get; set; }
    }
    public class DeleteQualifiedEducationRequest : DeleteEducationRequest
    {
    }

    public class DeleteStudentEducationRequest : DeleteEducationRequest
    {
    }
}
