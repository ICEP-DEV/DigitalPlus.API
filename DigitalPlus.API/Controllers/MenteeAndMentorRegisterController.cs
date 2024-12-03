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

        [HttpGet]
        public async Task<IEnumerable<MentorRegister>> GetAllMentorRegister()
        {
            var mentorRegisters = await _mentorRegisterInterface.GetAll();
            return mentorRegisters;
        }

       
    }
}
