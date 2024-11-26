using DigitalPlus.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Service.Interfaces
{
    public interface IScheduleService
    {
        Task<IEnumerable<Schedule>> GetAllSchedulesAsync();
        Task<Schedule> GetScheduleByIdAsync(int id);
        Task<Schedule> CreateScheduleAsync(Schedule schedule);
        Task<Schedule> UpdateScheduleAsync(Schedule schedule);
        Task<bool> DeleteScheduleAsync(int id);
        Task<IEnumerable<Schedule>> GetSchedulesByMentorIdAsync(int mentorId);
        Task<bool> UpdateSchedulesByMentorIdAsync(int mentorId, Schedule updatedSchedule);  // New method for bulk update by MentorId
        Task<bool> DeleteSchedulesByMentorIdAsync(int mentorId);
    }
}
