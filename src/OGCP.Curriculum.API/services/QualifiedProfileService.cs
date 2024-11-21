using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.repositories.interfaces;
using OGCP.Curriculum.API.services.interfaces;
using System.Linq.Expressions;

namespace OGCP.Curriculum.API.services;

public class QualifiedProfileService : IQualifiedProfileService
{
    private readonly IQualifiedProfileRepository repository;

    public QualifiedProfileService(IQualifiedProfileRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result> AddEducation(int id, DegreeEducation education)
    {
        QualifiedProfile profile = await this.repository.Find(id);
        if (profile is null)
        {
            return Result.Failure("");
        }

        profile.AddEducation(education);

        await this.repository.SaveChanges();
        return Result.Success();

    }

    public async Task<Result> AddJobExperience<T>(int id, T request)
    {
        var profile = await this.repository.Find(id);
        JobExperience jobExperince = FactoryJob.Get(request);
        profile.AddJobExperience(jobExperince);

        await this.repository.SaveChanges();
        return Result.Success();

    }

    private Expression<Func<QualifiedProfile, object>>[] GetQueryExpression()
    {
        return new Expression<Func<QualifiedProfile, object>>[]
        {
            x => x.Educations,
            x => x.Experiences,
            x => x.LanguagesSpoken,
            x => x.PersonalInfo,
        };
    }

    public Task<int> Create(QualifiedProfile request)
    {

        this.repository.Add(request);

        return this.repository.SaveChanges();
    }

    public Task<IReadOnlyList<QualifiedProfile>> Get()
    {
        return this.repository.Find();
    }

    public Task<QualifiedProfile> Get(int id)
    {
        return this.repository.Find(id);
    }
}
