using Microsoft.AspNetCore.Mvc;

namespace Backend.Utils;

public static class ResultExtensions
{
    public static ActionResult ErrorResponse(this Result result)
    {
        var body = new { errors = new { message = result.ErrorMessage } };

        return result.ErrorType switch
        {
            ErrorType.NotFound => new NotFoundObjectResult(body),
            ErrorType.Conflict => new ConflictObjectResult(body),
            _ => new BadRequestObjectResult(body)
        };
    }
}