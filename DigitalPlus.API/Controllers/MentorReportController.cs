using DigitalPlus.API.Dto;
using DigitalPlus.API.Model;
using DigitalPlus.Service.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DigitalPlus.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/add_Report")]
    public class MentorReportController : ControllerBase
    {
        private readonly MentorReportService _mentorReportService;

        public MentorReportController(MentorReportService mentorReportService)
        {
            _mentorReportService = mentorReportService;
        }

        // POST: api/MentorReport
        [HttpPost]
        public async Task<IActionResult> SubmitMentorReport([FromBody] MentorReportDto reportDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map DTO to Entity
            var mentorReport = new MentorReport
            {
                MentorId = reportDto.MentorId,
                Month = reportDto.Month,
                NoOfStudents = reportDto.NoOfStudents,
                Remarks = reportDto.Remarks,
                Challenges = reportDto.Challenges,
                SocialEngagement = reportDto.SocialEngagement
            };

            var createdReport = await _mentorReportService.CreateMentorReport(mentorReport);

            return CreatedAtAction(nameof(SubmitMentorReport), new { id = createdReport.ReportId }, createdReport);
        }


        [HttpGet("reports/{mentorId}")]
        public async Task<IActionResult> GetReportsByMentorId(int mentorId)
        {
            if (!await _mentorReportService.MentorExists(mentorId))
            {
                return NotFound($"Mentor with ID {mentorId} does not exist.");
            }

            var reports = await _mentorReportService.GetReportsByMentorId(mentorId);

            if (reports == null || !reports.Any())
            {
                return NotFound($"No reports found for Mentor ID {mentorId}.");
            }

            return Ok(new { message = "Reports retrieved successfully.", reports });
        }

        [HttpGet("all_reports")]
        public async Task<IActionResult> GetAllReports()
        {
            var reports = await _mentorReportService.GetAllReports();

            if (!reports.Any())
            {
                return NotFound("No reports found.");
            }

            return Ok(new { message = "All reports retrieved successfully.", reports });
        }

        [HttpPut("update_Report/{mentorId}/{reportId}")]
        public async Task<IActionResult> UpdateReport(int mentorId, int reportId, [FromBody] MentorReportDto updatedReportDto)
        {
            if (!await _mentorReportService.MentorExists(mentorId))
            {
                return NotFound($"Mentor with ID {mentorId} does not exist.");
            }

            if (!await _mentorReportService.ReportExists(mentorId, reportId))
            {
                return NotFound($"Report with ID {reportId} for Mentor ID {mentorId} does not exist.");
            }

            var updatedReport = new MentorReport
            {
                Month = updatedReportDto.Month,
                NoOfStudents = updatedReportDto.NoOfStudents,
                Remarks = updatedReportDto.Remarks,
                Challenges = updatedReportDto.Challenges,
                SocialEngagement = updatedReportDto.SocialEngagement
            };

            var report = await _mentorReportService.UpdateReport(mentorId, reportId, updatedReport);

            return Ok(new { message = "Report updated successfully.", report });
        }

        [HttpDelete("delete_Report/{mentorId}/{reportId}")]
        public async Task<IActionResult> DeleteReport(int mentorId, int reportId)
        {
            if (!await _mentorReportService.MentorExists(mentorId))
            {
                return NotFound($"Mentor with ID {mentorId} does not exist.");
            }

            if (!await _mentorReportService.ReportExists(mentorId, reportId))
            {
                return NotFound($"Report with ID {reportId} for Mentor ID {mentorId} does not exist.");
            }

            var isDeleted = await _mentorReportService.DeleteReport(mentorId, reportId);

            if (!isDeleted)
            {
                return StatusCode(500, "An error occurred while deleting the report.");
            }

            return Ok(new { message = "Report deleted successfully." });
        }
    }
}
