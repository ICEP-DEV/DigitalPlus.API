using DigitalPlus.API.Model;
using DigitalPlus.Data.Dto;
using DigitalPlus.Data.Model;
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
        private readonly IIMenteeAssignModInteface<MenteeAssignModule> _menteeAssignModService;

        // Inject IAssignModService into the controller
        public AssignModController(IAssignModService<AssignMod> assignModuleService, IIMenteeAssignModInteface<MenteeAssignModule> menteeAssignModService)
        {
            _assignModuleService = assignModuleService;
            _menteeAssignModService = menteeAssignModService;
        }

        // GET: api/AssignMod/mentor/{mentorId}
        [HttpGet("getmodulesBy_MentorId/{mentorId}")]
        public async Task<IActionResult> GetAssignedModulesByMentorId(int mentorId)
        {
            // Retrieve assigned modules with module details
            var assignedModules = await _assignModuleService.GetAssignedModulesByMentorId(mentorId);
            if (assignedModules == null || !assignedModules.Any())
            {
                return NotFound("No modules assigned for this mentor.");
            }

            // Prepare response data with both AssignModDto and module details
            var result = assignedModules.Select(am => new
            {
                AssignModId = am.AssignModId,
                MentorId = am.MentorId,
                ModuleId = am.ModuleId,
                ModuleCode = am.Module.Module_Code,  // Include module details without modifying the DTO
                ModuleName = am.Module.Module_Name,
                ModuleDescription = am.Module.Description
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


        //The section below is the ASSIGN MODULES FOR THE MENTEE

        //ADD ASSIGN MODULE FOR MENTEE

        [HttpPost("MenteeAssignMod")]
        public async Task<IActionResult> MenteeAssignModule([FromBody] MenteeAssignModDto menteeAssignModDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdAssignMod = await _menteeAssignModService.CreateAssignMod(menteeAssignModDto);
                return CreatedAtAction(nameof(MenteeAssignModule), new { id = createdAssignMod.AssignModId }, createdAssignMod);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        //Delete Mentee Assigned Module

        [HttpDelete("deleteMentee/{assignModId}")]
        public async Task<IActionResult> DeleteMenteeAssignedModule(int assignModId)
        {
            var isDeleted = await _menteeAssignModService.DeleteAssignedModule(assignModId);

            if (!isDeleted)
            {
                return NotFound($"Assigned module with ID {assignModId} not found.");
            }

            return Ok($"Assigned module with ID {assignModId} has been deleted.");
        }

        //GET ADDIGNED MODULE BY MENTEE ID
        [HttpGet("getmodulesBy_MenteeId/{menteeId}")]
        public async Task<IActionResult> GetAssignedModulesByMenteeId(int menteeId)
        {
            // Retrieve assigned modules with module details
            var assignedModules = await _menteeAssignModService.GetAssignedModulesByMenteeId(menteeId);    
            if (assignedModules == null || !assignedModules.Any())
            {
                return NotFound("No modules assigned for this mentee.");
            }

            // Prepare response data with both AssignModDto and module details
            var result = assignedModules.Select(am => new
            {
                AssignModId = am.AssignModId,
                MenteeId = am.MenteeId,
                ModuleId = am.ModuleId,
                ModuleCode = am.Module.Module_Code,  // Include module details without modifying the DTO
                ModuleName = am.Module.Module_Name,
                ModuleDescription = am.Module.Description
            }).ToList();

            return Ok(result);
        }


        //UPDATE MENTEE ASSIGNED MODULES
        [HttpPut("update/mentee")]
        public async Task<IActionResult> UpdateMenteeAssignedModule([FromBody] MenteeAssignModDto menteeAssignModDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedAssignMod = await _menteeAssignModService.UpdateAssignedModule(menteeAssignModDto);
            if (updatedAssignMod == null)
            {
                return NotFound("Assigned module not found.");
            }

            return Ok(updatedAssignMod);
        }

    }
}
