using JsonSubTypes;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.DTOs.requests.Education;
using OGCP.Curriculum.API.DTOs;
using OGCP.Curriculum.API.POCOS.requests.Education;
using OGCP.Curriculum.API.POCOS.requests.Profile;
using OGCP.Curriculum.API.POCOS.requests.work;
using OGCP.Curriculum.API.services.interfaces;
using OGCP.Curriculum.API.services;
using OGCP.Curriculum.API.repositories.interfaces;
using OGCP.Curriculum.API.repositories;
using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.commanding.commands.AddEducationDegree;
using OGCP.Curriculum.API.commanding.commands.AddEducationResearch;
using OGCP.Curriculum.API.commanding.commands.AddLanguageToProfile;
using OGCP.Curriculum.API.commanding.commands.CreateGeneralProfile;
using OGCP.Curriculum.API.commanding.commands.CreateQualifiedProfile;
using OGCP.Curriculum.API.commanding.commands.CreateStudentProfile;
using OGCP.Curriculum.API.commanding.commands.EditLanguageFromProfile;
using OGCP.Curriculum.API.Commanding.commands.RemoveEducationFromQualifiedProfile;
using OGCP.Curriculum.API.Commanding.commands.RemoveEducationFromStudentProfile;
using OGCP.Curriculum.API.commanding.commands.UpdateEducationToQualifiedProfile;
using OGCP.Curriculum.API.commanding.queries;
using OGCP.Curriculum.API.repositories.utils;
using OGCP.Curriculum.API.commanding;
using OGCP.Curriculum.API.DAL.Mutations.Interfaces;
using OGCP.Curriculum.API.DAL.Mutations;

namespace OGCP.Curriculum.API.Helpers;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.SetupControllers();
        builder.Services.SetupCommands();
        builder.Services.SetupServices();
        builder.Services.SetupRepositories();
        builder.Services.SetupDbContext(builder.Configuration);
        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        // Configure the HttP request pipeline.
        //the request fgoes from one midleware to the next one in the pipeline
        app.UseHttpsRedirection();

        app.UseCors("AllowSpecificOrigins");

        //In the case that the user fails authorization, from this midleware 403 is returned
        app.UseAuthorization();

        app.MapControllers();


        return app;
    }
}

public static class ServiceMounter
{
    public static void SetupJsonDeserialization(this IServiceCollection serviceProvider)
    {

    }

    public static void SetupControllers(this IServiceCollection Services)
    {
        Services.AddControllers().AddNewtonsoftJson(options =>
        {
            //System.Text.Json does not support polimorphic deserialization, but it support limited serialization
            //Polimorphic deserialization can be achieved only using Newtonsoft.json
            //System.Text.Json is more performant than Newtonsoft.json
            options.SerializerSettings.Converters.Add(
                JsonSubtypesConverterBuilder
                .Of(typeof(ProfileRequest), "RequestType")
                .RegisterSubtype(typeof(CreateGeneralProfileRequest), ProfileRequests.CreateGeneral)
                .RegisterSubtype(typeof(CreateQualifiedProfileRequest), ProfileRequests.CreateQualified)
                .RegisterSubtype(typeof(CreateStudentProfileRequest), ProfileRequests.CreateStudent)
                .SerializeDiscriminatorProperty()
                .Build()
            );

            options.SerializerSettings.Converters.Add(
                JsonSubtypesConverterBuilder
                .Of(typeof(AddEducationRequest), "EducationType")
                .RegisterSubtype(typeof(AddResearchEducationRequest), EducationRequests.AddResearch)
                .RegisterSubtype(typeof(AddDegreeEducationRequest), EducationRequests.AddDegree)
                .RegisterSubtype(typeof(AddResearchEducationToStudentProfileRequest), EducationRequests.AddResearchToStudent)
                .SerializeDiscriminatorProperty()
                .Build()
            );

            options.SerializerSettings.Converters.Add(
            JsonSubtypesConverterBuilder
            .Of(typeof(UpdateEducationRequest), "EducationType")
            .RegisterSubtype(typeof(UpdateDegreeEducationRequest), EducationRequests.UpdateDegree)
            .RegisterSubtype(typeof(UpdateResearchEducationRequest), EducationRequests.UpdateResearch)
            .RegisterSubtype(typeof(UpdateEducationToStudentProfileRequest), EducationRequests.UpdateResearchFromStudent)
            .SerializeDiscriminatorProperty()
            .Build()
            );

            options.SerializerSettings.Converters.Add(
                JsonSubtypesConverterBuilder
                .Of(typeof(DeleteEducationRequest), "EducationType")
                .RegisterSubtype(typeof(DeleteStudentEducationRequest), EducationRequests.DeleteStudentEducation)
                .RegisterSubtype(typeof(DeleteQualifiedEducationRequest), EducationRequests.DeleteQualifiedEducation)
                .SerializeDiscriminatorProperty()
                .Build()
                );

            options.SerializerSettings.Converters.Add(
                JsonSubtypesConverterBuilder
                .Of(typeof(CreateJobExperienceRequest), "ExperiencesType")
                .RegisterSubtype(typeof(CreateInternshipExperienceRequest), WorkExperienceCategory.Internship)
                .RegisterSubtype(typeof(CreateWorkExperienceRequest), WorkExperienceCategory.Employment)
                .SerializeDiscriminatorProperty()
                .Build()
            );
        });

        Services.AddCors(options =>
        {
            options.AddPolicy(name: "AllowSpecificOrigins", builder =>
            {
                builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
            });
        });

        Services.AddScoped<Message>();
        Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    }

