using Microsoft.AspNetCore.Mvc;
using OGCP.Curriculums.API.Envelopes;
using System.Net;

namespace OGCP.Curriculum.API.Controllers;
//SOURCES
//https://enterprisecraftsmanship.com
//http://bit.ly/vlad-updates
//[ApiController]
public class ApplicationController : Controller
{

    public ApplicationController()
    {
    }
    protected new IActionResult Ok(object result = null)
    {
        return new EnvelopeResult(Envelope.Ok(result), HttpStatusCode.OK);
    }

    //protected IActionResult NotFound(Error error, string invalidField = null)
    //{
    //    return new EnvelopeResult(Envelope.Error(error, invalidField), HttpStatusCode.NotFound);
    //}

    //protected IActionResult Error(Error error, string invalidField = null)
    //{
    //    return new EnvelopeResult(Envelope.Error(error, invalidField), HttpStatusCode.BadRequest);
    //}

    //protected IActionResult FromResult<T>(Result<T, Error> result)
    //    where T : class
    //{
    //    if (result.IsSucces)
    //        return Ok();

    //    return Error(result.Error);
    //}

    protected static byte[] ConvertIFormFileToByteArrayAsync(IFormFile file)
    {
        if (file is null)
        {
            return null;
        }
        using (var memoryStream = new MemoryStream())
        {
            file.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }


}
