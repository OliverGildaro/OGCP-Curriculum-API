using OGCP.Curriculum.API.services.interfaces;
using OGCP.Curriculum.API.services;

namespace OGCP.Curriculum.API.Helpers.DIMounters;

public static class ApplicationServicesMounter
{
    public static void SetupServices(this IServiceCollection Services)
    {
        Services.AddScoped<IStudentProfileService, StudentProfileService>();
        Services.AddScoped<IQualifiedProfileService, QualifiedProfileService>();
        Services.AddScoped<IGeneralProfileService, GeneralProfileService>();
        Services.AddScoped<IProfileService, ProfileService>();
    }
}
