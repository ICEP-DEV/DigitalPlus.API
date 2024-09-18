using DigitalPlus.API.Model;
using DigitalPlus.Data;
using DigitalPlus.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigitalPlus.Service.Services
{
    public class MenteeService : IIRegisterInterface<Mentee>
    {
        private readonly DigitalPlusDbContext _dbcontext;

        // Constructor that injects the DigitalPlusDbContext
        public MenteeService(DigitalPlusDbContext dbcontext)
        {
            _dbcontext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
        }
         
        // Method to register a new Mentee
        public async Task<Mentee> Register(Mentee mentee)
        {
            if (mentee == null)
            {
                throw new ArgumentNullException(nameof(mentee), "Mentee object cannot be null");
            }

            // Add new mentee to the database and save changes
            await _dbcontext.Mentees.AddAsync(mentee);
            await _dbcontext.SaveChangesAsync();
            return mentee;
        }

        // Method to get a single Mentee by ID
        public async Task<Mentee> Get(int id)
        {
            var mentee = await _dbcontext.Mentees.FindAsync(id);
            if (mentee == null)
            {
                throw new KeyNotFoundException($"Mentee with ID {id} was not found.");
            }
            return mentee;
        }

        // Method to get all Mentees
        public async Task<IEnumerable<Mentee>> GetAll()
        {
            // Retrieve all mentees from the database
            return await _dbcontext.Mentees.ToListAsync();
        }

        // Method to update a Mentee
        public async Task<Mentee> Update(Mentee mentee)
        {
            if (mentee == null)
            {
                throw new ArgumentNullException(nameof(mentee), "Mentee object cannot be null");
            }

            // Update mentee details in the database and save changes
            _dbcontext.Mentees.Update(mentee);
            await _dbcontext.SaveChangesAsync();
            return mentee;
        }

        // Method to delete a Mentee
        public async Task<Mentee> Delete(Mentee mentee)
        {
            if (mentee == null)
            {
                throw new ArgumentNullException(nameof(mentee), "Mentee object cannot be null");
            }

            // Remove mentee from the database and save changes
            _dbcontext.Mentees.Remove(mentee);
            await _dbcontext.SaveChangesAsync();
            return mentee;
        }
    }
}
