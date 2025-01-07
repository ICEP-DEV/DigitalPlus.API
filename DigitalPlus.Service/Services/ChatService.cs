using DigitalPlus.Data;
using DigitalPlus.Data.Model;
using DigitalPlus.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalPlus.Service.Services
{
    public class ChatService : IChatService
    {
        private readonly DigitalPlusDbContext _context;

        public ChatService(DigitalPlusDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Saves a chat message to the database.
        /// </summary>
        /// <param name="message">The chat message to save.</param>
        public async Task SaveMessageAsync(ChatMessage message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message), "Message cannot be null");

            await _context.ChatMessages.AddAsync(message);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves messages for a specific module, ordered by timestamp.
        /// </summary>
        /// <param name="moduleId">The ID of the module.</param>
        /// <returns>A list of chat messages for the module.</returns>
        public async Task<List<ChatMessage>> GetMessagesByModule(int moduleId)
        {
            return await _context.ChatMessages
                .AsNoTracking() // Improves performance for read-only queries
                .Where(msg => msg.ModuleId == moduleId)
                .OrderBy(msg => msg.Timestamp)
                .ToListAsync();
        }
    }
}
