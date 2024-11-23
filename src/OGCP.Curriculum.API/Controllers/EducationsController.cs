using ArtForAll.Shared.ErrorHandler;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OGCP.Curriculum.API.commanding;
using OGCP.Curriculum.API.commanding.commands.AddEducationDegree;
using OGCP.Curriculum.API.commanding.commands.UpdateEducationToQualifiedProfile;
using OGCP.Curriculum.API.factories;
using OGCP.Curriculum.API.POCOS.requests.Education;

namespace OGCP.Curriculum.API.Controllers
{
    [Route("api/v1/profiles")]
    [EnableCors("AllowSpecificOrigins")]
    [Produces("application/json")]
    public class EducationsController : Controller
    {
        private readonly Message message;
        private readonly IMapper mapper;

        public EducationsController(Message message, IMapper mapper)
        {
            this.message = message;
            this.mapper = mapper;
        }

        [HttpPut("{id}/educations")]
        [ProducesResponseType(203)]
        public async Task<IActionResult> AddEducationToProfile(int profileId, [FromBody] AddEducationRequest request)
        {
            try
            {
                //AddEducationToQualifiedProfileCommand command = EducationFactory.Get(request, id);
                var command = this.mapper.Map<AddEducationToQualifiedProfileCommand>(request);
                command.ProfileId = profileId;
                Result sds = await this.message.DispatchCommand(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut("{profileId}/educations/{educationId}")]
        [ProducesResponseType(203)]
        public async Task<IActionResult> UpdateEducationFromProfile(int profileId, int educationId, [FromBody] UpdateEducationRequest request)
        {
            try
            {
                var command = this.mapper.Map<UpdateEducationFromQualifiedProfileCommand>(request);
                command.ProfileId = profileId;
                command.EducationId = educationId;
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
