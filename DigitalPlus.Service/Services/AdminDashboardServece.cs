using DigitalPlus.API.Model;
using DigitalPlus.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DigitalPlus.Data.Model;

namespace DigitalPlus.Service.Services
{
    public class AdminDashboardService
    {
        private readonly DigitalPlusDbContext _context;
        public AdminDashboardService(DigitalPlusDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<DashboardData> GetDashboardData(int admin_Id)
        {
            // Assuming there are DbSets for Students (Mentees) and Mentors in the context

            // Get Total Mentees
            var totalMentees = await _context.Mentees.CountAsync();

            // Get Activated Mentees
            var activatedMentees = await _context.Mentees.CountAsync(m => m.Activated);

            // Get Deactivated Mentees
            var deactivatedMentees = totalMentees - activatedMentees;

            // Get Total Mentors
            var totalMentors = await _context.Mentors.CountAsync();

            // Get Activated Mentors
            var activatedMentors = await _context.Mentors.CountAsync(m => m.Activated);

            // Get Deactivated Mentors
            var deactivatedMentors = totalMentors - activatedMentors;

            // Construct the DashboardData model to return
            return new DashboardData
            {
                TotalMentees = totalMentees,
                ActivatedMentees = activatedMentees,
                DeactivatedMentees = deactivatedMentees,
                TotalMentors = totalMentors,
                ActivatedMentors = activatedMentors,
                DeactivatedMentors = deactivatedMentors
            };
        }

    }
}
