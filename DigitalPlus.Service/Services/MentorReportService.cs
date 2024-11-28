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
    public class MentorReportService
    {
        private readonly DigitalPlusDbContext _dbContext;

        public MentorReportService(DigitalPlusDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> MentorExists(int mentorId)
        {
            return await _dbContext.Mentors.AnyAsync(m => m.MentorId == mentorId);
        }

        public async Task<bool> ReportExists(int mentorId, int reportId)
        {
            return await _dbContext.MentorReports.AnyAsync(r => r.MentorId == mentorId && r.ReportId == reportId);
        }

        // Method to create and save the mentor report
        public async Task<MentorReport> CreateMentorReport(MentorReport report)
        {
            report.Date = DateTime.UtcNow; // Automatically set the current date and time
            _dbContext.MentorReports.Add(report);
            await _dbContext.SaveChangesAsync();
            return report;
        }


        public async Task<List<MentorReport>> GetReportsByMentorId(int mentorId)
        {
            return await _dbContext.MentorReports
                .Where(r => r.MentorId == mentorId)
                .ToListAsync();
        }

        public async Task<List<MentorReport>> GetAllReports()
        {
            return await _dbContext.MentorReports.ToListAsync();
        }

        // Update a report by mentorId and reportId
        public async Task<MentorReport> UpdateReport(int mentorId, int reportId, MentorReport updatedReport)
        {
            var report = await _dbContext.MentorReports
                .FirstOrDefaultAsync(r => r.MentorId == mentorId && r.ReportId == reportId);

            if (report == null) return null;

            report.Month = updatedReport.Month;
            report.NoOfStudents = updatedReport.NoOfStudents;
            report.Remarks = updatedReport.Remarks;
            report.Challenges = updatedReport.Challenges;
            report.SocialEngagement = updatedReport.SocialEngagement;

            await _dbContext.SaveChangesAsync();
            return report;
        }

        public async Task<bool> DeleteReport(int mentorId, int reportId)
        {
            var report = await _dbContext.MentorReports
                .FirstOrDefaultAsync(r => r.MentorId == mentorId && r.ReportId == reportId);

            if (report == null) return false;

            _dbContext.MentorReports.Remove(report);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
