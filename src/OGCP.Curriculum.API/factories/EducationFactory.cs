using OGCP.Curriculum.API.commanding.commands.AddEducationDegree;
using OGCP.Curriculum.API.commanding.commands.AddEducationResearch;
using OGCP.Curriculum.API.commanding.commands.UpdateEducationToQualifiedProfile;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.DTOs;
using OGCP.Curriculum.API.POCOS.requests.Education;

namespace OGCP.Curriculum.API.factories;

public class EducationFactory
{
    public static AddEducationToQualifiedProfileCommand Get(AddEducationRequest request, int id)
    {
        if (request.EducationType.Equals(EducationRequests.AddDegree) 
            && request is AddDegreeEducationRequest degreeEduc)
        {
            var result = new AddDegreeEducationToQualifiedProfileCommand
            {
                ProfileId = id,
                Degree = degreeEduc.Degree,
                EndDate = degreeEduc.EndDate,
                StartDate = degreeEduc.StartDate,
                Institution = degreeEduc.Institution
            };
            return result;
        }
        else if(request.EducationType.Equals(EducationRequests.AddResearch) &&
            request is AddResearchEducationRequest researchEduc)
        {
            return new AddResearchEducationToQualifiedProfileCommand
            {
                ProfileId = id,
                EndDate = researchEduc.EndDate,
                StartDate = researchEduc.StartDate,
                Institution = researchEduc.Institution,
                ProjectTitle = researchEduc.ProjectTitle,
                Summary = researchEduc.Summary,
                Supervisor = researchEduc.Supervisor
            };
        }
        else if(request.EducationType.Equals(EducationRequests.AddResearchToStudent)
            && request is AddResearchEducationToStudentProfileRequest researchStudentEduc)
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

    //public UpdateEducationFromProfileCommand GetUpdateEducationCommand(UpdateEducationRequest request, int profileId, int educationId)
    //{
    //    if (request.EducationType.Equals(EducationTypes.UpdateEducationDegreeRequest)
    //        && request is UpdateEd degreeEduc)
    //    {
    //        var result = new AddEducationDegreeToProfileCommand
    //        {
    //            Id = id,
    //            Degree = degreeEduc.Degree,
    //            EndDate = degreeEduc.EndDate,
    //            StartDate = degreeEduc.StartDate,
    //            Institution = degreeEduc.Institution
    //        };
    //        return result;
    //    }
    //    else if (request.EducationType.Equals(EducationTypes.AddEducationResearchRequest) &&
    //        request is AddResearchEducationRequest researchEduc)
    //    {
    //        return new AddEducationResearchToProfileCommand
    //        {
    //            Id = id,
    //            EndDate = researchEduc.EndDate,
    //            StartDate = researchEduc.StartDate,
    //            Institution = researchEduc.Institution,
    //            ProjectTitle = researchEduc.ProjectTitle,
    //            Summary = researchEduc.Summary,
    //            Supervisor = researchEduc.Supervisor
    //        };
    //    }
    //    else if (request.EducationType.Equals(EducationTypes.AddEducationToStudentProfileRequest)
    //        && request is AddEducationToStudentProfileRequest researchStudentEduc)
    //    {
    //        return new AddEducationToStudentProfileCommand
    //        {
    //            Id = id,
    //            EndDate = researchStudentEduc.EndDate,
    //            StartDate = researchStudentEduc.StartDate,
    //            Institution = researchStudentEduc.Institution,
    //            ProjectTitle = researchStudentEduc.ProjectTitle,
    //            Summary = researchStudentEduc.Summary,
    //            Supervisor = researchStudentEduc.Supervisor
    //        };
    //    }

    //    return null;
    //}
}
