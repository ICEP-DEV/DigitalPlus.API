using DigitalPlus.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigitalPlus.Service.Interfaces
{
    public interface IChatService
    {
        /// <summary>
        /// Saves a chat message to the database.
        /// </summary>
        /// <param name="message">The chat message to save.</param>
        Task SaveMessageAsync(ChatMessage message);

        /// <summary>
        /// Retrieves a list of messages for a specific module.
        /// </summary>
        /// <param name="moduleId">The ID of the module.</param>
        /// <returns>A list of chat messages for the specified module.</returns>
        Task<List<ChatMessage>> GetMessagesByModule(int moduleId);
    }
}
