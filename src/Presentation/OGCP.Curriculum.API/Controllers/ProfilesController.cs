using ArtForAll.Shared.ErrorHandler;
using ArtForAll.Shared.ErrorHandler.Maybe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OGCP.Curriculum.API.commanding;
using OGCP.Curriculum.API.commanding.commands.AddLanguageToProfile;
using OGCP.Curriculum.API.commanding.commands.AddSkillToLanguage;
using OGCP.Curriculum.API.commanding.commands.CreateQualifiedProfile;
using OGCP.Curriculum.API.commanding.commands.EditLanguageFromProfile;
using OGCP.Curriculum.API.Commanding.commands.DeleteProfile;
using OGCP.Curriculum.API.Commanding.commands.UpdateProfile;
using OGCP.Curriculum.API.DAL.Queries.interfaces;
using OGCP.Curriculum.API.DAL.Queries.Models;
using OGCP.Curriculum.API.DAL.Queries.utils;
using OGCP.Curriculum.API.DAL.Queries.utils.expand;
using OGCP.Curriculum.API.DTOs.requests.Language;
using OGCP.Curriculum.API.DTOs.requests.Profile;
using OGCP.Curriculum.API.DTOs.responses;
using OGCP.Curriculum.API.Filters;
using OGCP.Curriculum.API.POCOS.requests.Language;
using OGCP.Curriculum.API.POCOS.requests.Profile;
using OGCP.Curriculum.API.POCOS.requests.work;
using OGCP.Curriculum.API.POCOS.responses;
using OGCP.Curriculum.API.Querying.GetProfileById;
using OGCP.Curriculum.API.Querying.GetProfiles;
using OGCP.Curriculums.Ports;
using System.Dynamic;
using static System.Formats.Asn1.AsnWriter;
using customMapper = AutoMapper;

namespace OGCP.Curriculum.API.Controllers;

[Route("api/v1/profiles")]
[EnableCors("AllowSpecificOrigins")]
[ServiceFilter(typeof(ExceptionHandlerFilter))]
public class ProfilesController : ApplicationController
{
    private readonly IProfileReadModelRepository repository;
    private readonly Message message;
    private readonly customMapper.IMapper mapper;
    private readonly LinkGenerator linkGenerator;
    private readonly IApplicationInsights insights;

    public ProfilesController(
        IProfileReadModelRepository repository,
        Message message,
        customMapper.IMapper mapper,
        LinkGenerator linkGenerator,
        IApplicationInsights insights)
    {
        this.repository = repository;
        this.message = message;
        this.mapper = mapper;
        this.linkGenerator = linkGenerator;
        this.insights = insights;
    }
    [HttpGet("error")]
    public IActionResult GetError()
    {
        throw new Exception("This is a test exception.");
    }

    [HttpGet]
    //[Authorize]
    //[SkipModelValidationFilter]
    public async Task<IActionResult> GetProfilesAsync([FromQuery] QueryParameters parameters)
    {
        //insights.LogInformation(string.Format("Scopes: {0}", scopes));
        insights.LogInformation("MY_TRACKINGS: Entering to the endpoint");

        var query = new GetProfilesQuery()
        {
            Parameters = parameters
        };

        IReadOnlyList<ProfileReadModel> profiles = await this.message.DispatchQuery(query);
        IReadOnlyList<ProfileResponse> profilesResponse = mapper.Map<IReadOnlyList<ProfileResponse>>(profiles);
        IEnumerable<ExpandoObject> eventEntitiesDto =
                profilesResponse.ShapeData(parameters.Fields);
        return Ok(eventEntitiesDto);
    }



    [HttpGet("{id}", Name = "GetProfileById")]
    public async Task<IActionResult> GetProfileByIdAsync(int id)
    {
        var query = new GetProfileByIdQuery
        {
            Id = id
        };

        Maybe<ProfileReadModel> maybeProfile = await this.message.DispatchQuery(query);
        if (maybeProfile.HasNoValue)
        {
            return NotFound();
        }
        ProfileResponse profileResponse = mapper.Map<ProfileResponse>(maybeProfile.Value);

        return Ok(profileResponse);
    }

    [HttpGet("{id}/languages")]
    public async Task<IActionResult> GetLanguagesFromProfielAsync(int id)
    {
        IReadOnlyList<LanguageReadModel> languages = await this.repository.FindLanguagesFromProfile(id);
        IReadOnlyList<LanguageResponse> langRes = this.mapper.Map<IReadOnlyList<LanguageResponse>>(languages);
        return Ok(langRes);
    }


