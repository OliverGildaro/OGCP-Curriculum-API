using OGCP.Curriculums.Core.DomainModel;

namespace OGCP.Curriculum.API.Filters.Envelopes
{
    public class KeyValueError
    {
        public string FieldName { get; set; }
        public Error Error{ get; set; }
    }
}
