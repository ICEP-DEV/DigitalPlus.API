﻿using DigitalPlus.Data;
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

        public async Task<Announcement> CreateAnnouncementAsync(Announcement announcement)
        {

            // Convert the uploaded image file to a byte array if it exists
            /* if (announcement1.Image != null)
             {
                 using (var ms = new MemoryStream())
                 {
                     await announcement1.Image.CopyToAsync(ms);
                     announcement1.AnnouncementImage = ms.ToArray();
                 }
             }

             var announcement = new Announcement
             {
                 AnnouncementTitle = announcement1.AnnouncementTitle,
                 UserRole = announcement1.UserRole,
                 AnnouncementDate = announcement1.AnnouncementDate,
                 AnnouncementContent = announcement1.AnnouncementContent,
                 AnnouncementImage = announcement1.AnnouncementImage, 
                 EndDate = announcement1.EndDate
             };*/

             await _context.Announcements.AddAsync(announcement);
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

        public async Task<Announcement> UpdateAnnouncementAsync(int announcementId, Announcement announcement)
        {


            var existingAnnouncement = await _context.Announcements
                .FirstOrDefaultAsync(a => a.AnnouncementId == announcementId);

            if (existingAnnouncement == null)
            {
                throw new ArgumentException($"Announcement with ID {announcementId} not found.");
            }

            // Handle image upload
            if (announcement.Image != null)
            {
                using (var ms = new MemoryStream())
                {
                    await announcement.Image.CopyToAsync(ms);
                    announcement.AnnouncementImage = ms.ToArray();
                }
            }

            // Update the existing announcement properties
            existingAnnouncement.AnnouncementTitle = announcement.AnnouncementTitle;
            existingAnnouncement.UserRole = announcement.UserRole;
            existingAnnouncement.AnnouncementDate = announcement.AnnouncementDate;
            existingAnnouncement.AnnouncementContent = announcement.AnnouncementContent;
            existingAnnouncement.AnnouncementImage = announcement.AnnouncementImage;
            existingAnnouncement.EndDate = announcement.EndDate;

            await _context.SaveChangesAsync();
            return existingAnnouncement;

        }
    }


}