    public static void SetupCommands(this IServiceCollection Services)
    {
        Services.AddScoped<ICommandHandler<CreateGeneralProfileCommand, Result>, CreateGeneralProfileCommandHandler>();
        Services.AddScoped<ICommandHandler<CreateQualifiedProfileCommand, Result>, CreateQualifiedProfileCommandHandler>();
        Services.AddScoped<ICommandHandler<CreateStudentProfileCommand, Result>, CreateStudentProfileCommandHandler>();
        Services.AddScoped<ICommandHandler<UpdateLanguageFromProfileCommand, Result>, UpdateLanguageFromProfileCommandHandler>();
        Services.AddScoped<ICommandHandler<AddLangueToProfileCommand, Result>, AddLanguageToProfileCommandHandler>();
        Services.AddScoped<ICommandHandler<RemoveLangueFromProfileCommand, Result>, RemoveLanguageFromProfileCommandHandler>();
        Services.AddScoped<ICommandHandler<AddEducationToStudentProfileCommand, Result>, AddEducationToStudentProfileCommandHandler>();
        Services.AddScoped(typeof(ICommandHandler<UpdateDegreeEducationFromQualifiedProfileCommand, Result>),
            typeof(UpdateEducationFromQualifiedProfileCommandHandler<UpdateDegreeEducationFromQualifiedProfileCommand, Result>));
        Services.AddScoped(typeof(ICommandHandler<UpdateResearchEducationFromQualifiedProfileCommand, Result>),
            typeof(UpdateEducationFromQualifiedProfileCommandHandler<UpdateResearchEducationFromQualifiedProfileCommand, Result>));
        Services.AddScoped(typeof(ICommandHandler<AddDegreeEducationToQualifiedProfileCommand, Result>),
            typeof(AddEducationToQualifiedProfileCommandHandler<AddDegreeEducationToQualifiedProfileCommand, Result>));
        Services.AddScoped(typeof(ICommandHandler<AddResearchEducationToQualifiedProfileCommand, Result>),
            typeof(AddEducationToQualifiedProfileCommandHandler<AddResearchEducationToQualifiedProfileCommand, Result>));
        Services.AddScoped<ICommandHandler<RemoveEducationFromQualifiedProfileCommand, Result>, RemoveEducationFromQualifiedProfileCommandHandler>();
        Services.AddScoped<ICommandHandler<RemoveEducationFromStudentProfileCommand, Result>, RemoveEducationFromStudentProfileCommandHandler>();
        
        Services.AddScoped<IQueryHandler<GetProfilesQuery, IReadOnlyList<Profile>>, GetProfilesQueryHandler>();

    }

    public static void SetupServices(this IServiceCollection Services)
    {
        Services.AddScoped<IStudentProfileService, StudentProfileService>();
        Services.AddScoped<IQualifiedProfileService, QualifiedProfileService>();
        Services.AddScoped<IGeneralProfileService, GeneralProfileService>();
        Services.AddScoped<IProfileService, ProfileService>();
    }
    public static void SetupRepositories(this IServiceCollection Services)
    {
        Services.AddScoped<IGeneralProfileWriteRepo, GeneralProfileWriteRepo>();
        Services.AddScoped<IStudentProfileWriteRepo, StudentProfileWriteRepo>();
        Services.AddScoped<IQualifiedProfileWriteRepo, QualifiedProfileWriteRepo>();
        Services.AddScoped<IProfileWriteRepo, ProfileWriteRepo>();
        //Services.AddScoped<IProfileWriteRepo, ProfileWriteRepo>();
    }

    public static void SetupDbContext(this IServiceCollection Services, IConfiguration Configuration)
    {

        // Add services to the container.
        //The dbcontext is automatically dispose after getting out of scope along with the tracking objects
        //builder.Services.AddScoped(provider => new DbProfileContextConfig
        //{
        //    ConnectionString = builder.Configuration.GetConnectionString("conectionDb"),
        //    UseConsoleLogger = true
        //});
        Services.AddScoped<DbProfileContext>(provider =>
        {
            //I need to register in this way because there is an abiguity between the two constructors I have
            //In the DbProfileContext
            return new DbProfileContext(new DbProfileContextConfig
            {
                ConnectionString = Configuration.GetConnectionString("conectionDb"),
                UseConsoleLogger = true
            });
        });
    }
}
