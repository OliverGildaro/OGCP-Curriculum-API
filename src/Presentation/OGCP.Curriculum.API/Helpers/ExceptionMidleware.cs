using System.Net.Mime;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using OGCP.Curriculum.API.commanding;

namespace OGCP.Curriculum.API.Helpers;
public class ExceptionMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogInformation("MY_TRACKINGS: Exception was catched in exc eption middleware class");
            _logger.LogInformation(string.Format("MY_TRACKINGS message: {0}", ex.Message));
            _logger.LogInformation(string.Format("MY_TRACKINGS stack trace: {0}", ex.StackTrace));
            
            await HandleCustomExceptionResponseAsync(context, ex);
        }
    }

    private async Task HandleCustomExceptionResponseAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = MediaTypeNames.Application.Json;
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new
        {
            StatusCode = (int)context.Response.StatusCode,
            Message = "An expected error ocurred, catched in the midleware exception",
            StackTrace = ex.StackTrace?.ToString() // Avoid exposing internal details in production
        };
        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        var json = JsonSerializer.Serialize(response, options);
        await context.Response.WriteAsync(json);
    }
}
