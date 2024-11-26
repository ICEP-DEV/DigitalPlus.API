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
            ValidateAnnouncementCreation(announcementDto);

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
                Type = announcementDto.Type,
                AnnouncementDate = announcementDto.AnnouncementDate,
                AnnouncementContent = announcementDto.AnnouncementContent,
                AnnouncementImage = announcementDto.AnnouncementImage, // Save byte array
                IsImageUpload = announcementDto.IsImageUpload,
                Frequency = announcementDto.Frequency,
                TotalOccurrences = announcementDto.TotalOccurrences,
                EndDate = announcementDto.EndDate
            };

            _context.Announcements.Add(announcement);
            await _context.SaveChangesAsync();

            // Generate series for Recurring and Drip announcements
            if (announcement.Type != AnnouncementType.OneTime)
            {
                await GenerateAnnouncementSeriesAsync(announcement);
            }

            return announcement;
        }


        private void ValidateAnnouncementCreation(AnnouncementCreateDto dto)
        {
            switch (dto.Type)
            {
                case AnnouncementType.OneTime:
                    // One-time announcement requires only date
                    break;

                case AnnouncementType.Recurring:
                    if (!dto.Frequency.HasValue)
                        throw new ArgumentException("Recurring announcements must specify a frequency.");
                    if (!dto.EndDate.HasValue && !dto.TotalOccurrences.HasValue)
                        throw new ArgumentException("Recurring announcements must have either an end date or total occurrences.");
                    break;

                case AnnouncementType.Drip:
                    if (!dto.TotalOccurrences.HasValue || dto.TotalOccurrences < 2)
                        throw new ArgumentException("Drip announcements must specify at least 2 occurrences.");
                    break;
            }
        }

        public async Task<IEnumerable<Announcement>> GenerateAnnouncementSeriesAsync(Announcement baseAnnouncement)
        {
            var series = new List<Announcement>();

            switch (baseAnnouncement.Type)
            {
                case AnnouncementType.Recurring:
                    series = GenerateRecurringAnnouncements(baseAnnouncement);
                    break;

                case AnnouncementType.Drip:
                    series = GenerateDripAnnouncements(baseAnnouncement);
                    break;
            }

            _context.Announcements.AddRange(series);
            await _context.SaveChangesAsync();

            return series;
        }

        private List<Announcement> GenerateRecurringAnnouncements(Announcement baseAnnouncement)
        {
            var series = new List<Announcement>();
            var currentDate = baseAnnouncement.AnnouncementDate;

            int occurrenceCount = 0;
            while (ShouldContinueRecurring(baseAnnouncement, currentDate, occurrenceCount))
            {
                var occurrence = new Announcement
                {
                    AnnouncementTitle =baseAnnouncement.AnnouncementTitle,
                    UserRole = baseAnnouncement.UserRole,
                    Type = AnnouncementType.Recurring,
                    AnnouncementDate = currentDate,
                    AnnouncementContent = baseAnnouncement.AnnouncementContent,
                    AnnouncementImage = baseAnnouncement.AnnouncementImage,
                    IsImageUpload = baseAnnouncement.IsImageUpload
                };

                series.Add(occurrence);

                currentDate = IncrementDate(currentDate, baseAnnouncement.Frequency.Value);
                occurrenceCount++;
            }

            return series;
        }

        private List<Announcement> GenerateDripAnnouncements(Announcement baseAnnouncement)
        {
            var series = new List<Announcement>();
            var currentDate = baseAnnouncement.AnnouncementDate;

            for (int i = 0; i < baseAnnouncement.TotalOccurrences; i++)
            {
                var occurrence = new Announcement
                {
                    UserRole = baseAnnouncement.UserRole,
                    Type = AnnouncementType.Drip,
                    AnnouncementDate = currentDate,
                    AnnouncementContent = baseAnnouncement.AnnouncementContent,
                    AnnouncementImage = baseAnnouncement.AnnouncementImage,
                    IsImageUpload = baseAnnouncement.IsImageUpload
                };

                series.Add(occurrence);
                currentDate = currentDate.AddDays(7 * 2);
            }

            return series;
        }

        private bool ShouldContinueRecurring(Announcement baseAnnouncement, DateTime currentDate, int occurrenceCount)
        {
            if (baseAnnouncement.EndDate.HasValue && currentDate > baseAnnouncement.EndDate.Value)
                return false;

            if (baseAnnouncement.TotalOccurrences.HasValue && occurrenceCount >= baseAnnouncement.TotalOccurrences.Value)
                return false;

            return true;
        }

        private DateTime IncrementDate(DateTime currentDate, AnnouncementFrequency frequency)
        {
            return frequency switch
            {
                AnnouncementFrequency.TwoWeeks => currentDate.AddDays(7 * 2),
                AnnouncementFrequency.Monthly => currentDate.AddMonths(1),
                AnnouncementFrequency.Quarterly => currentDate.AddMonths(3),
                _ => throw new ArgumentException("Invalid frequency")
            };
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

            if (announcement == null)
            {
                return false;
            }

            // If the announcement is part of a series, delete related announcements
            if (announcement.Type != AnnouncementType.OneTime)
            {
                var relatedAnnouncements = await _context.Announcements
                    .Where(a => a.AnnouncementTitle == announcement.AnnouncementTitle
                             && a.UserRole == announcement.UserRole
                             && a.Type == announcement.Type)
                    .ToListAsync();

                _context.Announcements.RemoveRange(relatedAnnouncements);
            }
            else
            {
                _context.Announcements.Remove(announcement);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Announcement> UpdateAnnouncementAsync(int announcementId, AnnouncementCreateDto announcementDto)
        {
            ValidateAnnouncementCreation(announcementDto);

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
            existingAnnouncement.Type = announcementDto.Type;
            existingAnnouncement.AnnouncementDate = announcementDto.AnnouncementDate;
            existingAnnouncement.AnnouncementContent = announcementDto.AnnouncementContent;
            existingAnnouncement.AnnouncementImage = announcementDto.AnnouncementImage;
            existingAnnouncement.IsImageUpload = announcementDto.IsImageUpload;
            existingAnnouncement.Frequency = announcementDto.Frequency;
            existingAnnouncement.TotalOccurrences = announcementDto.TotalOccurrences;
            existingAnnouncement.EndDate = announcementDto.EndDate;

            // If the type changes or is a recurring/drip announcement, regenerate the series
            if (existingAnnouncement.Type != AnnouncementType.OneTime)
            {
                // Remove existing series
                var existingSeries = await _context.Announcements
                    .Where(a => a.AnnouncementTitle == existingAnnouncement.AnnouncementTitle
                             && a.UserRole == existingAnnouncement.UserRole
                             && a.Type == existingAnnouncement.Type)
                    .ToListAsync();

                _context.Announcements.RemoveRange(existingSeries);

                // Regenerate series based on updated announcement
                await GenerateAnnouncementSeriesAsync(existingAnnouncement);
            }

            await _context.SaveChangesAsync();
            return existingAnnouncement;
        
    }
    }

           
}
