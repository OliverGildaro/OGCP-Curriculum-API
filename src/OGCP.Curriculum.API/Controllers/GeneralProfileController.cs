using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.Controllers;
//This is an ASP web api MVC application
//MVC is an architecture pattern for implementing user interfaces
//controller depend on the view and the model
//Allows separation of concerns and testability
//The representation of the data => json or xml content are the view
[Route("api/v1/profiles/general")]
[EnableCors("AllowSpecificOrigins")]
[Produces("application/json")]
public class GeneralProfileController : Controller
{
    private readonly IGeneralProfileService service;

    public GeneralProfileController(IGeneralProfileService service)
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
    public IActionResult CreateProfile([FromBody] CreateGeneralProfileRequest profileRequest)
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
}
