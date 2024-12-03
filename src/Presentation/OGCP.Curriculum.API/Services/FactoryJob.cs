using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.POCOS.requests.work;

namespace OGCP.Curriculum.API.services
{
    public class FactoryJob
    {
        public static JobExperience Get<T>(T request)
        {
            if (request is CreateWorkExperienceRequest workExp)
            {
                var (company, startDate, endDate, description, position) = workExp;
                return WorkExperience.Create(company, startDate, endDate, description, position).Value;
            }else if(request is CreateInternshipExperienceRequest internExp)
            {
                var (company, startDate, endDate, description, role) = internExp;
                return InternshipExperience.Create(company, startDate, endDate, description, role).Value;
            }

            return null;
        }
    }
}