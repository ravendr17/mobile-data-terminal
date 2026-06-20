namespace Backend.Utils;

public class Result
{
    public bool IsSuccess { get; }
    public string? ErrorMessage { get; }
    public ErrorType ErrorType { get; }

    protected Result(bool isSuccess, string? errorMessage, ErrorType errorType)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
        ErrorType = errorType;
    }

    public static Result Success()
    {
        return new Result(true, null, ErrorType.None);
    }

    public static Result NotFound(string errorMessage)
    {
        return new Result(false, errorMessage, ErrorType.NotFound);
    }

    public static Result Conflict(string errorMessage)
    {
        return new Result(false, errorMessage, ErrorType.Conflict);
    }

    public static Result BadRequest(string errorMessage)
    {
        return new Result(false, errorMessage, ErrorType.BadRequest);
    }
}

public class Result<T>: Result
{
    public T? Data { get; }

    private Result(bool isSuccess, T? data, string? errorMessage, ErrorType errorType)
        : base(isSuccess, errorMessage, errorType)
    {
        Data = data;
    }

    public static Result<T> Success(T data)
    {
        return new Result<T>(true, data, null, ErrorType.None);
    }

    public static new Result<T> NotFound(string errorMessage)
    {
        return new Result<T>(false, default, errorMessage, ErrorType.NotFound);
    }

    public static new Result<T> Conflict(string errorMessage)
    {
        return new Result<T>(false, default, errorMessage, ErrorType.Conflict);
    }

    public static new Result<T> BadRequest(string errorMessage)
    {
        return new Result<T>(false, default, errorMessage, ErrorType.BadRequest);
    } 
}