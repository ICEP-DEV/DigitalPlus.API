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
    public class MenteeNotificationService : IMenteeNotificationService
    {
        private readonly DigitalPlusDbContext _context;
        public MenteeNotificationService(DigitalPlusDbContext context)
        {
            _context = context;
        }

        public async Task AddNotificationAsync(MenteeNotification notification)
        {
            _context.MenteeNotifications.Add(notification);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MenteeNotification>> GetAllNotificationsAsync()
        {
            return await _context.MenteeNotifications.OrderByDescending(n => n.CreatedAt).ToListAsync();
        }


        public async Task MarkAsReadAsync(string notificationId)
        {
            var notification = await _context.MenteeNotifications.FindAsync(notificationId);
            if (notification != null)
            {
                notification.IsRead = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
