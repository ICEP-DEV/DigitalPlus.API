using DigitalPlus.Service.Interfaces;
using DigitalPlus.Data.Model;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace DigitalPlus.API.Hubs
{
    public class ChatBoardHub : Hub
    {
        private readonly IChatService _chatService;

        public ChatBoardHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        // Method to send a text message to everyone in the module
        public async Task SendMessageToModule(int moduleId, string user, string message, string role)
        {
            string timestamp = DateTime.Now.ToString("h:mm tt");

            // Create a new ChatMessage object
            var chatMessage = new ChatMessage
            {
                ModuleId = moduleId,
                Sender = user,
                Role = role,
                Message = message,
                Timestamp = DateTime.Now
            };

            // Save the message to the database
            await _chatService.SaveMessageAsync(chatMessage);

            // Broadcast the text message to all clients
            await Clients.All.SendAsync("ReceiveMessage", moduleId, user, message, null, null, timestamp, role);
        }

        // Method to send a file message to everyone in the module
        public async Task SendFileToModule(int moduleId, string user, string fileName, string fileUrl, string role)
        {
            string timestamp = DateTime.Now.ToString("h:mm tt");

            // Create a new ChatMessage object for the file
            var chatMessage = new ChatMessage
            {
                ModuleId = moduleId,
                Sender = user,
                Role = role,
                FileName = fileName,
                FileUrl = fileUrl,
                Timestamp = DateTime.Now
            };

            // Save the file message to the database
            await _chatService.SaveMessageAsync(chatMessage);

            // Broadcast the file message to all clients
            await Clients.All.SendAsync("ReceiveMessage", moduleId, user, null, fileName, fileUrl, timestamp, role);
        }

        // Method for editing a message
        public async Task EditMessage(int moduleId, string messageId, string newContent)
        {
            // Broadcast the edited message to all clients
            await Clients.All.SendAsync("MessageEdited", moduleId, messageId, newContent);
        }

        // Method for deleting a message
        public async Task DeleteMessage(int moduleId, string messageId)
        {
            // Broadcast the deletion to all clients
            await Clients.All.SendAsync("MessageDeleted", moduleId, messageId);
        }

        // Override method called when a user connects
        public override async Task OnConnectedAsync()
        {
            string connectionId = Context.ConnectionId;
            await Clients.Caller.SendAsync("ReceiveMessage", "System", "Welcome to the module chat!", null, null, null, DateTime.Now.ToString("h:mm tt"), "system");
            await base.OnConnectedAsync();
        }
    }
}
