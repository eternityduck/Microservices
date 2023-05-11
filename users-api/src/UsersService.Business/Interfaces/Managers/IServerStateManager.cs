using UsersService.Business.Enums;

namespace UsersService.Business.Interfaces.Providers;

public interface IServerStateManager
{
    ServerState ServerState { get; set; }
}
