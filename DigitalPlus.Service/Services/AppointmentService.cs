using DigitalPlus.API.Model;
using DigitalPlus.Data;
using DigitalPlus.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Service.Services
{
    public class AppointmentService : ICrudInterface<Appointment>
    {
        public readonly DigitalPlusDbContext _digitalPlusDbContext;
        public AppointmentService(DigitalPlusDbContext digitalPlusDbContext) {
            _digitalPlusDbContext = digitalPlusDbContext;
        }
        public async Task<Appointment> Add(Appointment appointment)
        {
            try
            {  
                _digitalPlusDbContext.Add(appointment);
                await _digitalPlusDbContext.SaveChangesAsync();
                return appointment;

            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Database error occurred while adding the appointment.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding appointment: {ex.Message}", ex);
            }
        }

        public async Task<Appointment> Delete(Appointment appointment)
        {
            try
            {

                var exiatingAppointment = await _digitalPlusDbContext.Appointments.FindAsync(appointment.AppointmentId);
                if (exiatingAppointment == null) { 
                    
                    throw new KeyNotFoundException("Appointment not found.");
                }

                _digitalPlusDbContext.Appointments.Remove(appointment);
                await _digitalPlusDbContext.SaveChangesAsync();
                return exiatingAppointment;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Database error occurred while deleting the appointment.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting appointment: {ex.Message}", ex);
            }
        }

        public async Task<Appointment> Get(int id)
        {
            try
            {
                var appointment= await _digitalPlusDbContext.Appointments
                    .FirstOrDefaultAsync(c => c.StudentNumber == id);

                if (appointment == null)
                {
                    throw new KeyNotFoundException("Appointment not found.");
                }

                return appointment;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving appointment: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Appointment>> GetAll()
        {
            try
            {
                return await _digitalPlusDbContext.Appointments
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving appointments: {ex.Message}", ex);
            }
        }

        public Task<Appointment> Update(Appointment t)
        {
            throw new NotImplementedException();
        }
    }
}
