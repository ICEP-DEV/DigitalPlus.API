using DigitalPlus.API.Model;
using DigitalPlus.Data;
using DigitalPlus.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigitalPlus.Service.Services
{
    public class MentorService : IIRegisterInterface<Mentor>
    {
        private readonly DigitalPlusDbContext _dbcontext;

        // Constructor that injects the DigitalPlusDbContext
        public MentorService(DigitalPlusDbContext dbcontext)
        {
            _dbcontext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
        }

        // Method to get a mentor by ID
        public async Task<Mentor> Get(int id)
        {
            var mentor = await _dbcontext.Mentors.FindAsync(id);
            if (mentor == null)
            {
                throw new KeyNotFoundException($"Mentor with ID {id} not found.");
            }
            return mentor;
        }

        // Method to get all mentors
        public async Task<IEnumerable<Mentor>> GetAll()
        {
            return await _dbcontext.Mentors.ToListAsync();
        }

        // Method to add a mentor to the database (register)
        public async Task<Mentor> Register(Mentor mentor)
        {
            if (mentor == null)
            {
                throw new ArgumentNullException(nameof(mentor), "Mentor object cannot be null");
            }

            // Check if the mentor ID is already in use
            var existingMentor = await _dbcontext.Mentors.FindAsync(mentor.MentorId);
            if (existingMentor != null)
            {
                throw new InvalidOperationException($"Mentor with ID {mentor.MentorId} already exists.");
            }

            // Add mentor to the database
            await _dbcontext.Mentors.AddAsync(mentor);
            await _dbcontext.SaveChangesAsync();
            return mentor;
        }

        // Method to update a mentor
        public async Task<Mentor> Update(Mentor mentor)
        {
            if (mentor == null)
            {
                throw new ArgumentNullException(nameof(mentor), "Mentor object cannot be null");
            }

            var existingMentor = await _dbcontext.Mentors.FindAsync(mentor.MentorId);
            if (existingMentor == null)
            {
                throw new KeyNotFoundException($"Mentor with ID {mentor.MentorId} not found.");
            }

            _dbcontext.Entry(existingMentor).CurrentValues.SetValues(mentor);
            await _dbcontext.SaveChangesAsync();
            return mentor;
        }

        // Method to delete a mentor
        public async Task<Mentor> Delete(Mentor mentor)
        {
            if (mentor == null)
            {
                throw new ArgumentNullException(nameof(mentor), "Mentor object cannot be null");
            }

            var existingMentor = await _dbcontext.Mentors.FindAsync(mentor.MentorId);
            if (existingMentor == null)
            {
                throw new KeyNotFoundException($"Mentor with ID {mentor.MentorId} not found.");
            }

            _dbcontext.Mentors.Remove(existingMentor);
            await _dbcontext.SaveChangesAsync();
            return existingMentor;
        }

        // Method to get mentor by email and password
        public async Task<Mentor> GetByEmailAndPassword(string email, string password)
        {
            return await _dbcontext.Mentors
                .FirstOrDefaultAsync(m => m.StudentEmail == email && m.Password == password);
        }

        public async Task<Mentor> GetByEmail(string email)
        {
            return await _dbcontext.Mentors
                .FirstOrDefaultAsync(m => m.StudentEmail == email);
        }
    }
}
