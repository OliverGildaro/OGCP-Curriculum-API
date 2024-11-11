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
    public class QualifiedProfileController : Controller
    {
        private readonly IProfileService<QualifiedProfile, CreateQualifiedProfileRequest> service;

        public QualifiedProfileController(IProfileService<QualifiedProfile, CreateQualifiedProfileRequest> service)
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
        public IActionResult CreateProfile(CreateQualifiedProfileRequest profileRequest)
        {
            
            this.service.Create(profileRequest);

            return View();
        }
    }
}
