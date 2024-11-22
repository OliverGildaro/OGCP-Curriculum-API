namespace OGCP.Curriculum.API.DTOs;
public enum ProfileRequests
{
    CreateGeneral = 1,
    CreateQualified,
    CreateStudent
}

public enum EducationRequests
{
    AddDegree = 1,
    AddResearch = 2,
    AddResearchToStudent = 3,
    UpdateDegree = 4,
    UpdateResearch = 5,
    UpdateResearchFromStudent = 6,
}
