using DigitalPlus.Data.Dto;
using DigitalPlus.Service.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DigitalPlus.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssignModController : ControllerBase
    {
        private readonly AssignModService _assignModService;

        public AssignModController(AssignModService assignModService)
        {
            _assignModService = assignModService;
        }

        [HttpGet("mentor/{mentorId}")]
        public async Task<IActionResult> GetAssignedModulesByMentorId(int mentorId)
        {
            // Check if the mentor exists
            bool mentorExists = await _assignModService.MentorExists(mentorId);
            if (!mentorExists)
            {
                return NotFound($"Mentor with ID {mentorId} does not exist.");
            }

            // Retrieve assigned modules
            var assignedModules = await _assignModService.GetAllAssignedModulesByMentorId(mentorId);
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




    }
}
