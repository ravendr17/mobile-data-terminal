using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MobileDataTerminal.Api.Exceptions;

public sealed class GlobalExceptionHandler(
    IProblemDetailsService problemDetailsService,
    ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Unhandled exception occurred");

        var (statusCode, message) = exception switch
        {
            ApplicationException => 
                (StatusCodes.Status400BadRequest, exception.Message),
            ConflictException => 
                (StatusCodes.Status409Conflict, exception.Message),
            ResourceNotFoundException => 
                (StatusCodes.Status404NotFound, exception.Message),
            UnauthorizedException => 
                (StatusCodes.Status401Unauthorized, exception.Message),
            BadHttpRequestException => 
                (StatusCodes.Status400BadRequest, "Invalid/malformed request body."),
            _ => (StatusCodes.Status500InternalServerError, "An unexpected server error occured.")
        };

        httpContext.Response.StatusCode = statusCode;

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Type = exception.GetType().Name,
                Title = "An error occurred",
                Detail = message
            }
        });
    }
}