using DigitalPlus.API.Model;
using DigitalPlus.Service.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DigitalPlus.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [EnableCors("corspolicy")]
    [ApiController]
    public class DigitalPlusUserController : ControllerBase
    {
        private readonly IIRegisterInterface<Mentor> _mentorService;
        private readonly IIRegisterInterface<Mentee> _menteeService;

        // Constructor injecting both services
        public DigitalPlusUserController(IIRegisterInterface<Mentor> mentorService, IIRegisterInterface<Mentee> menteeService)
        {
            _mentorService = mentorService ?? throw new ArgumentNullException(nameof(mentorService));
            _menteeService = menteeService ?? throw new ArgumentNullException(nameof(menteeService));
        }

        // POST: api/DigitalPlusUser/AddMentor
        [HttpPost]
        public async Task<IActionResult> AddMentor([FromBody] Mentor mentor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (mentor == null)
            {
                return BadRequest("Mentor object cannot be null.");
            }

            var result = await _mentorService.Register(mentor);
            return CreatedAtAction(nameof(GetMentor), new { id = result.MentorId }, result);
        }

        // GET: api/DigitalPlusUser/GetMentor/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMentor(int id)
        {
            var mentor = await _mentorService.Get(id);
            if (mentor == null)
            {
                return NotFound();
            }
            return Ok(mentor);
        }

        // PUT: api/DigitalPlusUser/UpdateMentor
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMentor(int id, [FromBody] Mentor mentor)
        {
            if (mentor == null || mentor.MentorId != id)
            {
                return BadRequest("Mentor object is null or ID mismatch.");
            }

            var updatedMentor = await _mentorService.Update(mentor);
            if (updatedMentor == null)
            {
                return NotFound();
            }
            return Ok(updatedMentor);
        }

        // DELETE: api/DigitalPlusUser/DeleteMentor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMentor(int id)
        {
            var mentor = await _mentorService.Get(id);
            if (mentor == null)
            {
                return NotFound();
            }

            var result = await _mentorService.Delete(mentor);
            return Ok(result);
        }

        // GET: api/DigitalPlusUser/GetAllMentors
        [HttpGet]
        public async Task<IActionResult> GetAllMentors()
        {
            var mentors = await _mentorService.GetAll();
            return Ok(mentors);
        }

        // POST: api/DigitalPlusUser/AddMentee
        [HttpPost]
        public async Task<IActionResult> AddMentee([FromBody] Mentee mentee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (mentee == null)
            {
                return BadRequest("Mentee object cannot be null.");
            }

            var result = await _menteeService.Register(mentee);
            return CreatedAtAction(nameof(GetMentee), new { id = result.Mentee_Id }, result);
        }

        // GET: api/DigitalPlusUser/GetMentee/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMentee(int id)
        {
            var mentee = await _menteeService.Get(id);
            if (mentee == null)
            {
                return NotFound();
            }
            return Ok(mentee);
        }

        // PUT: api/DigitalPlusUser/UpdateMentee
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMentee(int id, [FromBody] Mentee mentee)
        {
            if (mentee == null || mentee.Mentee_Id != id)
            {
                return BadRequest("Mentee object is null or ID mismatch.");
            }

            var updatedMentee = await _menteeService.Update(mentee);
            if (updatedMentee == null)
            {
                return NotFound();
            }
            return Ok(updatedMentee);
        }

        // DELETE: api/DigitalPlusUser/DeleteMentee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMentee(int id)
        {
            var mentee = await _menteeService.Get(id);
            if (mentee == null)
            {
                return NotFound();
            }

            var result = await _menteeService.Delete(mentee);
            return Ok(result);
        }

        // GET: api/DigitalPlusUser/GetAllMentees
        [HttpGet]
        public async Task<IActionResult> GetAllMentees()
        {
            var mentees = await _menteeService.GetAll();
            return Ok(mentees);
        }
    }
}