    [HttpGet("languages")]
    public async Task<IActionResult> GetLanguagesGroupedAsync()
    {
        await this.repository.FindLanguagesGrouped();

        //IReadOnlyList<LanguageResponse> langRes = 
        //    this.mapper.Map<IReadOnlyList<LanguageResponse>>(languages);

        return Ok();
    }

    [HttpPost]
    [Consumes("application/json")]
    public async Task<IActionResult> CreateProfileAsync([FromBody] CreateProfileRequest profileRequest)
    {
        var command = this.mapper.Map<CreateProfileCommand>(profileRequest);
        var resultCreated = await this.message.DispatchCommand(command);
        var link = this.GenerateLinkForGetDashboardById(resultCreated.Id);

        return this.Created(link, new { productId = resultCreated.Id });
    }


    [HttpPut("{id}")]
    [ProducesResponseType(203)]
    public async Task<IActionResult> UpdateProfileAsync(int id, [FromBody] UpdateProfileRequest profile)
    {
        var command = this.mapper.Map<UpdateProfileCommand>(profile);
        command.Id = id;
        await this.message.DispatchCommand(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProfileAsync(int id)
    {
        var result = await this.message.DispatchCommand(new DeleteProfileCommand
        {
            Id = id
        });

        return NoContent();
    }


    [HttpPut("{id}/languages")]
    [ProducesResponseType(203)]
    public async Task<IActionResult> AddLanguageToProfileAsync(int id, [FromBody] AddLanguageRequest request)
    {
        var command = new AddLangueToProfileCommand
        {
            Id = id,
            Level = request.Level,
            Name = request.Name,
        };

        await this.message.DispatchCommand(command);

        return NoContent();
    }

    [HttpPost("{profileId}/languages/{educationId}/skills")]
    [ProducesResponseType(201)]
    public async Task<IActionResult> AddLanguageSkillToLanguageAsync(
        int profileId, int educationId, [FromBody] AddLanguageSkillRequest request)
    {
        var command = new AddLangueSkillToLanguageCommand
        {
            ProfileId = profileId,
            EducationId = educationId,
            Level = request.Level,
            Skill = request.Skill,
        };

        await this.message.DispatchCommand(command);

        return Created();
    }

    [HttpPut("{id}/languages/{languageId}")]
    [ProducesResponseType(203)]
    public async Task<IActionResult> EditLanguageFromProfileAsync(int id, int languageId, [FromBody] UpdateLanguageRequest request)
    {
        var command = new UpdateLanguageFromProfileCommand
        {
            Id = id,
            LanguageId = languageId,
            Level = request.Level,
            Name = request.Name,
        };

        await this.message.DispatchCommand(command);

        return NoContent();
    }


    [HttpDelete("{id}/languages/{languageId}")]
    [ProducesResponseType(203)]
    public async Task<IActionResult> RemoveLanguageFromProfileAsync(int id, int languageId)
    {
        var command = new RemoveLangueFromProfileCommand
        {
            Id = id,
            LanguageId = languageId,
        };

        await this.message.DispatchCommand(command);

        return NoContent();
    }

    [HttpPut("{id}/WorkExperienceTypes")]
    [ProducesResponseType(203)]
    public IActionResult AddJobExperienceToProfileAsync(int id, [FromBody] CreateJobExperienceRequest request)
    {
        try
        {
            //if (request is CreateWorkExperienceRequest workReq)
            //{
            //    var asas = workReq;
            //}
            //else if (request is CreateInternshipExperienceRequest interReq)
            //{ 
            //    var asas = interReq;
            //}

            //this.service.AddJobExperience(id, request);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }

    private ProfileResponse GetProfileDto(ProfileReadModel p)
    {
        return new ProfileResponse
        {
            GivenName = p.GivenName,
            FamilyNames = p.FamilyNames,
            Summary = p.Summary,
            Id = p.Id
        };
    }

    private string GenerateLinkForGetDashboardById(string id)
    {
        return this.linkGenerator.GetUriByAction(
            httpContext: this.HttpContext,
            action: nameof(this.GetProfileByIdAsync),
            controller: "profiles",
            values: new { profileId = id });
    }
}
