using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersService.Business.Enums;
using UsersService.Business.Interfaces.Providers;

namespace UsersService.Api.Controllers;

[ApiController]
[AllowAnonymous]
public sealed class ServerController : ControllerBase
{
    private readonly IServerStateManager _serverStateManager;

    private readonly ILogger<ServerController> _logger;

    public ServerController(IServerStateManager serverStateProvider, ILogger<ServerController> logger)
    {
        _serverStateManager = serverStateProvider;
        _logger = logger;
    }


    [HttpPost("users/untested-request")]
    public IActionResult ChangeServerStateToSlow()
    {
        _serverStateManager.ServerState = ServerState.Slow;
        _logger.LogWarning("Server state changed to Slow");
        return NoContent();
    }
}
