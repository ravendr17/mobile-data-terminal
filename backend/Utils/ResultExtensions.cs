using Microsoft.AspNetCore.Mvc;

namespace Backend.Utils;

public static class ResultExtensions
{
    public static ActionResult ErrorResponse<T>(this Result<T> result)
    {
        return result.ErrorType switch
        {
            ErrorType.NotFound => new NotFoundObjectResult(result.ErrorMessage),
            ErrorType.Conflict => new ConflictObjectResult(result.ErrorMessage),
            _ => new BadRequestObjectResult(result.ErrorMessage)
        };
    }
}