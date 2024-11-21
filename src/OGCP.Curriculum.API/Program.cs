using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.ErrorHandler;
using JsonSubTypes;
using Microsoft.EntityFrameworkCore;
using OGCP.Curriculum.API.commanding;
using OGCP.Curriculum.API.commanding.commands.AddEducationDegree;
//using OGCP.Curriculum.API.commanding.commands.AddEducationResearch;
using OGCP.Curriculum.API.commanding.commands.AddLanguageToProfile;
using OGCP.Curriculum.API.commanding.commands.CreateGeneralProfile;
using OGCP.Curriculum.API.commanding.commands.CreateQualifiedProfile;
using OGCP.Curriculum.API.commanding.commands.CreateStudentProfile;
using OGCP.Curriculum.API.commanding.commands.EditLanguageFromProfile;
using OGCP.Curriculum.API.commanding.queries;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.dtos.requests;
using OGCP.Curriculum.API.POCOS.requests;
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
        .RegisterSubtype(typeof(CreateGeneralProfileRequest), ProfileTypes.CreateGeneralProfileRequest)
        .RegisterSubtype(typeof(CreateQualifiedProfileRequest), ProfileTypes.CreateQualifiedProfileRequest)
        .RegisterSubtype(typeof(CreateStudentProfileRequest), ProfileTypes.CreateStudentProfileRequest)
        .SerializeDiscriminatorProperty()
        .Build()
    );

    options.SerializerSettings.Converters.Add(
        JsonSubtypesConverterBuilder
        .Of(typeof(AddEducationRequest), "EducationType")
        .RegisterSubtype(typeof(AddResearchEducationRequest), EducationTypes.AddEducationResearchRequest)
        .RegisterSubtype(typeof(AddDegreeEducationRequest), EducationTypes.AddEducationDegreeRequest)
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
builder.Services.AddScoped<ICommandHandler<EditLangueFromProfileCommand, Result>, EditLanguageFromProfileCommandHandler>();
builder.Services.AddScoped<ICommandHandler<AddLangueToProfileCommand, Result>, AddLanguageToProfileCommandHandler>();
builder.Services.AddScoped<ICommandHandler<RemoveLangueFromProfileCommand, Result>, RemoveLanguageFromProfileCommandHandler>();
//builder.Services.AddScoped<ICommandHandler<AddEducationToProfileCommand, Result>, AddEducationToProfileCommandHandler>();
builder.Services.AddScoped(typeof(ICommandHandler<,>), typeof(AddEducationToProfileCommandHandler<,>));
//builder.Services.AddScoped<ICommandHandler<AddEducationDegreeToProfileCommand, Result>, AddEducationToProfileCommandHandler>();
//builder.Services.AddScoped<ICommandHandler<AddEducationResearchToProfileCommand, Result>, AddEducationToProfileCommandHandler>();
//builder.Services.AddScoped<ICommandHandler<AddEducationResearchToProfileCommand, Result>, AddEducationResearchToProfileCommandHandler>();
builder.Services.AddScoped<IQueryHandler<GetProfilesQuery, IReadOnlyList<Profile>>, GetProfilesQueryHandler>();
builder.Services.AddScoped<DbProfileContext>();
builder.Services.AddScoped(provider => new DbProfileContextConfig
{
    ConnectionString = builder.Configuration.GetConnectionString("conectionDb"),
    UseConsoleLogger = true
});
builder.Services.AddScoped<Message>();

var app = builder.Build();

// Configure the HttP request pipeline.
//the request fgoes from one midleware to the next one in the pipeline
app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigins");

//In the case that the user fails authorization, from this midleware 403 is returned
app.UseAuthorization();

app.MapControllers();


app.Run();
