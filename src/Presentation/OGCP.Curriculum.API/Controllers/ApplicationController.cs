using ArtForAll.Shared.Contracts.DDD;
using ArtForAll.Shared.ErrorHandler;
using Microsoft.AspNetCore.Mvc;
using OGCP.Curriculums.API.Envelopes;
using OGCP.Curriculums.Core.DomainModel;
using System.Net;

namespace OGCP.Curriculum.API.Controllers;
//SOURCES
//https://enterprisecraftsmanship.com
//http://bit.ly/vlad-updates
[ApiController]
public class ApplicationController : ControllerBase
{
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
}
