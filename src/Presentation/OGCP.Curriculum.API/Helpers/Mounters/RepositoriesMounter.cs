using OGCP.Curriculum.API.DAL.Mutations.Interfaces;
using OGCP.Curriculum.API.DAL.Mutations;
using OGCP.Curriculum.API.DAL.Queries.interfaces;
using OGCP.Curriculum.API.DAL.Queries;

namespace OGCP.Curriculum.API.Helpers.DIMounters;

public static class RepositoriesMounter
{
    public static void SetupRepositories(this IServiceCollection Services)
    {
        Services.AddScoped<IGeneralProfileWriteRepo, GeneralProfileWriteRepo>();
        Services.AddScoped<IStudentProfileWriteRepo, StudentProfileWriteRepo>();
        Services.AddScoped<IQualifiedProfileWriteRepo, QualifiedProfileWriteRepo>();
        Services.AddScoped<IProfileWriteRepo, ProfileWriteRepo>();
        Services.AddScoped<IProfileReadModelRepository, ProfileReadModelRepository>();
    }
}
