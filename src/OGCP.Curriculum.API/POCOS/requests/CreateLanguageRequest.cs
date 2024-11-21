using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.POCOS.requests
{
    public class CreateLanguageRequest
    {
        public Languages Name { get; set; }
        public ProficiencyLevel Level { get; set; }
    }

    public class EditLanguageRequest
    {
        public Languages Name { get; set; }
        public ProficiencyLevel Level { get; set; }
    }
}
