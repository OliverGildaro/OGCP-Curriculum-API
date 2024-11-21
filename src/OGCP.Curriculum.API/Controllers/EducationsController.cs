using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.ErrorHandler;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OGCP.Curriculum.API.commanding;
using OGCP.Curriculum.API.factories;
using OGCP.Curriculum.API.POCOS.requests;

namespace OGCP.Curriculum.API.Controllers
{
    [Route("api/v1/profiles")]
    [EnableCors("AllowSpecificOrigins")]
    [Produces("application/json")]
    public class EducationsController : Controller
    {
        private readonly Message message;

        public EducationsController(Message message)
        {
            this.message = message;
        }

        [HttpPut("{id}/educations")]
        [ProducesResponseType(203)]
        public async Task<IActionResult> AddEducationToProfile(int id, [FromBody] AddEducationRequest request)
        {
            try
            {
                ICommand command = EducationFactory.Get(request, id);
                Result sds = await this.message.DispatchCommand(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
