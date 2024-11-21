using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.POCOS.requests
{
    public abstract class AddEducationRequest
    {
        public string Institution { get; set; }
        public EducationTypes EducationType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
    public class AddDegreeEducationRequest : AddEducationRequest
    {
        public EducationLevel Degree { get; set; }

        public void Deconstruct(out string institution, out EducationLevel degree, out DateTime startDate, out DateTime? endDate)
        {
            institution = base.Institution;
            degree = this.Degree;
            startDate = base.StartDate;
            endDate = base.EndDate;
        }
    }
    public class AddResearchEducationRequest : AddEducationRequest
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
            institution = this.Institution;
            startDate = this.StartDate;
            endDate = this.EndDate;
            projectTitle = this.ProjectTitle;
            supervisor = this.Supervisor;
            summary = this.Summary;
        }
    }
}
