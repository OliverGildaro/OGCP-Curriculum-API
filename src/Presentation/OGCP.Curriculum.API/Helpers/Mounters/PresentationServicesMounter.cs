using JsonSubTypes;
using OGCP.Curriculum.API.commanding;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.DTOs.requests.Education;
using OGCP.Curriculum.API.DTOs.requests.Profile;
using OGCP.Curriculum.API.DTOs;
using OGCP.Curriculum.API.Filters;
using OGCP.Curriculum.API.POCOS.requests.Education;
using OGCP.Curriculum.API.POCOS.requests.Profile;
using OGCP.Curriculum.API.POCOS.requests.work;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using OGCP.Curriculums.API.Envelopes;
using System.Net;
using ArtForAll.Shared.Contracts.DDD;
using FluentValidation.AspNetCore;
using OGCP.Curriculum.API.Validators;
using OGCP.Curriculums.Core.DomainModel;

namespace OGCP.Curriculum.API.Helpers.DIMounters;

public static class PresentationServicesMounter
{
    public static void SetupControllers(this IServiceCollection Services)
    {
        Services.AddControllers(options =>
        {
            options.Filters.Add<ExceptionHandlerFilter>();
        })
        .ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = ModelStateValidator.ValidateModelState;
        })
        .AddFluentValidation(options =>
        {
            options.RegisterValidatorsFromAssemblyContaining<CreateGeneralProfileRequestValidator>();
            options.RegisterValidatorsFromAssemblyContaining<CreateStudentProfileRequestValidator>();
            options.RegisterValidatorsFromAssemblyContaining<AddDegreeEducationRequestValidator>();
        })
        .AddNewtonsoftJson(options =>
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

        Services.AddScoped<ExceptionHandlerFilter>();

        Services.AddLocalization(options => options.ResourcesPath = "Resources");
    }

    private class ModelStateValidator
    {
        public static IActionResult ValidateModelState(ActionContext context)
        {
            (string fieldName, ModelStateEntry entry) = context.ModelState.First(x => x.Value.Errors.Count > 0);
            string errorSerialized = entry.Errors.First().ErrorMessage;

            Error error = Error.Deserialize(errorSerialized);
            Envelope envelope = Envelope.Error(error, fieldName);
            var envelopeResult = new EnvelopeResult(envelope, HttpStatusCode.BadRequest);

            return envelopeResult;
        }
    }
}
