using DigitalPlus.API.Model;
using DigitalPlus.Data.Dto;
using DigitalPlus.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalPlus.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssignModController : ControllerBase
    {
        private readonly IAssignModService<AssignMod> _assignModuleService;

        // Inject IAssignModService into the controller
        public AssignModController(IAssignModService<AssignMod> assignModuleService)
        {
            _assignModuleService = assignModuleService;
        }

        // GET: api/AssignMod/mentor/{mentorId}
        [HttpGet("getmodulesBy_MentorId/{mentorId}")]
        public async Task<IActionResult> GetAssignedModulesByMentorId(int mentorId)
        {
            // Retrieve assigned modules
            var assignedModules = await _assignModuleService.GetAssignedModulesByMentorId(mentorId);
            if (assignedModules == null || !assignedModules.Any())
            {
                return NotFound("No modules assigned for this mentor.");
            }

            // Prepare response data
            var result = assignedModules.Select(am => new AssignModDto
            {
                AssignModId = am.AssignModId,
                MentorId = am.MentorId,
                ModuleId = am.ModuleId
            }).ToList();

            return Ok(result);
        }

        // POST: api/AssignMod/AssignModule
        [HttpPost("AssignModule")]
        public async Task<IActionResult> AssignModule([FromBody] AssignModDto assignModDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdAssignMod = await _assignModuleService.CreateAssignMod(assignModDto);
                return CreatedAtAction(nameof(AssignModule), new { id = createdAssignMod.AssignModId }, createdAssignMod);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }


        // DELETE: api/AssignMod/{assignModId}
        [HttpDelete("delete/{assignModId}")]
        public async Task<IActionResult> DeleteAssignedModule(int assignModId)
        {
            var isDeleted = await _assignModuleService.DeleteAssignedModule(assignModId);

            if (!isDeleted)
            {
                return NotFound($"Assigned module with ID {assignModId} not found.");
            }

            return Ok($"Assigned module with ID {assignModId} has been deleted.");
        }

        // PUT: api/AssignMod
        [HttpPut("update")]
        public async Task<IActionResult> UpdateAssignedModule([FromBody] AssignModDto assignModDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedAssignMod = await _assignModuleService.UpdateAssignedModule(assignModDto);
            if (updatedAssignMod == null)
            {
                return NotFound("Assigned module not found.");
            }

            return Ok(updatedAssignMod);
        }

        [HttpGet("module/{moduleId}/mentors")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Mentor>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMentorsByModuleId(int moduleId)
        {
            try
            {
                var mentors = await _assignModuleService.GetMentorsByModuleId(moduleId);

                if (mentors == null || !mentors.Any())
                {
                    return NotFound($"No mentors found for module with ID: {moduleId}");
                }

                return Ok(new
                {
                    Success = true,
                    Message = "Mentors retrieved successfully",
                    Data = mentors
                });
            }
            catch (Exception ex)
            {
                // Log the exception here if you have logging configured
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Success = false,
                    Message = "An error occurred while retrieving the mentors",
                    Error = ex.Message
                });
            }
        }
    }
}
