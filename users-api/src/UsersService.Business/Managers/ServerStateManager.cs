using UsersService.Business.Enums;
using UsersService.Business.Interfaces.Managers;

namespace UsersService.Business.Managers;

public sealed class ServerStateManager : IServerStateManager
{
    public ServerState ServerState { get; set; } = ServerState.Regular;
}
