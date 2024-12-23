using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OGCP.Curriculum.API.commanding;
using OGCP.Curriculum.API.DTOs.requests.Images;
using OGCP.Curriculums.Commanding.ProfileCommandImages.AddImage;
using OGCP.Curriculums.Commanding.ProfileCommandImages.UpdateImage;

namespace OGCP.Curriculum.API.Controllers;

[Route("api/v1/profiles")]
[EnableCors("AllowSpecificOrigins")]
public class ImagesController : ApplicationController
{
    private readonly Message messages;

    public ImagesController(Message messages)
    {
        this.messages = messages;
    }

    [HttpPost("{profileId}/images")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
    public async Task<IActionResult> AddImage(int profileId, [FromForm] ImageAddRequest request)
    {
        try
        {

            var addImageCmd = new AddImageCommand
            {
                ProfileId = profileId,
                ImageContent = ConvertIFormFileToByteArrayAsync(request.Image),
                ContentType = request.Image.ContentType,
                FileName = request.Image.FileName
            };

            var imageUploadResult = await this.messages.DispatchCommand(addImageCmd);

            if (imageUploadResult.IsFailure)
            {
                return BadRequest();
            }

            return StatusCode(StatusCodes.Status201Created, new { id = imageUploadResult.Id });
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }

    [HttpPut("{id}")]//This is for a CRUD based API
    [Consumes("multipart/form-data")]
    [ProducesResponseType(203)]
    public async Task<IActionResult> UpdateImage([FromRoute] int id, [FromForm] ImageUpdateRequest request)
    {
        try
        {
            var updateImageCmd = new UpdateImageCommand
            {
                Id = id,
                ProfileId = request.ProfileId,
                ImageContent = ConvertIFormFileToByteArrayAsync(request.Image),
                ContentType = request.Image.ContentType,
                FileName = request.Image.FileName
            };

            var imageUpdatedResult = await this.messages.DispatchCommand(updateImageCmd);

            if (imageUpdatedResult.IsFailure)
            {
                return BadRequest();
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }
}
