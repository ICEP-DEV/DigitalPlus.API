using DigitalPlus.Data.Dto;
using DigitalPlus.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Service.Interfaces
{
    public interface IAnnouncementService
    {
        Task<Announcement> CreateAnnouncementAsync(AnnouncementCreateDto announcementDto);
        Task<IEnumerable<Announcement>> GetAnnouncementsForUserRoleAsync(AnnouncementUserRole userRole);
        Task<bool> DeleteAnnouncementAsync(int announcementId);
        Task<Announcement> UpdateAnnouncementAsync(int announcementId, AnnouncementCreateDto announcementDto);

    }
}
