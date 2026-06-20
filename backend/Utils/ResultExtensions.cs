using Microsoft.AspNetCore.Mvc;

namespace Backend.Utils;

public static class ResultExtensions
{
    public static ActionResult ErrorResponse<T>(this Result<T> result)
    {
        var body = new { message = result.ErrorMessage};

        return result.ErrorType switch
        {
            ErrorType.NotFound => new NotFoundObjectResult(body),
            ErrorType.Conflict => new ConflictObjectResult(body),
            _ => new BadRequestObjectResult(body)
        };
    }
}