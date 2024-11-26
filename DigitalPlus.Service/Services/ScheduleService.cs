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

    public class ScheduleService : IScheduleService
    {

        public readonly DigitalPlusDbContext _context;

        public  ScheduleService(DigitalPlusDbContext context)
        {
            _context = context;
        }

        public async Task<Schedule> CreateScheduleAsync(Schedule schedule)
        {
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();
            return schedule;
        }

        public async Task<bool> DeleteScheduleAsync(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null) return false;

            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSchedulesByMentorIdAsync(int mentorId)
        {
            var schedules = await _context.Schedules
           .Where(s => s.MentorId == mentorId)
           .ToListAsync();

            if (!schedules.Any()) return false;

            _context.Schedules.RemoveRange(schedules);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Schedule>> GetAllSchedulesAsync()
        {
            return await _context.Schedules.ToListAsync();
        }

        public async  Task<Schedule> GetScheduleByIdAsync(int id)
        {
            return await _context.Schedules.FindAsync(id);
        }

        public async Task<IEnumerable<Schedule>> GetSchedulesByMentorIdAsync(int mentorId)
        {
            return await _context.Schedules
            .Where(s => s.MentorId == mentorId)
            .ToListAsync();
        }

        public async Task<Schedule> UpdateScheduleAsync(Schedule schedule)
        {
           _context.Entry(schedule).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return schedule;
        }

        public async Task<bool> UpdateSchedulesByMentorIdAsync(int mentorId, Schedule updatedSchedule)
        {
            var schedules = await _context.Schedules
           .Where(s => s.MentorId == mentorId)
           .ToListAsync();

            if (!schedules.Any()) return false;

            foreach (var schedule in schedules)
            {
                schedule.TimeSlot = updatedSchedule.TimeSlot;
                schedule.DayOfTheWeek = updatedSchedule.DayOfTheWeek;
                schedule.ModuleList = updatedSchedule.ModuleList;
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }  
}