using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.DTOs;

namespace OGCP.Curriculum.API.POCOS.requests.Education
{
    public class UpdateEducationRequest
    {
        public string Institution { get; set; }
        public EducationRequests EducationType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class UpdateDegreeEducationRequest : UpdateEducationRequest
    {
        public EducationLevel Degree { get; set; }

        public void Deconstruct(out string institution, out EducationLevel degree, out DateTime startDate, out DateTime? endDate)
        {
            institution = Institution;
            degree = Degree;
            startDate = StartDate;
            endDate = EndDate;
        }
    }
    public class UpdateResearchEducationRequest : UpdateEducationRequest
    {
        public string ProjectTitle { get; set; }
        public string Supervisor { get; set; }
        public string Summary { get; set; }
        public void Deconstruct(
            out string institution,
            out DateTime startDate,
            out DateTime? endDate,
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

    public class UpdateEducationToStudentProfileRequest : UpdateResearchEducationRequest
    {
    }
}
