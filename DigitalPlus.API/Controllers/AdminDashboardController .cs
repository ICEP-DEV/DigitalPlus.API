using Microsoft.AspNetCore.Mvc;
using DigitalPlus.Service.Services;
using System.Threading.Tasks;

namespace DigitalPlus.API.Controllers
{
    [ApiController]
    [Route("api/admin-dashboard")]
    public class AdminDashboardController : ControllerBase
    {
        private readonly AdminDashboardService _adminDashboardService;

        public AdminDashboardController(AdminDashboardService adminDashboardService)
        {
            _adminDashboardService = adminDashboardService;
        }

        [HttpGet("Dashboard/{adminId}")]
        public async Task<IActionResult> GetDashboardData(int adminId)
        {
            // Validate adminId to ensure it is a valid value
            if (adminId <= 0)
            {
                return BadRequest("Invalid admin ID.");
            }

            try
            {
                var data = await _adminDashboardService.GetDashboardData(adminId);

                // Check if data is null (for instance, if no data was found for the provided adminId)
                if (data == null)
                {
                    return NotFound($"No dashboard data found for admin ID {adminId}.");
                }

                return Ok(data);
            }
            catch (Exception)
            {
                // Log the exception (assuming you have a logger configured)
                // _logger.LogError(ex, "Error occurred while fetching dashboard data.");

                // Return a generic error message to the client
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
