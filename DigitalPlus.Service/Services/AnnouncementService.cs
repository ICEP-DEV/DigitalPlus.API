using DigitalPlus.Data;
using DigitalPlus.Data.Dto;
using DigitalPlus.Data.Model;
using DigitalPlus.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
//using SixLabors.ImageSharp;
//using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
//using Image = SixLabors.ImageSharp.Image;

namespace DigitalPlus.Service.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly DigitalPlusDbContext _context;


        public AnnouncementService(DigitalPlusDbContext context)
        {
            _context = context;
        }

        public async Task<Announcement> CreateAnnouncementAsync(AnnouncementCreateDto announcementDto)
        {

            // Convert the uploaded image file to a byte array if it exists
            if (announcementDto.AnnouncementImageFile != null)
            {
                using (var ms = new MemoryStream())
                {
                    await announcementDto.AnnouncementImageFile.CopyToAsync(ms);
                    announcementDto.AnnouncementImage = ms.ToArray();
                }
            }

            var announcement = new Announcement
            {
                AnnouncementTitle = announcementDto.AnnouncementTitle,
                UserRole = announcementDto.UserRole,
                AnnouncementDate = announcementDto.AnnouncementDate,
                AnnouncementContent = announcementDto.AnnouncementContent,
                AnnouncementImage = announcementDto.AnnouncementImage, // Save byte array
                EndDate = announcementDto.EndDate
            };

            _context.Announcements.Add(announcement);
            await _context.SaveChangesAsync();

            return announcement;
        }




        public async Task<IEnumerable<Announcement>> GetAnnouncementsForUserRoleAsync(AnnouncementUserRole userRole)
        {
            return await _context.Announcements
             .Where(a => a.UserRole == userRole || a.UserRole == AnnouncementUserRole.Both)
             .ToListAsync();
        }

        public async Task<bool> DeleteAnnouncementAsync(int announcementId)
        {
            var announcement = await _context.Announcements
                 .FirstOrDefaultAsync(a => a.AnnouncementId == announcementId);

            _context.Announcements.Remove(announcement);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Announcement> UpdateAnnouncementAsync(int announcementId, AnnouncementCreateDto announcementDto)
        {


            var existingAnnouncement = await _context.Announcements
                .FirstOrDefaultAsync(a => a.AnnouncementId == announcementId);

            if (existingAnnouncement == null)
            {
                throw new ArgumentException($"Announcement with ID {announcementId} not found.");
            }

            // Handle image upload
            if (announcementDto.AnnouncementImageFile != null)
            {
                using (var ms = new MemoryStream())
                {
                    await announcementDto.AnnouncementImageFile.CopyToAsync(ms);
                    announcementDto.AnnouncementImage = ms.ToArray();
                }
            }

            // Update the existing announcement properties
            existingAnnouncement.AnnouncementTitle = announcementDto.AnnouncementTitle;
            existingAnnouncement.UserRole = announcementDto.UserRole;
            existingAnnouncement.AnnouncementDate = announcementDto.AnnouncementDate;
            existingAnnouncement.AnnouncementContent = announcementDto.AnnouncementContent;
            existingAnnouncement.AnnouncementImage = announcementDto.AnnouncementImage;
            existingAnnouncement.EndDate = announcementDto.EndDate;

            await _context.SaveChangesAsync();
            return existingAnnouncement;

        }
    }


}
