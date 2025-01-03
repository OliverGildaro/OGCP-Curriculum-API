﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace OGCP.Curriculum.API.Filters;

public class ExceptionHandlerFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        // Determine the response details based on the exception
        var statusCode = HttpStatusCode.InternalServerError;
        var message = "An unexpected error occurred.";

        if (context.Exception is ArgumentException)
        {
            statusCode = HttpStatusCode.BadRequest;
            message = "Invalid arguments provided.";
        }
        else if (context.Exception is UnauthorizedAccessException)
        {
            statusCode = HttpStatusCode.Unauthorized;
            message = "Unauthorized access.";
        }

        var response = new
        {
            StatusCode = (int)statusCode,
            Message = message,
            Details = context.Exception.Message // Avoid exposing internal details in production
        };

        // Create JSON response
        context.Result = new JsonResult(response)
        {
            StatusCode = (int)statusCode
        };

        context.ExceptionHandled = true; // Mark the exception as handled
    }
}
