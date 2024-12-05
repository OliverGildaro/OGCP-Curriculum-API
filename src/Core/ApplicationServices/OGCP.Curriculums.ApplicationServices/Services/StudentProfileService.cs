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

    public async Task<Result> AddEducationAsync(int id, ResearchEducation education)
    {
        Maybe<StudentProfile> profile = await this.repository.FindAsync(id);
        if (profile.HasNoValue)
        {
            return Result.Failure("");

        }

        profile.Value.AddEducation(education);

        var result = await this.repository.SaveChangesAsync();
        return Result.Success();
    }

    public Task<Result> Create(StudentProfile request)
    {
        return Task.FromResult(Result.Success());
    }

    public Task<Maybe<ResearchEducation>> FindResearchEducation(string institution, string projectTitle)
    {
        return this.repository.FindResearchEducation(institution, projectTitle);
    }

    public Task<IReadOnlyList<StudentProfile>> GetAsync()
    {
        return null;
    }


    public Task<StudentProfile> GetAsync(int id)
    {
        return null;
        //return this.repository.Find(id);
    }

    public async Task<Result> RemoveEducationAsync(int profileId, int educationId)
    {
        const string removeEducation = "EXEC DeleteOrphanedEducations;";
        //TODO: We need to check if this is not resulting in a product cartesian explosion
        //We may need to use .AsSplitQuery() in the repository
        Maybe<StudentProfile> profile = await this.repository.FindAsync(profileId);

        Result result = profile.Value.RemoveEducation(educationId);

        if (result.IsFailure)
        {
            return result;
        }
        await this.repository.SaveChangesAsync();

        var isPersisted = await this.repository.RemoveOrphanEducationsAsync(removeEducation);

        if (isPersisted < 1)
        {
            return Result.Failure("");
        }

        return Result.Success();
    }

    public async Task<Result> UpdateEducationAsync(int profileId, int educationId, ResearchEducation education)
    {
        Maybe<StudentProfile> profile = await this.repository.FindAsync(profileId);
        if (profile.HasNoValue)
        {
            return Result.Failure("");
        }

        var result = profile.Value.UpdateEducation(educationId, education);

        if (result.IsFailure)
        { 
            return result;
        }

        await this.repository.SaveChangesAsync();
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
