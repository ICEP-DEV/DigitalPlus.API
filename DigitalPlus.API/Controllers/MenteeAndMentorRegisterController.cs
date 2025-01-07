using DigitalPlus.Data;
using DigitalPlus.Data.Dto;
using DigitalPlus.Data.Model;
using DigitalPlus.Service.Interfaces;
using DigitalPlus.Service.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalPlus.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("corspolicy")]
    [ApiController]
    public class MenteeAndMentorRegisterController : Controller
    {

        private readonly DigitalPlusDbContext _digitalPlustDbContext;
        private readonly IMentorRegisterInterface _mentorRegisterInterface;
        public MenteeAndMentorRegisterController (DigitalPlusDbContext digitalPlusDbContext,IMentorRegisterInterface mentorRegisterInterface)
        {
            _digitalPlustDbContext = digitalPlusDbContext;
            _mentorRegisterInterface = mentorRegisterInterface;
        }

        [HttpPost]
        public async Task<MentorRegister> AddMentorRegister( [FromBody] InsertMentorRegisterDto insertMentorRegisterDto)
        {
            
            var mentorRegister= await _mentorRegisterInterface.AddRegister(insertMentorRegisterDto);
            return mentorRegister;
            
        }

        [HttpGet("GetMentorRegister/ByModuleId/{moduleId}")]
        public async Task<ActionResult> GetMentorRegisterByModuleId(int moduleId)
        {
            var mentorRegister = await _mentorRegisterInterface.GetRegiserBymoduleId(moduleId);

            if (mentorRegister == null)
            {
                return NotFound("No Registers Found under the module Id");
            }

            var result = mentorRegister.Select(am => new
            {
                MentorRegisterID = am.MentorRegisterID,
                MentorId = am.MentorId,
                ModuleId = am.ModuleId,
                ModuleCode = am.ModuleCode,
                IsRegisteractivated = am.IsRegisteractivated,
                Date = DateTime.Now,
            }).ToList();

            return Ok(result);
        }

        [HttpGet("GetMentorRegister/ByMentorId/{mentorId}")]
        public async Task<ActionResult> GetMentorRegisterByMentorId(int mentorId)
        {
            var mentorRegister = await _mentorRegisterInterface.GetRegisterByMentorId(mentorId);
            if (mentorRegister == null)
            {
                return NotFound("The registers under the mentorId Id is not found");
            }

            var result = mentorRegister.Select(am => new
            {

                MentorRegisterID = am.MentorRegisterID,
                MentorId = am.MentorId,
                ModuleId = am.ModuleId,
                ModuleCode = am.ModuleCode,
                IsRegisteractivated = am.IsRegisteractivated,
                Date = DateTime.Now,
            }).ToList(); return Ok(result);
        }
        [HttpGet]
        public async Task<IEnumerable<MentorRegister>> GetAllMentorRegister()
        {
            var mentorRegisters = await _mentorRegisterInterface.GetAll();
            return mentorRegisters;
        }

       
    }
}
