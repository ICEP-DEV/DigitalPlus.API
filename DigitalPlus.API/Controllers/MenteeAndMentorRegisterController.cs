using DigitalPlus.API.Model;
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
        private readonly IMenteeRegisterInterface _menteeRegisterInterface;
        public MenteeAndMentorRegisterController(DigitalPlusDbContext digitalPlusDbContext, IMentorRegisterInterface mentorRegisterInterface, IMenteeRegisterInterface menteeRegisterInterface)
        {
            _digitalPlustDbContext = digitalPlusDbContext;
            _mentorRegisterInterface = mentorRegisterInterface;
            _menteeRegisterInterface = menteeRegisterInterface;
        }

        [HttpPost("InsertMentorRegister")]
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
        [HttpGet("GetAllMentorsRegisters")]
        public async Task<IEnumerable<MentorRegister>> GetAllMentorRegister()
        {
            var mentorRegisters = await _mentorRegisterInterface.GetAll();
            return mentorRegisters;
        }

        //Mentee Register Endpoints
        [HttpPost("InsertMenteeRegister")]
        public async Task<MenteeRegister> AddMenteeRegister([FromBody] MenteeRegister menteeRegister)
        {
            var register = await _menteeRegisterInterface.AddMenteeRegister(menteeRegister);
            return register;

        }

        [HttpGet("GetMenteeRegister/ByModuleId/{moduleId}")]
        public async Task<ActionResult> GetMenteeRegisterByModuleId(int moduleId)
        {
            var menteeRegisters = await _menteeRegisterInterface.GetRegisterBymoduleId(moduleId);

            if (menteeRegisters == null)
            {
                return NotFound("No Registers Found under the module Id");
            }

            var result = menteeRegisters.Select(am => new
            {

                MenteeRegisterId = am.MenteeRegisterId,
                MentorRegisterId = am.MentorRegisterId,
                MenteeId = am.MenteeId,
                MentorId = am.MentorId,
                ModuleId = am.ModuleId,
                ModuleCode = am.ModuleCode,
                Rating = am.Rating,
                Comment = am.Comment,
                Date = DateTime.Now,
            }).ToList();

            return Ok(result);
        }

        [HttpGet("GetMenteeRegister/ByMenteeId/{menteeId}")]
        public async Task<ActionResult> GetMenteeRegisterByMenteeId(int menteeId)
        {
            var menteeRegister = await _menteeRegisterInterface.GetRegisterByMenteerId(menteeId);
            if (menteeRegister == null)
            {
                return NotFound("The registers under the menteeId Id is not found");
            }

            var result = menteeRegister.Select(am => new
            {
                MenteeRegisterId = am.MenteeRegisterId,
                MentorRegisterId = am.MentorRegisterId,
                MenteeId = am.MenteeId,
                MentorId = am.MentorId,
                ModuleId = am.ModuleId,
                ModuleCode = am.ModuleCode,
                Rating = am.Rating,
                Comment = am.Comment,
                Date = DateTime.Now,
            }).ToList(); return Ok(result);
        }

        [HttpGet("GetAllMenteesRegisters")]
        public async Task<IEnumerable<MenteeRegister>> GetAllMenteeRegisters()
        {
            var menteeRegisters = await _menteeRegisterInterface.GetAll();
            return menteeRegisters;
        }

    }
}
