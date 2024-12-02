using Microsoft.AspNetCore.SignalR;

namespace DigitalPlus.API.hubs
{
    public class CommunicationHub : Hub
    {
        // Method for sending direct messages from one user to another
        public async Task SendDirectMessage(string recipientUserId, string senderUserId, string senderName, string message, string timestamp)
        {
            try
            {
                if (string.IsNullOrEmpty(recipientUserId))
                {
                    Console.WriteLine("Error: recipientUserId is null or empty");
                    return;
                }

                await Clients.User(recipientUserId).SendAsync("ReceiveDirectMessage", senderUserId, senderName, message, timestamp);
                Console.WriteLine($"Message sent from {senderUserId} to {recipientUserId}: {message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SendDirectMessage: {ex.Message}");
            }
        }

        // Method for sending files from one user to another using base64 encoding
        public async Task SendFileMessage(string recipientUserId, string senderUserId, string senderName, string base64File, string fileType, string timestamp, string fileName)
        {
            try
            {
                if (string.IsNullOrEmpty(recipientUserId))
                {
                    Console.WriteLine("Error: recipientUserId is null or empty");
                    return;
                }

                if (string.IsNullOrEmpty(base64File) || string.IsNullOrEmpty(fileType) || string.IsNullOrEmpty(fileName))
                {
                    Console.WriteLine("Error: File content, file type, or file name is invalid");
                    return;
                }

                await Clients.User(recipientUserId).SendAsync("ReceiveFileMessage", senderUserId, senderName, base64File, fileType, timestamp, fileName);
                Console.WriteLine($"File sent from {senderUserId} to {recipientUserId}: {fileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SendFileMessage: {ex.Message}");
            }
        }

        // Method for editing a message
        public async Task EditMessage(string messageId, string newContent, string userId)
        {
            try
            {
                // Validate inputs
                if (string.IsNullOrEmpty(messageId) || string.IsNullOrEmpty(newContent) || string.IsNullOrEmpty(userId))
                {
                    Console.WriteLine("Error: Invalid parameters for EditMessage");
                    return;
                }

                // Notify clients that the message was edited
                await Clients.User(userId).SendAsync("MessageEdited", messageId, newContent);
                Console.WriteLine($"Message {messageId} edited by {userId} with new content: {newContent}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in EditMessage: {ex.Message}");
            }
        }


        // Method for deleting a message
        public async Task DeleteMessage(string messageId)
        {
            try
            {
                // Notify all clients that the message was deleted
                await Clients.All.SendAsync("ReceiveDeleteMessage", messageId);
                Console.WriteLine($"Message with ID {messageId} has been deleted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteMessage: {ex.Message}");
            }
        }

        // Override method called when a user connects
        public override async Task OnConnectedAsync()
        {
            try
            {
                string userId = Context.GetHttpContext()?.Request.Query["userId"].ToString();

                if (!string.IsNullOrEmpty(userId))
                {
                    Console.WriteLine($"User connected with UserIdentifier: {userId}");
                }
                else
                {
                    Console.WriteLine("User connected without a specific UserIdentifier.");
                }

                await Clients.Caller.SendAsync("ReceiveDirectMessage", "Server", "Server", "Welcome to the chat!", DateTime.Now.ToString("hh:mm:ss tt"));
                Console.WriteLine($"Client connected with ConnectionId: {Context.ConnectionId} and received a welcome message.");

                await base.OnConnectedAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in OnConnectedAsync: {ex.Message}");
            }
        }
    }
}
