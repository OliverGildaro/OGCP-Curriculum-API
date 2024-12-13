using OGCP.Curriculum.API.DAL.Mutations.Interfaces;
using OGCP.Curriculum.API.DAL.Mutations;
using OGCP.Curriculum.API.DAL.Queries.interfaces;
using OGCP.Curriculum.API.DAL.Queries;
using OGCP.Curriculums.Reads.ProfileRepository;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Distributed;

namespace OGCP.Curriculum.API.Helpers.DIMounters;

public static class RepositoriesMounter
{
    public static void SetupRepositories(this IServiceCollection Services, IConfiguration Configuration)
    {
        Services.AddScoped<IGeneralProfileWriteRepo, GeneralProfileWriteRepo>();
        Services.AddScoped<IStudentProfileWriteRepo, StudentProfileWriteRepo>();
        Services.AddScoped<IQualifiedProfileWriteRepo, QualifiedProfileWriteRepo>();
        Services.AddScoped<IProfileWriteRepo, ProfileWriteRepo>();
        //Services.AddMemoryCache();
        //Services.AddScoped<IProfileReadModelRepository, ProfileReadModelCacheRepository>();
        Services.AddScoped<ProfileReadModelRepository>();
        Services.AddScoped<IProfileReadModelRepository>(provider =>
        {
            var profileReadModelRepo = provider.GetService<ProfileReadModelRepository>();

            return new ProfileReadModelCacheRepository(profileReadModelRepo,
                provider.GetService<IDistributedCache>()!);
        });

        Services.AddStackExchangeRedisCache(options =>
        {
            var redicConnection = Configuration.GetConnectionString("redis");
            options.Configuration = redicConnection;
        });
    }
}
