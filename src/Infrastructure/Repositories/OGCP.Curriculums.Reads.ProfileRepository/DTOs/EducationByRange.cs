namespace OGCP.Curriculums.Reads.ProfileRepository.DTOs;
using System;
using System.Collections.Generic;

public class EducationDetailDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Institution { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string? Degree { get; set; }
    public string? ProjectTitle { get; set; }
}

public class InstitutionSummaryDto
{
    public string? Institution { get; set; }
    public int? ProfileCount { get; set; }
}

public class EducationByRangeResponse
{
    public List<EducationDetailDto> EducationDetails { get; set; }
    public int? TotalEducationsInRange { get; set; }
    public List<InstitutionSummaryDto> InstitutionSummary { get; set; }
}
