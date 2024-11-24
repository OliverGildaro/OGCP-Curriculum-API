using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.ErrorHandler;
using JsonSubTypes;
using Microsoft.EntityFrameworkCore;
using OGCP.Curriculum.API.commanding;
using OGCP.Curriculum.API.commanding.commands.AddEducationDegree;
using OGCP.Curriculum.API.commanding.commands.AddEducationResearch;

//using OGCP.Curriculum.API.commanding.commands.AddEducationResearch;
using OGCP.Curriculum.API.commanding.commands.AddLanguageToProfile;
using OGCP.Curriculum.API.commanding.commands.CreateGeneralProfile;
using OGCP.Curriculum.API.commanding.commands.CreateQualifiedProfile;
using OGCP.Curriculum.API.commanding.commands.CreateStudentProfile;
using OGCP.Curriculum.API.commanding.commands.EditLanguageFromProfile;
using OGCP.Curriculum.API.commanding.commands.UpdateEducationToQualifiedProfile;
using OGCP.Curriculum.API.commanding.queries;
using OGCP.Curriculum.API.Commanding.commands.RemoveEducationFromQualifiedProfile;
using OGCP.Curriculum.API.Commanding.commands.RemoveEducationFromStudentProfile;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.DTOs;
using OGCP.Curriculum.API.DTOs.requests.Education;
using OGCP.Curriculum.API.POCOS.requests.Education;
using OGCP.Curriculum.API.POCOS.requests.Profile;
using OGCP.Curriculum.API.POCOS.requests.work;
using OGCP.Curriculum.API.repositories;
using OGCP.Curriculum.API.repositories.interfaces;
using OGCP.Curriculum.API.repositories.utils;
using OGCP.Curriculum.API.services;
using OGCP.Curriculum.API.services.interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//The dbcontext is automatically dispose after getting out of scope along with the tracking objects
builder.Services.AddDbContext<DbProfileContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("conectionDb")));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowSpecificOrigins", builder =>
    {
        builder.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});

//System.Text.Json does not support polimorphic deserialization, but it support limited serialization
//Polimorphic deserialization can be achieved only using Newtonsoft.json
//System.Text.Json is more performant than Newtonsoft.json
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
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

builder.Services.AddScoped<IStudentProfileService, StudentProfileService>();
builder.Services.AddScoped<IQualifiedProfileService, QualifiedProfileService>();
builder.Services.AddScoped<IGeneralProfileService, GeneralProfileService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IGeneralProfileRepository, GeneralProfileRepository>();
builder.Services.AddScoped<IStudentProfileRepository, StudentProfileRepository>();
builder.Services.AddScoped<IQualifiedProfileRepository, QualifiedProfileRepository>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<ICommandHandler<CreateGeneralProfileCommand, Result>, CreateGeneralProfileCommandHandler>();
builder.Services.AddScoped<ICommandHandler<CreateQualifiedProfileCommand, Result>, CreateQualifiedProfileCommandHandler>();
builder.Services.AddScoped<ICommandHandler<CreateStudentProfileCommand, Result>, CreateStudentProfileCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateLanguageFromProfileCommand, Result>, UpdateLanguageFromProfileCommandHandler>();
builder.Services.AddScoped<ICommandHandler<AddLangueToProfileCommand, Result>, AddLanguageToProfileCommandHandler>();
builder.Services.AddScoped<ICommandHandler<RemoveLangueFromProfileCommand, Result>, RemoveLanguageFromProfileCommandHandler>();
builder.Services.AddScoped<ICommandHandler<AddEducationToStudentProfileCommand, Result>, AddEducationToStudentProfileCommandHandler>();
builder.Services.AddScoped(typeof(ICommandHandler<UpdateDegreeEducationFromQualifiedProfileCommand, Result>),
    typeof(UpdateEducationFromQualifiedProfileCommandHandler<UpdateDegreeEducationFromQualifiedProfileCommand, Result>));
builder.Services.AddScoped(typeof(ICommandHandler<UpdateResearchEducationFromQualifiedProfileCommand, Result>),
    typeof(UpdateEducationFromQualifiedProfileCommandHandler<UpdateResearchEducationFromQualifiedProfileCommand,Result>));
builder.Services.AddScoped(typeof(ICommandHandler<AddDegreeEducationToQualifiedProfileCommand, Result>),
    typeof(AddEducationToQualifiedProfileCommandHandler<AddDegreeEducationToQualifiedProfileCommand,Result>));
builder.Services.AddScoped(typeof(ICommandHandler<AddResearchEducationToQualifiedProfileCommand, Result>),
    typeof(AddEducationToQualifiedProfileCommandHandler<AddResearchEducationToQualifiedProfileCommand,Result>));
builder.Services.AddScoped<ICommandHandler<RemoveEducationFromQualifiedProfileCommand, Result>, RemoveEducationFromQualifiedProfileCommandHandler>();
builder.Services.AddScoped<ICommandHandler<RemoveEducationFromStudentProfileCommand, Result>, RemoveEducationFromStudentProfileCommandHandler>();

builder.Services.AddScoped<IQueryHandler<GetProfilesQuery, IReadOnlyList<Profile>>, GetProfilesQueryHandler>();
builder.Services.AddScoped<DbProfileContext>();
builder.Services.AddScoped(provider => new DbProfileContextConfig
{
    ConnectionString = builder.Configuration.GetConnectionString("conectionDb"),
    UseConsoleLogger = true
});
builder.Services.AddScoped<Message>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();

// Configure the HttP request pipeline.
//the request fgoes from one midleware to the next one in the pipeline
app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigins");

//In the case that the user fails authorization, from this midleware 403 is returned
app.UseAuthorization();

app.MapControllers();


app.Run();
