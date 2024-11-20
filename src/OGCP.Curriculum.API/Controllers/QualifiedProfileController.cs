using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OGCP.Curriculum.API.commanding;
using OGCP.Curriculum.API.commanding.AddLanguageToProfile;
using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.dtos.requests;
using OGCP.Curriculum.API.factories;
using OGCP.Curriculum.API.services.interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
    public IActionResult GetProfiles()
    {
        var profiles = this.service.Get();
        return Ok(profiles);
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
        await this.message.DIspatch(command);
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

            await this.message.DIspatch(command);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }

    [HttpPut("{id}/educations")]
    [ProducesResponseType(203)]
    public IActionResult AddEducationToProfile(int id, [FromBody] CreateDegreeEducationRequest request)
    {
        try
        {
            this.service.AddEducation(id, request);

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

            this.service.AddJobExperience(id, request);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }
}
