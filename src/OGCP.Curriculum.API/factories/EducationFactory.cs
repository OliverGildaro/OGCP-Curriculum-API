using ArtForAll.Shared.Contracts.CQRS;
using OGCP.Curriculum.API.commanding.commands.AddEducationDegree;
//using OGCP.Curriculum.API.commanding.commands.AddEducationResearch;
using OGCP.Curriculum.API.commanding.commands.CreateGeneralProfile;
using OGCP.Curriculum.API.commanding.commands.CreateQualifiedProfile;
using OGCP.Curriculum.API.commanding.commands.CreateStudentProfile;
using OGCP.Curriculum.API.POCOS.requests;

namespace OGCP.Curriculum.API.factories;

public class EducationFactory
{
    public static AddEducationToProfileCommand Get(AddEducationRequest request, int id)
    {
        if (request is AddDegreeEducationRequest generalRequest)
        {
            var result = new AddEducationDegreeToProfileCommand
            {
                Id = id,
                Degree = generalRequest.Degree,
                EndDate = generalRequest.EndDate,
                StartDate = generalRequest.StartDate,
                Institution = generalRequest.Institution
            };
            return result;
        }
        else if(request is AddResearchEducationRequest qualifiedProfile)
        {
            return new AddEducationResearchToProfileCommand
            {
                Id = id,
                EndDate = qualifiedProfile.EndDate,
                StartDate = qualifiedProfile.StartDate,
                Institution = qualifiedProfile.Institution,
                ProjectTitle = qualifiedProfile.ProjectTitle,
                Summary = qualifiedProfile.Summary,
                Supervisor = qualifiedProfile.Supervisor
            };
        }

        return null;
    }
}
