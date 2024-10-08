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
    }
}
