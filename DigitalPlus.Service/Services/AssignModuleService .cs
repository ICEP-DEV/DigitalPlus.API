using DigitalPlus.API.Model;
using DigitalPlus.Data;
using DigitalPlus.Data.Dto;
using DigitalPlus.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalPlus.Service.Services
{
    public class AssignModuleService : IAssignModService<AssignMod>
    {
        private readonly DigitalPlusDbContext _dbContext;

        public AssignModuleService(DigitalPlusDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Create a new module assignment
        public async Task<AssignMod> CreateAssignMod(AssignModDto assignModDto)
        {
            var assignMod = new AssignMod
            {
                MentorId = assignModDto.MentorId,
                ModuleId = assignModDto.ModuleId
            };

            _dbContext.AssignMods.Add(assignMod);
            await _dbContext.SaveChangesAsync();
            return assignMod;
        }

        // Get all assigned modules for a specific mentor
        public async Task<IEnumerable<AssignMod>> GetAssignedModulesByMentorId(int mentorId)
        {
            return await _dbContext.AssignMods
                .Where(am => am.MentorId == mentorId)  // Filter by MentorId
                .ToListAsync();
        }

        // Delete an assigned module for a mentor
        public async Task<bool> DeleteAssignedModule(int assignModId)
        {
            var assignMod = await _dbContext.AssignMods.FindAsync(assignModId);
            if (assignMod == null)
            {
                return false; // If the record doesn't exist, return false
            }

            _dbContext.AssignMods.Remove(assignMod);
            await _dbContext.SaveChangesAsync();
            return true; // Return true if the deletion was successful
        }

        // Update an assigned module
        public async Task<AssignMod> UpdateAssignedModule(AssignModDto assignModDto)
        {
            var assignMod = await _dbContext.AssignMods
                .FirstOrDefaultAsync(am => am.AssignModId == assignModDto.AssignModId);

            if (assignMod == null)
            {
                return null;
            }

            assignMod.MentorId = assignModDto.MentorId;
            assignMod.ModuleId = assignModDto.ModuleId;

            _dbContext.AssignMods.Update(assignMod);
            await _dbContext.SaveChangesAsync();
            return assignMod;
        }

        public async Task<IEnumerable<Mentor>> GetMentorsByModuleId(int moduleId)
        {
            return await _dbContext.AssignMods
               .Where(am => am.ModuleId == moduleId)
               .Include(am => am.Mentor)
               .Select(am => am.Mentor)
               .ToListAsync();
        }
    }
}
