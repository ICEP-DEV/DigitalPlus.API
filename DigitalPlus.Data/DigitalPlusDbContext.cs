using DigitalPlus.API.Model;
using DigitalPlus.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace DigitalPlus.Data
{
    public class DigitalPlusDbContext: DbContext
    {
        public DigitalPlusDbContext(DbContextOptions<DigitalPlusDbContext> options) : base(options) { }
        public DbSet<Key> Keys { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Mentor> Mentors { get; set; }
        public DbSet<Mentee> Mentees { get; set; }
        public DbSet<Administrator> Admins { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<AssignMod> AssignMods { get; set; }
        public DbSet<Register> Registers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        
        // public DbSet<Status> statuses { get; set; }
    }
}
