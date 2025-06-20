using DigitalPlus.Data;
using DigitalPlus.Data.Model;
using DigitalPlus.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Service.Services
{
    public class MentorNotificationService : IMentorNotificationService
    {
        private readonly DigitalPlusDbContext _context;

        public MentorNotificationService(DigitalPlusDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(MentorNotification notification)
        {
            _context.MentorNotifications.Add(notification);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MentorNotification>> GetAllAsync()
        {
            return await _context.MentorNotifications.OrderByDescending(n => n.CreatedAt).ToListAsync();
        }

        public async Task MarkAsReadAsync(int id)
        {
            var notification = await _context.MentorNotifications.FindAsync(id);
            if (notification != null)
            {
                notification.IsRead = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
