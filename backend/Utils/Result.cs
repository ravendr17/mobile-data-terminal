namespace Backend.Utils;

public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Data { get; }
    public string? ErrorMessage { get; }
    public ErrorType ErrorType { get; }

    private Result(bool isSuccess, T? data, string? errorMessage, ErrorType errorType)
    {
        IsSuccess = isSuccess;
        Data = data;
        ErrorMessage = errorMessage;
        ErrorType = errorType;
    }

    public static Result<T> Success(T data)
    {
        return new Result<T>(true, data, null, ErrorType.None);
    }

    public static Result<T> NotFound(string errorMessage)
    {
        return new Result<T>(false, default, errorMessage, ErrorType.NotFound);
    }

    public static Result<T> Conflict(string errorMessage)
    {
        return new Result<T>(false, default, errorMessage, ErrorType.Conflict);
    }

    public static Result<T> BadRequest(string errorMessage)
    {
        return new Result<T>(false, default, errorMessage, ErrorType.BadRequest);
    } 
}