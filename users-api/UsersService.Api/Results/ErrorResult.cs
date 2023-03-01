namespace UsersService.Api.Results;

public sealed record ErrorResult(string Message)
{
    public static ErrorResult Create(string message)
        => new(message);
}
