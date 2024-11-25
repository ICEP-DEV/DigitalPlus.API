using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace DigitalPlus.API.hubs
{
    public class ChatBoardHub : Hub
    {
        // Method to send a text message to everyone in the module
        public async Task SendMessageToModule(string moduleId, string user, string message)
        {
            string timestamp = DateTime.Now.ToString("h:mm tt");

            // Broadcast the text message to all clients, filtering by module ID
            await Clients.All.SendAsync("ReceiveMessage", moduleId, user, message, null, null, timestamp);
        }

        // Method to send a file message to everyone in the module
        public async Task SendFileToModule(string moduleId, string user, string fileName, string fileUrl)
        {
            string timestamp = DateTime.Now.ToString("h:mm tt");

            // Broadcast the file message to all clients, filtering by module ID
            await Clients.All.SendAsync("ReceiveMessage", moduleId, user, null, fileName, fileUrl, timestamp);
        }

        // Method for editing a message
        public async Task EditMessage(string moduleId, string messageId, string newContent)
        {
            // Broadcast the edited message to all clients, filtering by module ID
            await Clients.All.SendAsync("MessageEdited", moduleId, messageId, newContent);
        }

        // Method for deleting a message
        public async Task DeleteMessage(string moduleId, string messageId)
        {
            // Broadcast the deletion to all clients, filtering by module ID
            await Clients.All.SendAsync("MessageDeleted", moduleId, messageId);
        }

        // Override method called when a user connects
        public override async Task OnConnectedAsync()
        {
            string connectionId = Context.ConnectionId;
            await Clients.Caller.SendAsync("ReceiveMessage", "System", "Welcome to the module chat!", null, null, null, DateTime.Now.ToString("h:mm tt"));
            await base.OnConnectedAsync();
        }
    }
}
