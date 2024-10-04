using DigitalPlus.API.Model;
using DigitalPlus.Service.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DigitalPlus.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("corspolicy")]
    [ApiController]
    public class DigitalPlusUserController : ControllerBase
    {
        private readonly IIRegisterInterface<Mentor> _mentorService;
        private readonly IIRegisterInterface<Mentee> _menteeService;
        private readonly IIRegisterInterface<Administrator> _adminService;

        // Constructor injecting mentor, mentee, and admin services
        public DigitalPlusUserController(
            IIRegisterInterface<Mentor> mentorService,
            IIRegisterInterface<Mentee> menteeService,
            IIRegisterInterface<Administrator> adminService)
        {
            _mentorService = mentorService ?? throw new ArgumentNullException(nameof(mentorService));
            _menteeService = menteeService ?? throw new ArgumentNullException(nameof(menteeService));
            _adminService = adminService ?? throw new ArgumentNullException(nameof(adminService));
        }

        // Mentor Endpoints
        [HttpPost("AddMentor")]
        public async Task<IActionResult> AddMentor([FromBody] Mentor mentor)
        {
            if (!ModelState.IsValid || mentor == null)
            {
                return BadRequest(ModelState);
            }

            var result = await _mentorService.Register(mentor);
            return CreatedAtAction(nameof(GetMentor), new { id = result.MentorId }, result);
        }

        [HttpGet("GetMentor/{id}")]
        public async Task<IActionResult> GetMentor(int id)
        {
            var mentor = await _mentorService.Get(id);
            if (mentor == null)
            {
                return NotFound();
            }
            return Ok(mentor);
        }

        [HttpPut("UpdateMentor/{id}")]
        public async Task<IActionResult> UpdateMentor(int id, [FromBody] Mentor mentor)
        {
            if (mentor == null || mentor.MentorId != id)
            {
                return BadRequest("Mentor object is null or ID mismatch.");
            }

            try
            {
                var updatedMentor = await _mentorService.Update(mentor);
                return Ok(updatedMentor);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("DeleteMentor/{id}")]
        public async Task<IActionResult> DeleteMentor(int id)
        {
            var mentor = await _mentorService.Get(id);
            if (mentor == null)
            {
                return NotFound();
            }

            await _mentorService.Delete(mentor);
            return NoContent(); // 204 No Content
        }

        [HttpGet("GetAllMentors")]
        public async Task<IActionResult> GetAllMentors()
        {
            var mentors = await _mentorService.GetAll();
            return Ok(mentors);
        }

        // Mentee Endpoints
        [HttpPost("AddMentee")]
        public async Task<IActionResult> AddMentee([FromBody] Mentee mentee)
        {
            if (!ModelState.IsValid || mentee == null)
            {
                return BadRequest(ModelState);
            }

            var result = await _menteeService.Register(mentee);
            return CreatedAtAction(nameof(GetMentee), new { id = result.Mentee_Id }, result);
        }

        [HttpGet("GetMentee/{id}")]
        public async Task<IActionResult> GetMentee(int id)
        {
            var mentee = await _menteeService.Get(id);
            if (mentee == null)
            {
                return NotFound();
            }
            return Ok(mentee);
        }

        [HttpPut("UpdateMentee/{id}")]
        public async Task<IActionResult> UpdateMentee(int id, [FromBody] Mentee mentee)
        {
            if (mentee == null || mentee.Mentee_Id != id)
            {
                return BadRequest("Mentee ID mismatch.");
            }

            try
            {
                var updatedMentee = await _menteeService.Update(mentee);
                return Ok(updatedMentee);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("DeleteMentee/{id}")]
        public async Task<IActionResult> DeleteMentee(int id)
        {
            var mentee = await _menteeService.Get(id);
            if (mentee == null)
            {
                return NotFound();
            }

            await _menteeService.Delete(mentee);
            return NoContent(); // 204 No Content
        }

        [HttpGet("GetAllMentees")]
        public async Task<IActionResult> GetAllMentees()
        {
            var mentees = await _menteeService.GetAll();
            return Ok(mentees);
        }

        // Administrator Endpoints
        [HttpPost("AddAdministrator")]
        public async Task<IActionResult> AddAdministrator([FromBody] Administrator admin)
        {
            if (!ModelState.IsValid || admin == null)
            {
                return BadRequest(ModelState);
            }

            var result = await _adminService.Register(admin);
            return CreatedAtAction(nameof(GetAdministrator), new { id = result.Admin_Id }, result);
        }

        [HttpGet("GetAdministrator/{id}")]
        public async Task<IActionResult> GetAdministrator(int id)
        {
            var admin = await _adminService.Get(id);
            if (admin == null)
            {
                return NotFound();
            }
            return Ok(admin);
        }

        [HttpPut("UpdateAdministrator/{id}")]
        public async Task<IActionResult> UpdateAdministrator(int id, [FromBody] Administrator admin)
        {
            if (admin == null || admin.Admin_Id != id)
            {
                return BadRequest("Administrator object is null or ID mismatch.");
            }

            var updatedAdmin = await _adminService.Update(admin);
            if (updatedAdmin == null)
            {
                return NotFound();
            }
            return Ok(updatedAdmin);
        }

        [HttpDelete("DeleteAdministrator/{id}")]
        public async Task<IActionResult> DeleteAdministrator(int id)
        {
            var admin = await _adminService.Get(id);
            if (admin == null)
            {
                return NotFound();
            }

            await _adminService.Delete(admin);
            return NoContent(); // 204 No Content
        }

        [HttpGet("GetAllAdministrators")]
        public async Task<IActionResult> GetAllAdministrators()
        {
            var admins = await _adminService.GetAll();
            return Ok(admins);
        }
    }
}
