using Microsoft.AspNetCore.Mvc;
using DigitalPlus.Service.Services;

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
            var data = await _adminDashboardService.GetDashboardData(adminId);
            return Ok(data);
        }
    }

}
