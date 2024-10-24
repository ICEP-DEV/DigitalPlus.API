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

        public DbSet<MentorReport> MentorReports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply unique constraint to Mentee's StudentEmail
            modelBuilder.Entity<Mentee>()
                .HasIndex(m => m.StudentEmail)
                .IsUnique();

            // Apply unique constraint to Mentor's StudentEmail and PersonalEmail
            modelBuilder.Entity<Mentor>()
                .HasIndex(m => m.StudentEmail)
                .IsUnique();

            modelBuilder.Entity<Mentor>()
                .HasIndex(m => m.PersonalEmail)
                .IsUnique();

            // Apply unique constraint to Administrator's EmailAddress
            modelBuilder.Entity<Administrator>()
                .HasIndex(a => a.EmailAddress)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
