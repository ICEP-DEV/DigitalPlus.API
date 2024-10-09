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
                ModuleId = am.ModuleId,
                ModuleCode = am.Module?.Module_Code, // Using null-coalescing operator to avoid null exception
                ModuleName = am.Module?.Module_Name ?? "Unknown Module", // Handle null Module
                ModuleDescription = am.Module?.Description ?? "No description available" // Handle null Module
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

            var createdAssignMod = await _assignModuleService.CreateAssignMod(assignModDto);
            return CreatedAtAction(nameof(AssignModule), new { id = createdAssignMod.AssignModId }, createdAssignMod);
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
    }
}
