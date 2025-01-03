using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using OGCP.Curriculums.Ports;

namespace OGCP.Curriculum.API.Filters;

public class ExceptionHandlerFilter : IExceptionFilter
{
    private readonly IApplicationInsights insights;

    public ExceptionHandlerFilter(IApplicationInsights insights)
    {
        this.insights = insights;
    }
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
        insights.LogInformation("MY_TRACKINGS: Exception was catched in exception handler filter class");
        insights.LogInformation(string.Format("MY_TRACKINGS message: {0}", context.Exception.Message));
        insights.LogInformation(string.Format("MY_TRACKINGS stack trace: {0}", context.Exception.StackTrace));
        // Create JSON response
        context.Result = new JsonResult(response)
        {
            StatusCode = (int)statusCode
        };

        context.ExceptionHandled = true; // Mark the exception as handled
    }
}
