using ArtForAll.Shared.ErrorHandler;
using ArtForAll.Shared.ErrorHandler.Maybe;
using OGCP.Curriculum.API.DAL.Mutations.Interfaces;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.services.interfaces;
using System.Linq.Expressions;

namespace OGCP.Curriculum.API.services;

public class StudentProfileService : IStudentProfileService
{
    private readonly IStudentProfileWriteRepo repository;

    public StudentProfileService(IStudentProfileWriteRepo repository)
    {
        this.repository = repository;
    }

    public async Task<Result> AddEducation(int id, ResearchEducation education)
    {
        Maybe<StudentProfile> profile = await this.repository.Find(id);
        if (profile.HasNoValue)
        {
            return Result.Failure("");

        }

        profile.Value.AddEducation(education);

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
        return null;
    }


    public Task<StudentProfile> Get(int id)
    {
        return null;
        //return this.repository.Find(id);
    }

    public async Task<Result> RemoveEducation(int profileId, int educationId)
    {
        const string removeEducation = "EXEC DeleteOrphanedEducations;";
        //TODO: We need to check if this is not resulting in a product cartesian explosion
        //We may need to use .AsSplitQuery() in the repository
        Maybe<StudentProfile> profile = await this.repository.Find(profileId);

        Result result = profile.Value.RemoveEducation(educationId);

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
