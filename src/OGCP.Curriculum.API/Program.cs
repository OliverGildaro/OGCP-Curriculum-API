using JsonSubTypes;
using Microsoft.EntityFrameworkCore;
using OGCP.Curriculum.API.domainModel;
using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.dtos.requests;
using OGCP.Curriculum.API.repositories;
using OGCP.Curriculum.API.repositories.interfaces;
using OGCP.Curriculum.API.services;
using OGCP.Curriculum.API.services.interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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
        .RegisterSubtype(typeof(CreateGeneralProfileRequest), ProfileEnum.CreateGeneralProfileRequest)
        .RegisterSubtype(typeof(CreateQualifiedProfileRequest), ProfileEnum.CreateQualifiedProfileRequest)
        .RegisterSubtype(typeof(CreateStudentProfileRequest), ProfileEnum.CreateStudentProfileRequest)
        .SerializeDiscriminatorProperty()
        .Build()
    );

    options.SerializerSettings.Converters.Add(
        JsonSubtypesConverterBuilder
        .Of(typeof(CreateJobExperienceRequest), "ExperiencesType")
        .RegisterSubtype(typeof(CreateInternshipExperienceRequest), WorkExperiences.INTERSHIP)
        .RegisterSubtype(typeof(CreateWorkExperienceRequest), WorkExperiences.WORK)
        .SerializeDiscriminatorProperty()
        .Build()
    );
});

builder.Services.AddScoped<IStudentProfileService, StudentProfileService>();
builder.Services.AddScoped<IQualifiedProfileService, QualifiedProfileService>();
builder.Services.AddScoped<IGeneralProfileService, GeneralProfileService>();
builder.Services.AddScoped<IGeneralProfileRepository, GeneralProfileRepository>();
builder.Services.AddScoped<IStudentProfileRepository, StudentProfileRepository>();
builder.Services.AddScoped<IQualifiedProfileRepository, QualifiedProfileRepository>();

var app = builder.Build();

// Configure the HttP request pipeline.

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
