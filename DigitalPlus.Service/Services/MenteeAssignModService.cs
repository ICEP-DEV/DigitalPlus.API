using DigitalPlus.Data;
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

        public async Task<MenteeAssignModule> CreateAssignMod(MenteeAssignModule assignModule)
        {
            try
            {
                var existingMentee = await _digitalPlusDbContext.Mentees.AnyAsync(m => m.Mentee_Id == assignModule.MenteeId);
                if (!existingMentee) {

                    throw new Exception($"Mentee with ID {assignModule.MenteeId} does not exist. Cannot assign module.");
                }

                var assignMod = new MenteeAssignModule { 
                    MenteeId = assignModule.MenteeId ,
                    ModuleId = assignModule.ModuleId 
                       };

                _digitalPlusDbContext.menteeAssignModules.Add(assignMod);
                await _digitalPlusDbContext.SaveChangesAsync();
                return assignMod;

            }
            catch (DbUpdateException dbEx) {
                var innerExceptionMessage = dbEx.InnerException != null ? dbEx.InnerException.Message : dbEx.Message;
                throw new Exception("Database update error: " + innerExceptionMessage);
            }
        }

        public async Task<bool> DeleteAssignedModule(int moduleId)
        {
           var assinMod = await _digitalPlusDbContext.menteeAssignModules.FindAsync(moduleId);
            if (assinMod == null)
            {
                return false;
            }

            _digitalPlusDbContext.menteeAssignModules.Remove(assinMod);
            await _digitalPlusDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<MenteeAssignModule>> GetAssignedModulesByMenteeId(int menteeId)
        {
             return await _digitalPlusDbContext.menteeAssignModules
                .Where(am =>am.MenteeId == menteeId)
                .Include(am => am.Module) .ToListAsync();
          
        }

        public async Task<MenteeAssignModule> UpdateAssignedModule(MenteeAssignModule assignModule)
        {
            var assignMod = await _digitalPlusDbContext.menteeAssignModules
                .FirstOrDefaultAsync(am => am.AssignModId == assignModule.AssignModId);

            if(assignMod == null)
            {
                return null;
            }

            assignMod.MenteeId = assignModule.AssignModId;
            assignMod.ModuleId = assignModule.ModuleId;

            _digitalPlusDbContext.menteeAssignModules.Update(assignMod);
            await _digitalPlusDbContext.SaveChangesAsync() ;
            return assignMod;
        }
    }
}
