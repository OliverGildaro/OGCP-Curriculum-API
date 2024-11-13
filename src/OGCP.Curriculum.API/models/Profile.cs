using OGCP.Curriculum.API.dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OGCP.Curriculum.API.models;

public interface IEntity<TEntityId>
{
    public TEntityId Id { get; set; }
}

public class Profile : IEntity<int>
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Summary { get; set; }
    //public Profiletype Profiletype { get; set; }
    public bool IsPublic { get; set; }//Public or private
    public string Visibility { get; set; }//Public and friends only
    public string Status { get; set; }//progress of profile completion
    public PersonalInfo PersonalInfo { get; set; }
    public List<Language> LanguagesSpoken { get; set; } = new List<Language>();
    public List<Skill> Skills { get; set; } = new List<Skill>();
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Profile()
    {
        
    }

    public Profile(string firstName, string lastName, string summary)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Summary = summary;
        this.IsPublic = true;
        this.Visibility = string.Empty;
        this.Status = string.Empty;
        this.CreatedAt = DateTime.UtcNow;
        this.UpdatedAt = DateTime.UtcNow;
    }

    internal bool AddLanguage(Language language)
    {
        if (this.LanguagesSpoken is null)
        {
            this.LanguagesSpoken = new List<Language>();
        }
        this.LanguagesSpoken.Add(language);
        return true;
    }
}

public class QualifiedProfile : Profile
{
    public QualifiedProfile()
    {
        
    }
    public QualifiedProfile(string firstName, string lastName, string summary, string desiredJobRole)
        : base(firstName, lastName, summary)
    {
        this.DesiredJobRole = desiredJobRole;
    }

    public string DesiredJobRole { get; set; }
    public List<Education> Education { get; set; } = new List<Education>();
    public List<WorkExperience> WorkExperience { get; set; } = new List<WorkExperience>();
}

public class GeneralProfile : Profile
{
    public GeneralProfile()
    {
        
    }
    public GeneralProfile(string firstName, string lastName, string summary, string[] personalGoals) 
        : base(firstName, lastName, summary)
    {
        this.PersonalGoals = personalGoals;
    }

    public string[] PersonalGoals { get; set; } = new string[] {};
    public List<WorkExperience> WorkExperience { get; set; } = new List<WorkExperience>();
}

public class StudentProfile : Profile
{
    public StudentProfile(string firstName, string lastName, string summary, string major, string careerGoals)
        : base(firstName, lastName, summary)
    {
        this.CareerGoals = careerGoals;
        this.Major = major;
    }

    public string Major { get; set; }
    public List<Internship> Internships { get; set; } = new List<Internship>();
    public List<ExtracurricularActivity> ExtraActivities { get; set; } = new List<ExtracurricularActivity>();
    public List<ResearchExperience> ResearchExperiences { get; set; } = new List<ResearchExperience>();
    public string CareerGoals { get; set; }
}


public class Language
{
    public Language()
    {
        
    }
    public Language(LanguageEnum name, LevelEnum level)
    {
        this.Name = name.ToString();
        this.Level = level.ToString();
    }
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Level { get; set; }
}

public class PersonalInfo
{
    [Key]  // Use ProfileId as both FK and PK
    public int Id { get; set; }
    public int? ProfileId { get; set; } // Optional foreign key property
    [ForeignKey(nameof(ProfileId))]
    public Profile Profile { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}
public class Skill
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Level { get; set; } // Ejemplo: Básico, Intermedio, Avanzado
}

public class Education
{
    [Key]
    public int Id { get; set; }
    public string Institution { get; set; }
    public string Degree { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class ExtracurricularActivity
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Description { get; set; }
}

public class ResearchExperience
{
    [Key]
    public int Id { get; set; }
    public string Projecttitle { get; set; }
    public string Supervisor { get; set; }
    public string Summary { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class JobExperience
{
    [Key]
    public int Id { get; set; }
    public string Company { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Description { get; set; }
}

public class WorkExperience : JobExperience
{
    public string Position { get; set; }
}

public class Internship : JobExperience
{
    public string Role { get; set; }
}
