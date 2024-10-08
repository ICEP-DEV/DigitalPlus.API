using DigitalPlus.API.Model;
using DigitalPlus.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Service.Services
{
    public class MentorReportService
    {
        private readonly DigitalPlusDbContext _dbContext;

        public MentorReportService(DigitalPlusDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Method to create and save the mentor report
        public async Task<MentorReport> CreateMentorReport(MentorReport report)
        {
            report.Date = DateTime.UtcNow; // Automatically set the current date and time
            _dbContext.MentorReports.Add(report);
            await _dbContext.SaveChangesAsync();
            return report;
        }
    }
}
