using DigitalPlus.API.Model;
using DigitalPlus.Data;
using DigitalPlus.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Service.Services
{
    public class MenteeRegisterSevices : IMenteeRegisterInterface
    {
        private readonly DigitalPlusDbContext _digitalPlusDbContext;

        public MenteeRegisterSevices(DigitalPlusDbContext digitalPlusDbContext)
        {
            _digitalPlusDbContext = digitalPlusDbContext;
        }
        public async Task<MenteeRegister> AddMenteeRegister(MenteeRegister menteeRegister)
        {
            try
            {
                _digitalPlusDbContext.MenteeRegisters.Add(menteeRegister);
                await _digitalPlusDbContext.SaveChangesAsync();
                return menteeRegister;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Database error occurred while adding the Mentee Register.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding Mentee Register: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<MenteeRegister>> GetAll()
        {
            return await _digitalPlusDbContext.MenteeRegisters.ToListAsync();
        }

        public async Task<IEnumerable<MenteeRegister>> GetRegisterByMenteerId(int menteeId)
        {
            return await _digitalPlusDbContext.MenteeRegisters
               .Where(am => am.MenteeId == menteeId)
               .ToListAsync();
        }

        public async Task<IEnumerable<MenteeRegister>> GetRegisterBymoduleId(int moduleId)
        {
            return await _digitalPlusDbContext.MenteeRegisters
                .Where(am => am.ModuleId == moduleId)
                .ToListAsync();
        }
    }
}
