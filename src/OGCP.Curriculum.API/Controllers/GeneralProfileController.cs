using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.models;
using OGCP.Curriculum.API.services;

namespace OGCP.Curriculum.API.Controllers
{
    [Route("api/v1/profiles/general")]
    [EnableCors("AllowSpecificOrigins")]
    [Produces("application/json")]
    public class GeneralController : Controller
    {
        private readonly IProfileService<GeneralProfile, CreateGeneralProfileRequest> service;

        public GeneralController(IProfileService<GeneralProfile, CreateGeneralProfileRequest> service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult GetProfiles()
        {
            var asas = this.service.Get();
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateProfile(CreateGeneralProfileRequest profileRequest)
        {
            
            this.service.Create(profileRequest);

            return View();
        }
    }
}
