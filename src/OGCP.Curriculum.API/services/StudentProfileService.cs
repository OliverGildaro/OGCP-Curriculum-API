using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.repositories.interfaces;
using OGCP.Curriculum.API.services.interfaces;
using System.Linq.Expressions;

namespace OGCP.Curriculum.API.services;

public class StudentProfileService : IStudentProfileService
{
    private readonly IStudentProfileRepository repository;

    public StudentProfileService(IStudentProfileRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result> AddEducation(int id, ResearchEducation education)
    {
        StudentProfile profile = await this.repository.Find(id, GetQueryExpression());
        if (profile is null)
        {
            return Result.Failure("");

        }

        profile.AddEducation(education);

        var result = await this.repository.SaveChanges();
        return Result.Success();
    }

    public Task<int> Create(StudentProfile request)
    {
        this.repository.Add(request);
        return this.repository.SaveChanges();
    }

    public Task<IReadOnlyList<StudentProfile>> Get()
    {
        return this.repository.Find();
    }

    public Task<StudentProfile> Get(int id)
    {
        return this.repository.Find(id);
    }

    public async Task<Result> RemoveEducation(int profileId, int educationId)
    {
        const string removeEducation = "EXEC DeleteOrphanedEducations;";
        StudentProfile profile = await this.repository.Find(profileId, this.GetQueryExpression());

        Result result = profile.RemoveEducation(educationId);

        if (result.IsFailure)
        {
            return result;
        }
        await this.repository.SaveChanges();

        var isPersisted = await this.repository.RemoveOrphanEducations(removeEducation);

        if (isPersisted < 1)
        {
            return Result.Failure("");
        }

        return Result.Success();
    }

    private Expression<Func<StudentProfile, object>>[] GetQueryExpression()
    {
        return
        [
            x => x.Educations,
            x => x.Experiences,
            x => x.LanguagesSpoken,
            x => x.PersonalInfo,
        ];
    }
}
