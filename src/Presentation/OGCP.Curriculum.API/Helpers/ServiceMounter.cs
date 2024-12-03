using JsonSubTypes;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.DTOs.requests.Education;
using OGCP.Curriculum.API.DTOs;
using OGCP.Curriculum.API.POCOS.requests.Education;
using OGCP.Curriculum.API.POCOS.requests.Profile;
using OGCP.Curriculum.API.POCOS.requests.work;
using OGCP.Curriculum.API.services.interfaces;
using OGCP.Curriculum.API.services;
using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.commanding.commands.AddEducationDegree;
using OGCP.Curriculum.API.commanding.commands.AddEducationResearch;
using OGCP.Curriculum.API.commanding.commands.AddLanguageToProfile;
using OGCP.Curriculum.API.commanding.commands.CreateQualifiedProfile;
using OGCP.Curriculum.API.commanding.commands.EditLanguageFromProfile;
using OGCP.Curriculum.API.Commanding.commands.RemoveEducationFromQualifiedProfile;
using OGCP.Curriculum.API.Commanding.commands.RemoveEducationFromStudentProfile;
using OGCP.Curriculum.API.commanding.commands.UpdateEducationToQualifiedProfile;
using OGCP.Curriculum.API.commanding;
using OGCP.Curriculum.API.DAL.Mutations.Interfaces;
using OGCP.Curriculum.API.DAL.Mutations;
using OGCP.Curriculum.API.Commanding.commands.UpdateEducationFromStudentProfile;
using OGCP.Curriculum.API.DTOs.requests.Profile;
using OGCP.Curriculum.API.Commanding.commands.UpdateProfile;
using OGCP.Curriculum.API.DAL.Queries.context;
using OGCP.Curriculum.API.DAL.Mutations.context;
using OGCP.Curriculum.API.DAL.Queries.Models;
using OGCP.Curriculum.API.DAL.Queries;
using OGCP.Curriculum.API.DAL.Queries.interfaces;
using OGCP.Curriculum.API.Querying.GetProfiles;
using OGCP.Curriculum.API.Querying.GetProfileById;
using ArtForAll.Shared.ErrorHandler.Maybe;
using OGCP.Curriculum.API.Commanding.commands.DeleteProfile;

namespace OGCP.Curriculum.API.Helpers;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        try
        {
            builder.Services.SetupControllers();
            builder.Services.SetupCommands();
            builder.Services.SetupQueries();
            builder.Services.SetupServices();
            builder.Services.SetupRepositories();
            builder.Services.SetupDbContext(builder.Configuration);
            return builder.Build();
        }
        catch (Exception ex)
        {

            throw;
        }
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
                .Of(typeof(UpdateProfileRequest), "RequestType")
                .RegisterSubtype(typeof(UpdateGeneralProfileRequest), ProfileRequests.UpdateGeneral)
                .RegisterSubtype(typeof(UpdateQualifiedProfileRequest), ProfileRequests.UpdateQualified)
                .RegisterSubtype(typeof(UpdateStudentProfileRequest), ProfileRequests.UpdateStudent)
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
        
        //EDUCATIONS
        Services.AddScoped<ICommandHandler<AddEducationToStudentProfileCommand, Result>, AddEducationToStudentProfileCommandHandler>();
        Services.AddScoped(typeof(ICommandHandler<AddDegreeEducationToQualifiedProfileCommand, Result>),
            typeof(AddEducationToQualifiedProfileCommandHandler<AddDegreeEducationToQualifiedProfileCommand, Result>));
        Services.AddScoped(typeof(ICommandHandler<AddResearchEducationToQualifiedProfileCommand, Result>),
            typeof(AddEducationToQualifiedProfileCommandHandler<AddResearchEducationToQualifiedProfileCommand, Result>));
        Services.AddScoped(typeof(ICommandHandler<UpdateDegreeEducationFromQualifiedProfileCommand, Result>),
            typeof(UpdateEducationFromQualifiedProfileCommandHandler<UpdateDegreeEducationFromQualifiedProfileCommand, Result>));
        Services.AddScoped(typeof(ICommandHandler<UpdateResearchEducationFromQualifiedProfileCommand, Result>),
            typeof(UpdateEducationFromQualifiedProfileCommandHandler<UpdateResearchEducationFromQualifiedProfileCommand, Result>));
        Services.AddScoped<ICommandHandler<UpdateEducationFromStudentProfileCommand, Result>, UpdateEducationFromStudentProfileCommandHandler>();
        Services.AddScoped<ICommandHandler<RemoveEducationFromQualifiedProfileCommand, Result>, RemoveEducationFromQualifiedProfileCommandHandler>();
        Services.AddScoped<ICommandHandler<RemoveEducationFromStudentProfileCommand, Result>, RemoveEducationFromStudentProfileCommandHandler>();
    }

    public static void SetupQueries(this IServiceCollection Services)
    {
        Services.AddScoped<IQueryHandler<GetProfilesQuery, IReadOnlyList<ProfileReadModel>>, GetProfilesQueryHandler>();
        Services.AddScoped<IQueryHandler<GetProfileByIdQuery, Maybe<ProfileReadModel>>, GetProfileByIdQueryHandler>();
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
        Services.AddScoped<IProfileReadModelRepository, ProfileReadModelRepository>();
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
        Services.AddScoped<DbWriteProfileContext>(provider =>
        {
            //I need to register in this way because there is an abiguity between the two constructors I have
            //In the DbProfileContext
            return new DbWriteProfileContext(new DbProfileWritesContextConfig
            {
                ConnectionString = Configuration.GetConnectionString("conectionDb"),
                UseConsoleLogger = true
            });
        });

        Services.AddScoped<DbReadProfileContext>(provider =>
        {
            //I need to register in this way because there is an abiguity between the two constructors I have
            //In the DbProfileContext
            return new DbReadProfileContext(new DbProfileReadsContextConfig
            {
                ConnectionString = Configuration.GetConnectionString("conectionDb"),
                UseConsoleLogger = true
            });
        });
    }
}
