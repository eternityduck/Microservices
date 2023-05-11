using UsersService.Business.Interfaces.Providers;

namespace UsersService.Api.Middlewares;

public sealed class ServerStateHandlingMiddleware
{
    private readonly RequestDelegate _next;

    private readonly IServerStateManager _serverStateManager;

    public ServerStateHandlingMiddleware(RequestDelegate next, IServerStateManager serverStateManager)
    {
        _next = next;
        _serverStateManager = serverStateManager;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (_serverStateManager.ServerState is Business.Enums.ServerState.Slow)
        {
            await Task.Delay(TimeSpan.FromSeconds(10));
        }

        await _next(context);
    }
}
