using DigitalPlus.API.Model;
using DigitalPlus.Service.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DigitalPlus.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [EnableCors("corspolicy")]
    [ApiController]
    public class DigitalPlusLoginController : ControllerBase
    {
        private readonly IIRegisterInterface<Mentor> _mentorService;
        private readonly IIRegisterInterface<Mentee> _menteeService;
        private readonly IIRegisterInterface<Administrator> _adminService;

        // Constructor injecting all services
        public DigitalPlusLoginController(IIRegisterInterface<Mentor> mentorService, IIRegisterInterface<Mentee> menteeService, IIRegisterInterface<Administrator> adminService)
        {
            _mentorService = mentorService ?? throw new ArgumentNullException(nameof(mentorService));
            _menteeService = menteeService ?? throw new ArgumentNullException(nameof(menteeService));
            _adminService = adminService ?? throw new ArgumentNullException(nameof(adminService));
        }

        // POST: api/DigitalPlusLogin/Login
        [HttpPost]
       public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    if (loginRequest == null || string.IsNullOrWhiteSpace(loginRequest.Email) || string.IsNullOrWhiteSpace(loginRequest.Password))
    {
        return BadRequest(new { Success = false, Message = "Invalid login request." });
    }

    // Check Administrators table
    var admin = await _adminService.GetByEmailAndPassword(loginRequest.Email, loginRequest.Password);
    if (admin != null)
    {
       return Ok(new { Success = true, Message = "Successfully Logged in!", Role = "Admin", User = admin });
    }

    // Check Mentors table
    var mentor = await _mentorService.GetByEmailAndPassword(loginRequest.Email, loginRequest.Password);
    if (mentor != null)
    {
        if (mentor.Activated != true) // Assuming 1 means activated
        {
            return Ok(new { Success = false, Message = "Account not activated. Please contact the administrator." });
        }
        return Ok(new { Success = true, Message = "Successfully Logged in!", Role = "Mentor", User = mentor });
    }

    // Check Mentees table
    var mentee = await _menteeService.GetByEmailAndPassword(loginRequest.Email, loginRequest.Password);
    if (mentee != null)
    {
        if (mentee.Activated != true) // Assuming 1 means activated
        {
            return Ok(new { Success = false, Message = "Account not activated. Please contact the administrator." });
        }
        return Ok(new { Success = true, Message = "Successfully Logged in!", Role = "Mentee", User = mentee });
    }

    // If no match is found
    return Ok(new { Success = false, Message = "User does not exist." });
}


    }

    // Simple DTO for login request
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
