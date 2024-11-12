using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.factories;
using OGCP.Curriculum.API.helpers;
using OGCP.Curriculum.API.models;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.Controllers
{
    [Route("api/v1/profiles/general")]
    [EnableCors("AllowSpecificOrigins")]
    [Produces("application/json")]
    public class GeneralController : Controller
    {
        private readonly IProfileService service;

        public GeneralController(IProfileService service)
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
            } else
            {
                this.service.Create((CreateQualifiedProfileRequest)profileRequest);

            }
            return Ok();
        }
    }
}
