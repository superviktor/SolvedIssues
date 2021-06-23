using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace SignalRTest.WebApp.Hubs
{
    //[Authorize]
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Client(Context.ConnectionId).SendAsync("SystemMessage", "Connected");
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Clients.Group("groupName").SendAsync("SystemMessage", "Disconnected");
        }
    }
}
