using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.ErrorHandler.Maybe;
using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.commanding.commands.AddEducationDegree;
using OGCP.Curriculum.API.commanding.commands.AddEducationResearch;
using OGCP.Curriculum.API.commanding.commands.AddLanguageToProfile;
using OGCP.Curriculum.API.commanding.commands.CreateQualifiedProfile;
using OGCP.Curriculum.API.Commanding.commands.DeleteProfile;
using OGCP.Curriculum.API.commanding.commands.EditLanguageFromProfile;
using OGCP.Curriculum.API.Commanding.commands.RemoveEducationFromQualifiedProfile;
using OGCP.Curriculum.API.Commanding.commands.RemoveEducationFromStudentProfile;
using OGCP.Curriculum.API.Commanding.commands.UpdateEducationFromStudentProfile;
using OGCP.Curriculum.API.commanding.commands.UpdateEducationToQualifiedProfile;
using OGCP.Curriculum.API.Commanding.commands.UpdateProfile;
using OGCP.Curriculum.API.DAL.Queries.Models;
using OGCP.Curriculum.API.Querying.GetProfileById;
using OGCP.Curriculum.API.Querying.GetProfiles;
using OGCP.Curriculum.API.commanding.commands.AddSkillToLanguage;
using OGCP.Curriculums.Commanding.ProfileCommandImages.AddImage;

namespace OGCP.Curriculum.API.Helpers.DIMounters;

public static class CommandsQueriesMounter
{
    public static void SetupCommands(this IServiceCollection Services)
    {
        //PROFILES
        Services.AddScoped(typeof(ICommandHandler<CreateQualifiedProfileCommand, Result>),
            typeof(CreateProfileCommandHandler<CreateQualifiedProfileCommand, Result>));
        Services.AddScoped(typeof(ICommandHandler<CreateGeneralProfileCommand, Result>),
            typeof(CreateProfileCommandHandler<CreateGeneralProfileCommand, Result>));
        Services.AddScoped(typeof(ICommandHandler<CreateStudentProfileCommand, Result>),
            typeof(CreateProfileCommandHandler<CreateStudentProfileCommand, Result>));

        Services.AddScoped(typeof(ICommandHandler<UpdateGeneralProfileCommand, Result>),
            typeof(UpdateProfileCommandHandler<UpdateGeneralProfileCommand, Result>));
        Services.AddScoped(typeof(ICommandHandler<UpdateQualifiedProfileCommand, Result>),
            typeof(UpdateProfileCommandHandler<UpdateQualifiedProfileCommand, Result>));
        Services.AddScoped(typeof(ICommandHandler<UpdateStudentProfileCommand, Result>),
            typeof(UpdateProfileCommandHandler<UpdateStudentProfileCommand, Result>));
        Services.AddScoped<ICommandHandler<DeleteProfileCommand, Result>, DeleteProfileCommandHandler>();

        //LANGUAGES
        Services.AddScoped<ICommandHandler<AddLangueToProfileCommand, Result>, AddLanguageToProfileCommandHandler>();
        Services.AddScoped<ICommandHandler<UpdateLanguageFromProfileCommand, Result>, UpdateLanguageFromProfileCommandHandler>();
        Services.AddScoped<ICommandHandler<RemoveLangueFromProfileCommand, Result>, RemoveLanguageFromProfileCommandHandler>();
        Services.AddScoped<ICommandHandler<AddLangueSkillToLanguageCommand, Result>, AddLangueSkillToLanguageCommandHandler>();

        //EDUCATIONS
        Services.AddScoped<ICommandHandler<AddEducationToStudentProfileCommand, Result>, AddEducationToStudentProfileCommandHandler>();
        Services.AddScoped<ICommandHandler<AddResearchEducationToQualifiedProfileCommand, Result>, AddResearchEducationToQualifiedProfileCommandHandler>();
        Services.AddScoped<ICommandHandler<AddDegreeEducationToQualifiedProfileCommand, Result>, AddDegreeEducationToQualifiedProfileCommandHandler>();
        Services.AddScoped<ICommandHandler<UpdateEducationFromStudentProfileCommand, Result>, UpdateEducationFromStudentProfileCommandHandler>();
        Services.AddScoped<ICommandHandler<UpdateDegreeEducationFromQualifiedProfileCommand, Result>, UpdateDegreeEducationFromQualifiedProfileCommandHandler>();
        Services.AddScoped<ICommandHandler<UpdateResearchEducationFromQualifiedProfileCommand, Result>, UpdateResearchEducationFromQualifiedProfileCommandHandler>();
        Services.AddScoped<ICommandHandler<RemoveEducationFromQualifiedProfileCommand, Result>, RemoveEducationFromQualifiedProfileCommandHandler>();
        Services.AddScoped<ICommandHandler<RemoveEducationFromStudentProfileCommand, Result>, RemoveEducationFromStudentProfileCommandHandler>();

        //IMAGES
        Services.AddScoped<ICommandHandler<AddImageCommand, Result>, AddImageCommandHandler>();

    }

    public static void SetupQueries(this IServiceCollection Services)
    {
        Services.AddScoped<IQueryHandler<GetProfilesQuery, IReadOnlyList<ProfileReadModel>>, GetProfilesQueryHandler>();
        Services.AddScoped<IQueryHandler<GetProfileByIdQuery, Maybe<ProfileReadModel>>, GetProfileByIdQueryHandler>();
    }
}
