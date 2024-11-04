using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace HealthGuard.API.Hubs
{
    [Authorize]
    public class NotificationHub : Hub
    {
    }
}
