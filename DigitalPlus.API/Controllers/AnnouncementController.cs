using DigitalPlus.Data.Dto;
using DigitalPlus.Data.Model;
using DigitalPlus.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DigitalPlus.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AnnouncementController : ControllerBase
    {
        private readonly IAnnouncementService _announcementService;

        public AnnouncementController(
           IAnnouncementService announcementService
          )
        {
            _announcementService = announcementService;

        }

        [HttpPost]
        public async Task<IActionResult> CreateAnnouncement([FromForm] AnnouncementCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var announcementDto = new AnnouncementCreateDto
            {
                AnnouncementTitle = model.AnnouncementTitle, // Added title here
                UserRole = model.UserRole,
                Type = model.Type,
                AnnouncementDate = model.AnnouncementDate,
                AnnouncementContent = model.AnnouncementContent,
                IsImageUpload = model.IsImageUpload,
                Frequency = model.Frequency,
                TotalOccurrences = model.TotalOccurrences,
                EndDate = model.EndDate
            };
            if (model.AnnouncementImageFile != null)
            {
                using (var ms = new MemoryStream())
                {
                    await model.AnnouncementImageFile.CopyToAsync(ms);
                    announcementDto.AnnouncementImage = ms.ToArray();
                }
            }
            var announcement = await _announcementService.CreateAnnouncementAsync(announcementDto);
            return CreatedAtAction(
                nameof(GetAnnouncements),
                new { userRole = announcement.UserRole },
                new
                {
                    announcement.AnnouncementId,
                    announcement.AnnouncementTitle,
                    announcement.AnnouncementContent,
                    ImageUploaded = announcement.IsImageUpload
                }
            );
        }



        [HttpGet("{userRole}")]
        public async Task<IActionResult> GetAnnouncements(AnnouncementUserRole userRole)
        {
            try
            {
                var announcements = await _announcementService.GetAnnouncementsForUserRoleAsync(userRole);

                // Optional: Sanitize response if needed
                var sanitizedAnnouncements = announcements.Select(a => new
                {

                    a.AnnouncementId,
                    a.AnnouncementTitle,
                    a.UserRole,
                    a.Type,
                    a.AnnouncementDate,
                    a.AnnouncementContent,
                    HasImage = a.AnnouncementImage != null,
                    a.Frequency,
                    a.TotalOccurrences,
                    a.EndDate
                });

                return Ok(sanitizedAnnouncements);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving announcements.");
            }
        }
    }
}
