using ArtForAll.Shared.ErrorHandler;
using ArtForAll.Shared.ErrorHandler.Maybe;
using OGCP.Curriculum.API.DAL.Mutations.Interfaces;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.services.interfaces;
using System.Linq.Expressions;

namespace OGCP.Curriculum.API.services;

public class QualifiedProfileService : IQualifiedProfileService
{
    private readonly IQualifiedProfileWriteRepo repository;

    public QualifiedProfileService(IQualifiedProfileWriteRepo repository)
    {
        this.repository = repository;
    }

    public async Task<Result> AddEducationAsync(int id, Education education)
    {
        Maybe<QualifiedProfile> profile = await this.repository.FindAsync(id);
        if (profile.HasNoValue)
        {
            return Result.Failure("");
        }

        profile.Value.AddEducation(education);

        await this.repository.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result> AddJobExperience<T>(int id, T request)
    {
        Maybe<QualifiedProfile> profile = await this.repository.FindAsync(id);
        JobExperience jobExperince = null;
        profile.Value.AddJobExperience(jobExperince);

        await this.repository.SaveChangesAsync();
        return Result.Success();

    }

    private Expression<Func<QualifiedProfile, object>>[] GetQueryExpression()
    {
        return
        [
            x => x.Educations,
            x => x.Experiences,
            x => x.LanguagesSpoken,
            x => x.PersonalInfo,
        ];
    }

    //public async Task<Result> Create(QualifiedProfile request)
    //{
    //    var result = this.repository.Add(request);

    //    if (result.IsFailure)
    //    {
    //        throw new ArgumentException();
    //    }

    //    var resultSave = repository.SaveChanges();

    //    return Result.Success();
    //}

    public Task<IReadOnlyList<QualifiedProfile>> GetAsync()
    {
        //return this.repository.Find();
        return null;
    }

    public Task<QualifiedProfile> GetAsync(int id)
    {
        //return this.repository.Find(id);
        return null;
    }

    public async Task<Result> UpdateEducationAsync(int profileId, int educationId, Education education)
    {
        Maybe<QualifiedProfile> profile = await this.repository.FindAsync(profileId);
        if (profile.HasNoValue)
        {
            return Result.Failure("");
        }


        profile.Value.UpdateEducation(educationId, education);

        await this.repository.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result> RemoveEducationAsync(int id, int educationId)
    {
        const string removeEducation = "EXEC DeleteOrphanedEducations;";
        Maybe<QualifiedProfile> profile = await this.repository.FindAsync(id);

        Result result = profile.Value.RemoveEducation(educationId);

        if (result.IsFailure)
        {
            return result;
        }
        await this.repository.SaveChangesAsync();

        return await this.repository.RemoveOrphanEducationsAsync(removeEducation);
    }

    public Task<Maybe<DegreeEducation>> FindDegreeEducation(string institution, EducationLevel degree)
    {
        return this.repository.FindDegreeEducation(institution, degree);
    }

    public Task<Maybe<ResearchEducation>> FindResearchEducation(string institution, string projectTitle)
    {
        return this.repository.FindResearchEducation(institution, projectTitle);
    }
}
