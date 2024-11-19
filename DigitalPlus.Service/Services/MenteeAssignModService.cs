using DigitalPlus.API.Model;
using DigitalPlus.Data;
using DigitalPlus.Data.Dto;
using DigitalPlus.Data.Model;
using DigitalPlus.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Service.Services
{
    public class MenteeAssignModService : IIMenteeAssignModInteface<MenteeAssignModule>
    {
        private readonly DigitalPlusDbContext _digitalPlusDbContext;

        public MenteeAssignModService(DigitalPlusDbContext digitalPlusDbContext)
        {
            _digitalPlusDbContext = digitalPlusDbContext;
        }

        public async Task<MenteeAssignModule> CreateAssignMod(MenteeAssignModDto menteeAssignModDto)
        {
            try {
            
                    
                var menteeExist = await _digitalPlusDbContext.Mentees.AnyAsync(m => m.Mentee_Id == menteeAssignModDto.MenteeId);

                if (!menteeExist) {
                    throw new Exception($"Mentee with ID {menteeAssignModDto.MenteeId} does not exist. Cannot assign module.");
                }

                var assignMod = new MenteeAssignModule
                {
                    MenteeId = menteeAssignModDto.MenteeId,
                    ModuleId = menteeAssignModDto.ModuleId
                };

                _digitalPlusDbContext.MenteeAssignModules.Add(assignMod);
                await _digitalPlusDbContext.SaveChangesAsync();
                return assignMod;

            } catch (DbUpdateException dbEx) {
                var innerExceptionMessage = dbEx.InnerException != null ? dbEx.InnerException.Message : dbEx.Message;
                throw new Exception("Database update error: " + innerExceptionMessage);
            }
        }

        public async Task<bool> DeleteAssignedModule(int moduleId)
        {
            var assinMod = await _digitalPlusDbContext.MenteeAssignModules.FindAsync(moduleId);
            if (assinMod == null)
            {
                return false;
            }

            _digitalPlusDbContext.MenteeAssignModules.Remove(assinMod);
            await _digitalPlusDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<MenteeAssignModule>> GetAssignedModulesByMenteeId(int menteeId)
        {
                    return await _digitalPlusDbContext.MenteeAssignModules
                   .Where(am => am.MenteeId == menteeId)
                   .Include(am => am.Module).ToListAsync();
        }

        public async Task<MenteeAssignModule> UpdateAssignedModule(MenteeAssignModDto menteeAssignModDto)
        {
            var assignMod = await _digitalPlusDbContext.MenteeAssignModules
                .FirstOrDefaultAsync(am => am.AssignModId == menteeAssignModDto.AssignModId);

            if (assignMod == null)
            {
                return null;
            }

            assignMod.MenteeId = menteeAssignModDto.MenteeId;
            assignMod.ModuleId = menteeAssignModDto.ModuleId;

            _digitalPlusDbContext.MenteeAssignModules.Update(assignMod);
            await _digitalPlusDbContext.SaveChangesAsync();
            return assignMod;

        }
    }
}
