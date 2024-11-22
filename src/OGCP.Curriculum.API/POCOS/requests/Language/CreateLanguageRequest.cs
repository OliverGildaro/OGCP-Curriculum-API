using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.POCOS.requests.Language
{
    public class CreateLanguageRequest
    {
        public Languages Name { get; set; }
        public ProficiencyLevel Level { get; set; }
    }
}
