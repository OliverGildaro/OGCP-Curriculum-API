﻿using OGCP.Curriculums.Reads.ProfileRepository.Models;

namespace OGCP.Curriculum.API.DAL.Queries.Models;

public class ProfileReadModel
{
    public int Id { get; set; }
    public string GivenName { get; set; }
    public string FamilyNames { get; set; }
    public string Summary { get; set; }
    public string DesiredJobRole { get; set; }
    public string[]? PersonalGoals { get; set; } = new string[] { };
    public string? Major { get; set; }
    public string? CareerGoals { get; set; }
    public string? Discriminator { get; set; }
    public IReadOnlyList<ProfileLanguageReadModel> ProfileLanguages { get; } = new List<ProfileLanguageReadModel>();
    public IReadOnlyList<ProfileEducationReadModel> ProfileEducations { get; } = new List<ProfileEducationReadModel>();
}
