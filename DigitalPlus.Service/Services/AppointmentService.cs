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

        public Task<Appointment> Delete(Appointment t)
        {
            throw new NotImplementedException();
        }

        public Task<Appointment> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Appointment>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Appointment> Update(Appointment t)
        {
            throw new NotImplementedException();
        }
    }
}
