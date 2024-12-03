using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.dtos;



public class PersonalInfoRequest
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string Phone { get; init; }
    public string Summary { get; init; }
}

public class SkillRequest
{
    public string Name { get; init; }
    public string Level { get; init; }
}

public class WorkExperienceRequest
{
    public string Company { get; init; }
    public string Position { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public string Description { get; init; }
}


