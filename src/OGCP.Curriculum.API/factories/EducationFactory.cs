using OGCP.Curriculum.API.commanding.commands.AddEducationDegree;
using OGCP.Curriculum.API.commanding.commands.AddEducationResearch;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.POCOS.requests;

namespace OGCP.Curriculum.API.factories;

public class EducationFactory
{
    public static AddEducationToProfileCommand Get(AddEducationRequest request, int id)
    {
        if (request.EducationType.Equals(EducationTypes.AddEducationDegreeRequest) 
            && request is AddDegreeEducationRequest degreeEduc)
        {
            var result = new AddEducationDegreeToProfileCommand
            {
                Id = id,
                Degree = degreeEduc.Degree,
                EndDate = degreeEduc.EndDate,
                StartDate = degreeEduc.StartDate,
                Institution = degreeEduc.Institution
            };
            return result;
        }
        else if(request.EducationType.Equals(EducationTypes.AddEducationResearchRequest) &&
            request is AddResearchEducationRequest researchEduc)
        {
            return new AddEducationResearchToProfileCommand
            {
                Id = id,
                EndDate = researchEduc.EndDate,
                StartDate = researchEduc.StartDate,
                Institution = researchEduc.Institution,
                ProjectTitle = researchEduc.ProjectTitle,
                Summary = researchEduc.Summary,
                Supervisor = researchEduc.Supervisor
            };
        }
        else if(request.EducationType.Equals(EducationTypes.AddEducationToStudentProfileRequest)
            && request is AddEducationToStudentProfileRequest researchStudentEduc)
        {
            return new AddEducationToStudentProfileCommand
            {
                Id = id,
                EndDate = researchStudentEduc.EndDate,
                StartDate = researchStudentEduc.StartDate,
                Institution = researchStudentEduc.Institution,
                ProjectTitle = researchStudentEduc.ProjectTitle,
                Summary = researchStudentEduc.Summary,
                Supervisor = researchStudentEduc.Supervisor
            };
        }

        return null;
    }
}
