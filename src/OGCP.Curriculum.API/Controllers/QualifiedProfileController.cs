using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OGCP.Curriculum.API.commanding;
using OGCP.Curriculum.API.commanding.commands.AddLanguageToProfile;
using OGCP.Curriculum.API.commanding.commands.EditLanguageFromProfile;
using OGCP.Curriculum.API.commanding.queries;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.dtos.requests;
using OGCP.Curriculum.API.factories;
using OGCP.Curriculum.API.POCOS.requests;
using OGCP.Curriculum.API.POCOS.responses;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.Controllers;

[Route("api/v1/profiles")]
[EnableCors("AllowSpecificOrigins")]
[Produces("application/json")]
public class QualifiedProfileController : Controller
{
    private readonly IQualifiedProfileService service;
    private readonly Message message;

    public QualifiedProfileController(IQualifiedProfileService service, Message message)
    {
        this.service = service;
        this.message = message;
    }

    [HttpGet]
    public async Task<IActionResult> GetProfiles()
    {
        var query = new GetProfilesQuery();
        IReadOnlyList<Profile> profiles = await this.message.DispatchQuery(query);
        IReadOnlyList<ProfileResponse> response = profiles.Select(p => GetProfileDto(p)).ToArray();
        
        return Ok(profiles);
    }

    private ProfileResponse GetProfileDto(Profile p)
    {
        return new ProfileResponse
        {
            FirstName = p.FirstName,
            LastName = p.LastName,
            Summary = p.Summary,
            Id = p.Id
        };
    }

    [HttpGet("{id}")]
    public IActionResult GetProfile(int id)
    {
        var profile = this.service.Get(id);
        return Ok(profile);
    }

    [HttpPost]
    [Consumes("application/json")]
    public async Task<IActionResult> CreateProfile([FromBody] ProfileRequest profileRequest)
    {
        var command = ProfileFactory.Get(profileRequest);
        await this.message.DispatchCommand(command);
        return Ok();
    }

    [HttpPut("{id}/languages")]
    [ProducesResponseType(203)]
    public async Task<IActionResult> AddLanguageToProfile(int id, [FromBody] CreateLanguageRequest request)
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
    public async Task<IActionResult> EditLanguageFromProfile(int id, int languageId, [FromBody] EditLanguageRequest request)
    {
        try
        {
            var command = new EditLangueFromProfileCommand
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
    public async Task<IActionResult> RemoveLanguageFromProfile(int id, int languageId)
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
    public IActionResult AddJobExperienceToProfile(int id, [FromBody] CreateJobExperienceRequest request)
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
}
