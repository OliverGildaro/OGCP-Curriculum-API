using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.DTOs;

namespace OGCP.Curriculum.API.POCOS.requests.Education
{
    public abstract class AddEducationRequest
    {
        public string Institution { get; set; }
        public EducationRequests EducationType { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
    }
    public class AddDegreeEducationRequest : AddEducationRequest
    {
        public EducationLevel Degree { get; set; }

        public void Deconstruct(out string institution, out EducationLevel degree, out DateOnly startDate, out DateOnly? endDate)
        {
            institution = Institution;
            degree = Degree;
            startDate = StartDate;
            endDate = EndDate;
        }
    }
    public class AddResearchEducationRequest : AddEducationRequest
    {
        public string ProjectTitle { get; set; }
        public string Supervisor { get; set; }
        public string Summary { get; set; }
        public void Deconstruct(
            out string institution,
            out DateOnly startDate,
            out DateOnly? endDate,
            out string projectTitle,
            out string supervisor,
            out string summary)
        {
            institution = Institution;
            startDate = StartDate;
            endDate = EndDate;
            projectTitle = ProjectTitle;
            supervisor = Supervisor;
            summary = Summary;
        }
    }

    public class AddResearchEducationToStudentProfileRequest : AddResearchEducationRequest
    {
    }
}
