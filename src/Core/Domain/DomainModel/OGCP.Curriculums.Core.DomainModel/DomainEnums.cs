namespace OGCP.Curriculum.API.domainmodel;

public enum ProfileDetailLevel
{
    Minimal = 1,
    Partial,
    Complete
}

public enum EducationLevel
{
    HighSchool = 1,
    Diploma,
    Certificate,
    Associate,
    Bachelor,
    Master,
    Doctorate
}

public enum ProficiencyLevel
{
    Beginner = 1,
    Intermediate = 2,
    Proficient = 3,
    Advanced = 4,
    Expert = 5
}

public enum Languages
{
    Spanish = 1,
    English = 2,
    Korean = 3,
    Italian = 4,
    Portuguese = 5
}

public enum WorkExperienceCategory
{
    Employment = 1,
    Internship = 2
}



public enum CategorySkill
{
    Programming = 1,
    WebFrameworks,
    Databases,
    Cloud,
    TestingTechniques,
    ProjectManagement,
    AutomationTools,
    AutomationBuildingTools,
    VesionControl,
    Others
}

public enum ProfessionTypes
{
    Engineering,
    Medicine
}
