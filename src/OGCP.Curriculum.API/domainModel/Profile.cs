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
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Summary { get; set; }
    public bool IsPublic { get; set; }
    public string Visibility { get; set; }
    public string Status { get; set; }
    public PersonalInfo PersonalInfo { get; set; }
    public List<Language> LanguagesSpoken { get; set; } = new List<Language>();
    public ICollection<Skill> Skills { get; set; } = new List<Skill>();//?? IEnumerable
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
    public List<JobExperience> WorkExperience { get; set; } = new List<JobExperience>();

    internal void AddEducation(Education education)
    {
        this.Education.Add(education);
    }

    internal void AddJobExperience(JobExperience workExperience)
    {
        this.WorkExperience.Add(workExperience);
    }
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
    public StudentProfile()
    {
        
    }
    public StudentProfile(string firstName, string lastName, string summary, string major, string careerGoals)
        : base(firstName, lastName, summary)
    {
        this.CareerGoals = careerGoals;
        this.Major = major;
    }

    public string Major { get; set; }
    public List<InternshipExperience> Internships { get; set; } = new List<InternshipExperience>();
    public List<ResearchEducation> ResearchEducation { get; set; } = new List<ResearchEducation>();
    public string CareerGoals { get; set; }

    internal void AddEducation(ResearchEducation education)
    {
        this.ResearchEducation.Add(education);
    }
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
    public string Name { get; set; }//TODO enum to string ocnversion on FluentAPI
    public string Level { get; set; }//TODO enum to string ocnversion on FluentAPI
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
    public Education()
    {
        
    }
    protected Education(string institution, DateTime startDate, DateTime? endDate)
    {
        Institution = institution;
        StartDate = startDate;
        EndDate = endDate;
    }

    [Key]
    public int Id { get; set; }
    public string Institution { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class DegreeEducation : Education
{
    public DegreeEducation()
    {
        
    }
    public DegreeEducation(string institution, DegreeEnum degree, DateTime startDate, DateTime? endDate)
        : base(institution, startDate, endDate)
    {
        Degree = degree.ToString();
    }

    public string Degree { get; set; }//TODO enum to string ocnversion on FluentAPI
}

public class ResearchEducation : Education
{
    public ResearchEducation()
    {
        
    }
    public ResearchEducation(string institution, DateTime startDate, DateTime? endDate, string projectTitle, string supervisor, string summary)
        : base(institution, startDate, endDate)
    {
        ProjectTitle = projectTitle;
        Supervisor = supervisor;
        Summary = summary;
    }

    public string ProjectTitle { get; set; }
    public string Supervisor { get; set; }
    public string Summary { get; set; }
}

public class JobExperience
{
    public JobExperience()
    {
        
    }

    public JobExperience(string company, DateTime startDate, DateTime? endDate, string description)
    {
        Company = company;
        StartDate = startDate;
        EndDate = endDate;
        Description = description;
    }

    [Key]
    public int Id { get; set; }
    public string Company { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Description { get; set; }
}

public class WorkExperience : JobExperience
{
    private string position;

    public WorkExperience()
    {
    }

    public WorkExperience(string company, DateTime startDate, DateTime? endDate, string description, string position)
        :base(company, startDate, endDate, description)
    {
        this.position = position;
    }

    public string Position => position;
}

public class InternshipExperience : JobExperience
{
    public InternshipExperience()
    {
        
    }

    public InternshipExperience(string company, DateTime startDate, DateTime? endDate, string description, string role)
        :base(company, startDate, endDate, description)
    {
        Role = role;
    }

    public string Role { get; set; }
}
