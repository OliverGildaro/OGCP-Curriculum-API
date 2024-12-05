using ArtForAll.Shared.ErrorHandler;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OGCP.Curriculum.API.commanding;
using OGCP.Curriculum.API.commanding.commands.AddEducationDegree;
using OGCP.Curriculum.API.commanding.commands.UpdateEducationToQualifiedProfile;
using OGCP.Curriculum.API.Commanding.commands.RemoveEducationFromQualifiedProfile;
using OGCP.Curriculum.API.DTOs.requests.Education;
using OGCP.Curriculum.API.Filters;
using OGCP.Curriculum.API.POCOS.requests.Education;

namespace OGCP.Curriculum.API.Controllers;

[Route("api/v1/profiles")]
[EnableCors("AllowSpecificOrigins")]
[Produces("application/json")]
[ServiceFilter(typeof(ExceptionHandlerFilter))]
public class EducationsController : Controller
{
    private readonly Message message;
    private readonly IMapper mapper;

    public EducationsController(Message message, IMapper mapper)
    {
        this.message = message;
        this.mapper = mapper;
    }

    [HttpPut("{profileId}/educations")]
    [ProducesResponseType(203)]
    public async Task<IActionResult> AddEducationToProfileAsync(int profileId, [FromBody] AddEducationRequest request)
    {
            //AddEducationToQualifiedProfileCommand command = EducationFactory.Get(request, id);
            var command = this.mapper.Map<AddEducationToProfileCommand>(request);
            command.ProfileId = profileId;
            Result sds = await this.message.DispatchCommand(command);
            return NoContent();
    }

    [HttpPut("{profileId}/educations/{educationId}")]
    [ProducesResponseType(203)]
    public async Task<IActionResult> UpdateEducationFromProfileAsync(int profileId, int educationId, [FromBody] UpdateEducationRequest request)
    {
            var command = this.mapper.Map<UpdateEducationFromQualifiedProfileCommand>(request);
            command.ProfileId = profileId;
            command.EducationId = educationId;
            Result sds = await this.message.DispatchCommand(command);
            return NoContent();
    }

    [HttpDelete("{profileId}/educations/{educationId}")]
    [ProducesResponseType(203)]
    public async Task<IActionResult> RemoveEducationFromProfileAsync(int profileId, int educationId, [FromBody] DeleteEducationRequest request)
    {
            var command = this.mapper.Map<RemoveEducationFromProfileCommand>(request);
            command.Id = profileId;
            command.EducationId = educationId;
            Result sds = await this.message.DispatchCommand(command);
            return NoContent();
    }
}
