using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.Controllers;

[Route("api/v1/profiles/students")]
[EnableCors("AllowSpecificOrigins")]
[Produces("application/json")]
public class StudentController : Controller
{
    private readonly IStudentProfileService service;

    public StudentController(IStudentProfileService service)
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
    public IActionResult CreateProfile([FromBody] CreateStudentProfileRequest profileRequest)
    {
        this.service.Create(profileRequest);
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
    public IActionResult AddEducationToProfile(int id, [FromBody] CreateResearchEducationRequest request)
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
