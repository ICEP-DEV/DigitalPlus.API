﻿using DigitalPlus.Data.Dto;
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
        public async Task<IActionResult> CreateAnnouncement([FromForm] AnnouncementCreateDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var announcementDto = new Announcement
            {
                AnnouncementTitle = model.AnnouncementTitle, // Added title here
                UserRole = model.UserRole,
                AnnouncementDate = model.AnnouncementDate,
                AnnouncementContent = model.AnnouncementContent,
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
           return Ok(announcement);
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
                    a.AnnouncementDate,
                    a.AnnouncementContent,
                    HasImage = a.AnnouncementImage != null,
                    a.EndDate
                });

                return Ok(sanitizedAnnouncements);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving announcements.");
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Announcement>> UpdateAnnouncement(int id, [FromForm] AnnouncementCreateDto viewModel)
        {
            // Map ViewModel to DTO
            var announcementDto = new Announcement
            {
                AnnouncementTitle = viewModel.AnnouncementTitle,
                UserRole = viewModel.UserRole,
                AnnouncementDate = viewModel.AnnouncementDate,
                AnnouncementContent = viewModel.AnnouncementContent,
                Image = viewModel.AnnouncementImageFile,
                EndDate = viewModel.EndDate
            };

            try
            {
                var updatedAnnouncement = await _announcementService.UpdateAnnouncementAsync(id, announcementDto);
                return Ok(updatedAnnouncement);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            var result = await _announcementService.DeleteAnnouncementAsync(id);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
