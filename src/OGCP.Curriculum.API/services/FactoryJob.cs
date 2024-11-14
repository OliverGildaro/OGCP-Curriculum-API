using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.dtos.requests;
using OGCP.Curriculum.API.models;

namespace OGCP.Curriculum.API.services
{
    internal class FactoryJob
    {
        internal static JobExperience Get<T>(T request)
        {
            if (request is CreateWorkExperienceRequest workExp)
            {
                var (company, startDate, endDate, description, position) = workExp;
                return new WorkExperience(company, startDate, endDate, description, position);
            }else if(request is CreateInternshipExperienceRequest internExp)
            {
                var (company, startDate, endDate, description, role) = internExp;
                return new InternshipExperience(company, startDate, endDate, description, role);
            }

            return null;
        }
    }
}