using DigitalPlus.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Service.Interfaces
{
    public interface IMenteeNotificationService
    {
        Task<IEnumerable<MenteeNotification>> GetAllNotificationsAsync();
        Task AddNotificationAsync(MenteeNotification notification);
        Task MarkAsReadAsync(string notificationId);
    }
}
