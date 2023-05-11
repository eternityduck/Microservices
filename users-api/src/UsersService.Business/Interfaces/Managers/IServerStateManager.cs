using UsersService.Business.Enums;

namespace UsersService.Business.Interfaces.Managers;

public interface IServerStateManager
{
    ServerState ServerState { get; set; }
}
