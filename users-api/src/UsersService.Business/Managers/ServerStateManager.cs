using UsersService.Business.Enums;
using UsersService.Business.Interfaces.Providers;

namespace UsersService.Business.Providers;

public sealed class ServerStateManager : IServerStateManager
{
    public ServerState ServerState { get; set; } = ServerState.Regular;
}
