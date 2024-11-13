using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.Controllers;

[Route("api/v1/profiles")]
[EnableCors("AllowSpecificOrigins")]
[Produces("application/json")]
public class QualifiedProfileController : Controller
{
    private readonly IQualifiedProfileService service;

    public QualifiedProfileController(IQualifiedProfileService service)
    {
        this.service = service;
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
    public IActionResult CreateProfile([FromBody] ProfileRequest profileRequest)
    {
        if (profileRequest.RequestType.Equals(ProfileEnum.CreateGeneralProfileRequest))
        {
            this.service.Create((CreateGeneralProfileRequest)profileRequest);
        } else if(profileRequest.RequestType.Equals(ProfileEnum.CreateQualifiedProfileRequest))
        {
            this.service.Create((CreateQualifiedProfileRequest)profileRequest);
        } else if(profileRequest.RequestType.Equals(ProfileEnum.CreateStudentProfileRequest))
        {
            this.service.Create((CreateStudentProfileRequest)profileRequest);
        }
        return Ok();
    }

    [HttpPut("{id}/languages")]
    [ProducesResponseType(203)]
    public IActionResult AddLanguageToProfile(int id, [FromBody] CreateLanguageRequest request)
    {
        try
        {
            this.service.AddLanguage(id, request);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }

    [HttpPut("{id}/educations")]
    [ProducesResponseType(203)]
    public IActionResult AddEducationToProfile(int id, [FromBody] CreateEducationRequest request)
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
}
