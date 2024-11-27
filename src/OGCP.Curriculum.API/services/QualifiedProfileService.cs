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

    public async Task<Result> AddEducation(int id, Education education)
    {
        Maybe<QualifiedProfile> profile = await this.repository.Find(id);
        if (profile.HasValue)
        {
            return Result.Failure("");
        }

        profile.Value.AddEducation(education);

        await this.repository.SaveChanges();
        return Result.Success();

    }

    public async Task<Result> AddJobExperience<T>(int id, T request)
    {
        Maybe<QualifiedProfile> profile = await this.repository.Find(id);
        JobExperience jobExperince = FactoryJob.Get(request);
        profile.Value.AddJobExperience(jobExperince);

        await this.repository.SaveChanges();
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

    public Task<int> Create(QualifiedProfile request)
    {
        this.repository.Add(request);
        return this.repository.SaveChanges();
    }

    public Task<IReadOnlyList<QualifiedProfile>> Get()
    {
        //return this.repository.Find();
        return null;
    }

    public Task<QualifiedProfile> Get(int id)
    {
        //return this.repository.Find(id);
        return null;
    }

    public async Task<Result> UpdateEducation(int profileId, Education education)
    {
        Maybe<QualifiedProfile> profile = await this.repository.Find(profileId);
        if (profile.HasNoValue)
        {
            return Result.Failure("");
        }

        profile.Value.UpdateEducation(education);

        await this.repository.SaveChanges();
        return Result.Success();
    }

    public async Task<Result> RemoveEducation(int id, int educationId)
    {
        const string removeEducation = "EXEC DeleteOrphanedEducations;";
        Maybe<QualifiedProfile> profile = await this.repository.Find(id);

        Result result = profile.Value.RemoveEducation(educationId);

        if (result.IsFailure)
        {
            return result;
        }
        await this.repository.SaveChanges();

        return await this.repository.RemoveOrphanEducations(removeEducation);
    }
}
