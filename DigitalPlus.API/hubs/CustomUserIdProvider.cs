using Microsoft.AspNetCore.SignalR;

namespace DigitalPlus.API.hubs
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            return connection.GetHttpContext()?.Request.Query["userId"];
        }
    }
}
