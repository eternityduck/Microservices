using UsersService.Api.Results;
using UsersService.Business.Exceptions;

namespace UsersService.Api.Middlewares;

public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    private readonly ILoggerFactory _loggerFactory;

    private ILogger Logger => _loggerFactory.CreateLogger<ExceptionHandlingMiddleware>();

    public ExceptionHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        _loggerFactory = loggerFactory;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            Logger.LogError("An exception occured: {Exception}", ex);

            var (message, statusCode) = ex switch
            {
                EntityNotFoundException => (ex.Message, StatusCodes.Status404NotFound),
                UnprocessableEntityException => (ex.Message, StatusCodes.Status422UnprocessableEntity),
                _ => ("An unknown exception occured on the server", StatusCodes.Status500InternalServerError)
            };

            var result = ErrorResult.Create(message);
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(result);
        }
    }
}
