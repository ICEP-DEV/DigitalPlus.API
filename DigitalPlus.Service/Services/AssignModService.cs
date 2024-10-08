using DigitalPlus.API.Model;
using DigitalPlus.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Service.Services
{
    public class AssignModService
    {
        private readonly DigitalPlusDbContext _digitalPlusDbContext;

        public AssignModService(DigitalPlusDbContext digitalPlusDbContext)
        {
            _digitalPlusDbContext = digitalPlusDbContext;
        }

        // Service method to retrieve assigned modules for a specific mentor
        public async Task<IEnumerable<AssignMod>> GetAllAssignedModulesByMentorId(int mentorId)
        {
            return await _digitalPlusDbContext.AssignMods
                .Where(am => am.MentorId == mentorId)
                .Include(am => am.Module)
                .ToListAsync();
        }

        // Method to check if a mentor exists
        public async Task<bool> MentorExists(int mentorId)
        {
            return await _digitalPlusDbContext.Mentors.AnyAsync(m => m.MentorId == mentorId);
        }
    }

}
