using Microsoft.AspNetCore.Mvc;

namespace OGCP.Curriculum.API.DTOs.requests.Images;
public class ImageUpdateRequest
{
    [FromForm]
    public int ProfileId { get; set; }
    [FromForm]
    public IFormFile Image { get; set; }
}