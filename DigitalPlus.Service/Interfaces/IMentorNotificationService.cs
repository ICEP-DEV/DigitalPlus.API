using DigitalPlus.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Service.Interfaces
{
    public interface IMentorNotificationService
    {
        Task<IEnumerable<MentorNotification>> GetAllAsync();
        Task AddAsync(MentorNotification notification);
        Task MarkAsReadAsync(int id);
    }
}
