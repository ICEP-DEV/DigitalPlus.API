using DigitalPlus.API.Model;
using DigitalPlus.Data;
using DigitalPlus.Data.Dto;
using DigitalPlus.Data.Model;
using DigitalPlus.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Service.Services
{
    public class MentorRegisterService : IMentorRegisterInterface
    {

        private readonly DigitalPlusDbContext _digitalPlusDbContext;

        public MentorRegisterService(DigitalPlusDbContext digitalPlusDbContext)
        {
            _digitalPlusDbContext = digitalPlusDbContext;
        }

        public async Task<MentorRegister> AddRegister(InsertMentorRegisterDto insertMentorRegisterDto)
        {
            insertMentorRegisterDto.Date = DateTime.UtcNow;

            var mentorRegister = new MentorRegister
            {
                MentorId = insertMentorRegisterDto.MentorId,
                MentorName= insertMentorRegisterDto.MentorName,
                ModuleId=insertMentorRegisterDto.ModuleId,
                ModuleCode=insertMentorRegisterDto.ModuleCode,
                IsRegisteractivated=insertMentorRegisterDto.IsRegisteractivated,
                Date=insertMentorRegisterDto.Date,
             };

            await _digitalPlusDbContext.MentorRegisters.AddAsync(mentorRegister);
             await _digitalPlusDbContext.SaveChangesAsync();
            return mentorRegister;
        }

        public async Task<IEnumerable<MentorRegister>> GetAll()
        {
            return await _digitalPlusDbContext.MentorRegisters.ToListAsync();
        }

        public async Task<IEnumerable<MentorRegister>> GetRegiserBymoduleId(int moduleId)
        {
            return await _digitalPlusDbContext.MentorRegisters
                .Where(am => am.ModuleId == moduleId)
                .ToListAsync();
        }

        public async Task<IEnumerable<MentorRegister>> GetRegisterByMentorId(int mentorId)
        {
            return await _digitalPlusDbContext.MentorRegisters
                .Where(am => am.MentorId == mentorId).ToListAsync();
        }
    }
}
