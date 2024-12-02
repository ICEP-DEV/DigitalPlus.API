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
        public async Task<AssignMod> CreateAssignMod(AssignModDto assignModDto)
        {
            try
            {
                // Check if the Mentor exists
                var mentorExists = await _dbContext.Mentors.AnyAsync(m => m.MentorId == assignModDto.MentorId);
                if (!mentorExists)
                {
                    throw new Exception($"Mentor with ID {assignModDto.MentorId} does not exist. Cannot assign module.");
                }

                var assignMod = new AssignMod
                {
                    MentorId = assignModDto.MentorId, // Set only the MentorId, not the entire Mentor entity
                    ModuleId = assignModDto.ModuleId
                };

                _dbContext.AssignMods.Add(assignMod);
                await _dbContext.SaveChangesAsync();
                return assignMod;
            }
            catch (DbUpdateException dbEx)
            {
                var innerExceptionMessage = dbEx.InnerException != null ? dbEx.InnerException.Message : dbEx.Message;
                throw new Exception("Database update error: " + innerExceptionMessage);
            }
        }



        // Get all assigned modules for a specific mentor
        public async Task<IEnumerable<AssignMod>> GetAssignedModulesByMentorId(int mentorId)
        {
            return await _dbContext.AssignMods
                .Where(am => am.MentorId == mentorId)  // Filter by MentorId
                .Include(am => am.Module)  // Include the Module navigation property correctly
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
             .Include(am => am.Mentor) // Assuming Mentor is a navigation property
             .Select(am => am.Mentor)  // Select only the mentor details
             .ToListAsync();
        }
    }
}
