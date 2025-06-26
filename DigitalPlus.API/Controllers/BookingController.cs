using DigitalPlus.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using DigitalPlus.Data.Model;
using DigitalPlus.Data;

namespace DigitalPlus.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly DigitalPlusDbContext _dbContext;

        public BookingController(DigitalPlusDbContext context)
        {
            _dbContext = context;
        }

        // POST: api/Booking/AddBooking
        [HttpPost("AddBooking")]
        public async Task<IActionResult> AddBooking([FromBody] Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            booking.Status = "Pending"; // Default status for a new booking
            _dbContext.Bookings.Add(booking);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(AddBooking), new { id = booking.BookingId }, booking);
        }

        // GET: api/Booking/GetAllBookings
        [HttpGet("GetAllBookings")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetAllBookings()
        {
            var bookings = await _dbContext.Bookings.ToListAsync();
            return Ok(bookings);
        }

        // GET: api/Booking/GetBookingsByMentorId/{mentorId}
        [HttpGet("GetBookingsByMentorId/{mentorId}")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookingsByMentorId(int mentorId)
        {
            var bookings = await _dbContext.Bookings
                .Where(b => b.MentorId == mentorId)
                .ToListAsync();

            if (bookings.Count == 0)
            {
                return NotFound("No bookings found for the specified mentor.");
            }

            return Ok(bookings);
        }

        // GET: api/Booking/GetBookingsByMenteeId/{menteeId}
        [HttpGet("GetBookingsByMenteeId/{menteeId}")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookingsByMenteeId(int menteeId)
        {
            var bookings = await _dbContext.Bookings
                .Where(b => b.MenteeId == menteeId)
                .ToListAsync();

            if (bookings.Count == 0)
            {
                return NotFound("No bookings found for the specified mentee.");
            }

            return Ok(bookings);
        }


        // DELETE: api/Booking/DeleteBooking/{id}
        [HttpDelete("DeleteBooking/{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _dbContext.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound("Booking not found.");
            }

            _dbContext.Bookings.Remove(booking);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        // PUT: api/Booking/UpdateBooking
        [HttpPut("UpdateBooking")]
        public async Task<IActionResult> UpdateBooking([FromBody] Booking updatedBooking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingBooking = await _dbContext.Bookings.FindAsync(updatedBooking.BookingId);
            if (existingBooking == null)
            {
                return NotFound("Booking not found.");
            }

            // Update booking properties
            existingBooking.MenteeId = updatedBooking.MenteeId;
            existingBooking.MentorId = updatedBooking.MentorId;
            existingBooking.ModuleId = updatedBooking.ModuleId;
            existingBooking.BookingDateTime = updatedBooking.BookingDateTime;
            existingBooking.SessionType = updatedBooking.SessionType;
            existingBooking.Status = updatedBooking.Status;

            await _dbContext.SaveChangesAsync();
            return Ok(existingBooking);
        }

        // POST: api/Booking/MoveToAppointment/{id}
        [HttpPost("MoveToAppointment/{id}")]
        public async Task<IActionResult> MoveToAppointment(int id)
        {
            var booking = await _dbContext.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound("Booking not found.");
            }

            // Create an Appointment from the Booking
            var appointment = new Appointment
            {
                StudentNumber = booking.MenteeId,
                MentorId = booking.MentorId,
                ModuleId = booking.ModuleId,
                DateTime = booking.BookingDateTime,
                LessonType = booking.SessionType
            };

            // Save Appointment and remove Booking
            _dbContext.Appointments.Add(appointment);
            _dbContext.Bookings.Remove(booking);

            await _dbContext.SaveChangesAsync();
            return Ok("Booking moved to Appointment table successfully.");
        }

        [HttpPost("MentorRescheduleRequest")]
        public async Task<IActionResult> MentorRescheduleRequest(int bookingId, [FromQuery] string reason = null)
        {
            var booking = await _dbContext.Bookings.FindAsync(bookingId);
            if (booking == null)
            {
                return NotFound("Booking not found.");
            }

            // Fetch the mentor name using MentorId
            var mentor = await _dbContext.Mentors.FindAsync(booking.MentorId);
            if (mentor == null)
            {
                return NotFound("Mentor not found for the given MentorId.");
            }
            string mentorName = mentor.FirstName + " " + mentor.LastName;

            // Fetch the module name using ModuleId
            var module = await _dbContext.Modules.FindAsync(booking.ModuleId);
            if (module == null)
            {
                return NotFound("Module not found for the given ModuleId.");
            }
            string moduleName = module.Module_Name.ToUpper();

            // Build the response message
            string responseMessage = $"Mentor {mentorName} requested a reschedule for module {moduleName}.";
            if (!string.IsNullOrEmpty(reason))
            {
                responseMessage += $" Reason: {reason}";
            }

            // Delete the booking after rescheduling
            _dbContext.Bookings.Remove(booking);
            await _dbContext.SaveChangesAsync();  // Persist the deletion

            return Ok(responseMessage);
        }


    }
}
