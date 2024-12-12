using customMapper = AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OGCP.Curriculum.API.commanding;
using OGCP.Curriculum.API.commanding.commands.AddLanguageToProfile;
using OGCP.Curriculum.API.commanding.commands.EditLanguageFromProfile;
using OGCP.Curriculum.API.Commanding.commands.UpdateProfile;
using OGCP.Curriculum.API.DTOs.requests.Profile;
using OGCP.Curriculum.API.POCOS.requests.Language;
using OGCP.Curriculum.API.POCOS.requests.Profile;
using OGCP.Curriculum.API.POCOS.requests.work;
using OGCP.Curriculum.API.POCOS.responses;
using OGCP.Curriculum.API.Commanding.commands.DeleteProfile;
using System.Dynamic;
using OGCP.Curriculum.API.DAL.Queries.Models;
using OGCP.Curriculum.API.DAL.Queries.utils.expand;
using OGCP.Curriculum.API.DAL.Queries.utils;
using OGCP.Curriculum.API.commanding.commands.CreateQualifiedProfile;
using OGCP.Curriculum.API.Querying.GetProfileById;
using OGCP.Curriculum.API.Querying.GetProfiles;
using OGCP.Curriculum.API.Filters;
using OGCP.Curriculum.API.DAL.Queries.interfaces;
using ArtForAll.Shared.ErrorHandler.Maybe;
using OGCP.Curriculum.API.DTOs.responses;

namespace OGCP.Curriculum.API.Controllers;

[Route("api/v1/profiles")]
[EnableCors("AllowSpecificOrigins")]
[ServiceFilter(typeof(ExceptionHandlerFilter))]
public class ProfilesController : ApplicationController
{
    private readonly IProfileReadModelRepository repository;
    private readonly Message message;
    private readonly customMapper.IMapper mapper;

    public ProfilesController(IProfileReadModelRepository repository, Message message, customMapper.IMapper mapper)
    {
        this.repository = repository;
        this.message = message;
        this.mapper = mapper;
    }
    [HttpGet("error")]
    public IActionResult GetError()
    {
        throw new Exception("This is a test exception.");
    }

    [HttpGet]
    public async Task<IActionResult> GetProfilesAsync([FromQuery] QueryParameters parameters)
    {
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



    [HttpGet("{id}")]
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
        try
        {
            var command = this.mapper.Map<CreateProfileCommand>(profileRequest);
            await this.message.DispatchCommand(command);
            return Ok();
        }
        catch (Exception ex)
        {

            throw;
        }
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
            FirstName = p.FirstName,
            LastName = p.LastName,
            Summary = p.Summary,
            Id = p.Id
        };
    }
}
