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

    public class ScheduleService : ICrudInterface<Schedule>
    {

        public readonly DigitalPlusDbContext _digitalPlusDbContext;

        public  ScheduleService(DigitalPlusDbContext digitalPlusDbContext)
        {
            _digitalPlusDbContext = digitalPlusDbContext;
        }
        public async Task<Schedule> Add(Schedule schedule)
        {
            try
            {
                _digitalPlusDbContext.Add(schedule);
                await _digitalPlusDbContext.SaveChangesAsync();
                return schedule;

            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Database error occurred while adding the Schedule.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding Schedule: {ex.Message}", ex);
            }
        }

        public async Task<Schedule> Delete(Schedule schedule)
        {

            try
            {

                var existingSchedule     = await _digitalPlusDbContext.Schedules.FindAsync(schedule.ScheduleId);
                if (existingSchedule == null)
                {

                    throw new KeyNotFoundException("Schedule not found.");
                }

                _digitalPlusDbContext.Schedules.Remove(schedule);
                await _digitalPlusDbContext.SaveChangesAsync();
                return existingSchedule;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Database error occurred while deleting the Schedule.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting Schedule: {ex.Message}", ex);
            }
        }

        public async Task<Schedule> Get(int id)
        {

            try
            {
                var schedule = await _digitalPlusDbContext.Schedules
                    .FirstOrDefaultAsync(c => c.ScheduleId == id);

                if (schedule == null)
                {
                    throw new KeyNotFoundException("Schedule not found.");
                }

                return schedule;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving Schedule: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Schedule>> GetAll()
        {
            try
            {
                return await _digitalPlusDbContext.Schedules
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving Schedule: {ex.Message}", ex);
            }
        }

        public Task<Schedule> Update(Schedule t)
        {
            throw new NotImplementedException();
        }
    }
}
