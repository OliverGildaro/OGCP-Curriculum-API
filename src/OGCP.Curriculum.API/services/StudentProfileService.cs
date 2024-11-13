using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.factories.interfaces;
using OGCP.Curriculum.API.models;
using OGCP.Curriculum.API.repositories.interfaces;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.services;

public class StudentProfileService : IStudentProfileService
{
    private readonly IStudentProfileRepository repository;

    public StudentProfileService(IStudentProfileRepository repository, IProfileFactory factory)
    {
        this.repository = repository;
    }

    public void AddEducation(int id, CreateEducationRequest request)
    {
        StudentProfile profile = this.repository.Find(id);
        if (profile is null)
        {
            return;
        }

        (string institution, DegreeEnum degree, DateTime startDate, DateTime? endDate ) = request;
        Education education = new Education(institution, degree, startDate, endDate);

        profile.AddEducation(education);

        this.repository.SaveChanges();
    }

    public void AddLanguage(int id, CreateLanguageRequest languageRequest)
    {
        StudentProfile profile = this.repository.Find(id);

        Language language = new Language(languageRequest.Name, languageRequest.Level);
        bool result = profile.AddLanguage(language);

        repository.SaveChanges();
    }

    public void Create(ProfileRequest request)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<StudentProfile> Get()
    {
        throw new NotImplementedException();
    }

    public StudentProfile Get(int id)
    {
        throw new NotImplementedException();
    }
}
