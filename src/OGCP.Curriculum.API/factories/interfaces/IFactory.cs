using OGCP.Curriculum.API.repositories.interfaces;
using OGCP.Curriculum.API.services;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.factories.interfaces;

public static class GenericFactory
{
    private static IQualifiedProfileService FindQualifiedProfileService(this IServiceProvider provider)
    {
        return provider.GetRequiredService<IQualifiedProfileService>();
    }

    private static IGeneralProfileService FindGeneralProfileService(this IServiceProvider provider)
    {
        return provider.GetRequiredService<IGeneralProfileService>();
    }

    private static IStudentProfileService FindStudentProfileService(this IServiceProvider provider)
    {
        return provider.GetRequiredService<IStudentProfileService>();
    }
}