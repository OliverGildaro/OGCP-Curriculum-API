
using ArtForAll.Shared.Contracts.CQRS;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.commanding.commands.AddEducationDegree
{
    public class AddEducationDegreeToProfileCommand : ICommand
    {
        public int Id { get; set; }
        public string Institution { get; set; }
        public EducationLevel Degree { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public void Deconstruct(
            out int id,
            out string institution,
            out EducationLevel degree,
            out DateTime startDate,
            out DateTime? endDate)
        {
            id = this.Id;
            institution = this.Institution;
            degree = this.Degree;
            startDate = this.StartDate;
            endDate = this.EndDate;
        }
    }
}
