using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.dtos.requests;

namespace OGCP.Curriculum.API.services
{
    internal class FactoryJob
    {
        internal static JobExperience Get<T>(T request)
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