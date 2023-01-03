using Microsoft.AspNetCore.SignalR;

namespace BulletinBoard.Data.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task Send(string message, string userName)
        {
            await Clients.Others.SendAsync("Receive", message, userName);
        }
    }
}
