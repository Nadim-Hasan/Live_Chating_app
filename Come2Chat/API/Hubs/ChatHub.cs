using API.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ChatService _chatService;

        public ChatHub(ChatService chatService)
        {

            _chatService = chatService;

        }

        public override async Task OnConnectedAsync()
        {
            // Add the connected user to the "Come2Chat" group
            await Groups.AddToGroupAsync(Context.ConnectionId, "Come2Chat");

            // Send a "UserConnected" message to the caller (connected user)
            await Clients.Caller.SendAsync("UserConnected");
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // Perform default disconnection handling
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Come2Chat");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
