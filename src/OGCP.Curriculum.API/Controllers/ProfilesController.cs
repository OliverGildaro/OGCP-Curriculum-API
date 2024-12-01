using customMapper = AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OGCP.Curriculum.API.commanding;
using OGCP.Curriculum.API.commanding.commands.AddLanguageToProfile;
using OGCP.Curriculum.API.commanding.commands.EditLanguageFromProfile;
using OGCP.Curriculum.API.Commanding.commands.UpdateProfile;
using OGCP.Curriculum.API.DTOs.requests.Profile;
using OGCP.Curriculum.API.factories;
using OGCP.Curriculum.API.POCOS.requests.Language;
using OGCP.Curriculum.API.POCOS.requests.Profile;
using OGCP.Curriculum.API.POCOS.requests.work;
using OGCP.Curriculum.API.POCOS.responses;
using OGCP.Curriculum.API.services.interfaces;
using OGCP.Curriculum.API.Querying;
using OGCP.Curriculum.API.DAL.Queries.Models;
using OGCP.Curriculum.API.DAL.Queries.utils;
using OGCP.Curriculum.API.Querying.GetProfiles;
using OGCP.Curriculum.API.Querying.GetProfileById;
using OGCP.Curriculum.API.Commanding.commands.DeleteProfile;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Dynamic;
using OGCP.Curriculum.API.DAL.Queries.utils.expand;

namespace OGCP.Curriculum.API.Controllers;

[Route("api/v1/profiles")]
[EnableCors("AllowSpecificOrigins")]
[Produces("application/json")]
public class ProfilesController : Controller
{
    private readonly IQualifiedProfileService service;
    private readonly Message message;
    private readonly customMapper.IMapper mapper;

    public ProfilesController(IQualifiedProfileService service, Message message, customMapper.IMapper mapper)
    {
        this.service = service;
        this.message = message;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetProfilesAsync([FromQuery] QueryParameters parameters)
    {
        var query = new GetProfilesQuery()
        {
            Parameters = parameters
        };

        IReadOnlyList<ProfileReadModel> profiles = await this.message.DispatchQuery(query);
        
        IEnumerable<ExpandoObject> eventEntitiesDto =
                profiles.ShapeData(parameters.Fields);
        return Ok(eventEntitiesDto);
    }



    [HttpGet("{id}")]
    public async Task<IActionResult> GetProfileByIdAsync(int id)
    {
        var query = new GetProfileByIdQuery
        {
            Id = id
        };

        var profile = await this.message.DispatchQuery(query);
        return Ok(profile);
    }

    [HttpPost]
    [Consumes("application/json")]
    public async Task<IActionResult> CreateProfileAsync([FromBody] ProfileRequest profileRequest)
    {
        var command = ProfileFactory.Get(profileRequest);
        await this.message.DispatchCommand(command);
        return Ok();
    }


    [HttpPut("{id}")]
    [ProducesResponseType(203)]
    public async Task<IActionResult> UpdateProfileAsync(int id, [FromBody] UpdateProfileRequest profile)
    {
        try
        {
            var command = this.mapper.Map<UpdateProfileCommand>(profile);
            command.Id = id;
            await this.message.DispatchCommand(command);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
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
        try
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
        catch (Exception ex)
        {
            return BadRequest();
        }
    }

    [HttpPut("{id}/languages/{languageId}")]
    [ProducesResponseType(203)]
    public async Task<IActionResult> EditLanguageFromProfileAsync(int id, int languageId, [FromBody] UpdateLanguageRequest request)
    {
        try
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
        catch (Exception ex)
        {
            return BadRequest();
        }
    }


    [HttpDelete("{id}/languages/{languageId}")]
    [ProducesResponseType(203)]
    public async Task<IActionResult> RemoveLanguageFromProfileAsync(int id, int languageId)
    {
        try
        {
            var command = new RemoveLangueFromProfileCommand
            {
                Id = id,
                LanguageId = languageId,
            };

            await this.message.DispatchCommand(command);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
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
