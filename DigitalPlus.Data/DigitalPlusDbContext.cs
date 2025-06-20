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

        public DbSet<ChatMessage> ChatMessages{ get; set; }
        public DbSet<Mentor> Mentors { get; set; }
        public DbSet<Mentee> Mentees { get; set; }
        public DbSet<Administrator> Admins { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<AssignMod> AssignMods { get; set; }
        public DbSet<MenteeRegister> Registers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Booking> Bookings{ get; set; }

        public DbSet<MentorReport> MentorReports { get; set; }

        public DbSet<MenteeAssignModule> MenteeAssignModules { get; set; }

        public DbSet<Announcement> Announcements { get; set; }

        public DbSet<MentorKey> MentorKeys { get; set; }

        public DbSet<MentorRegister> MentorRegisters { get; set; }  

        public DbSet<MenteeRegister> MenteeRegisters { get; set; }

        public DbSet<Questions> Questions { get; set; }

        public DbSet<MessageAttachment> MessageAttachments { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MentorNotification> MentorNotifications { get; set; }



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
