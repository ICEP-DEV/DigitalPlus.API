﻿using DigitalPlus.API.Model;
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

            // Check if the Mentee_Id already exists
            var existingMentee = await _dbcontext.Mentees.FindAsync(mentee.Mentee_Id);
            if (existingMentee != null)
            {
                throw new InvalidOperationException($"Mentee with ID {mentee.Mentee_Id} already exists.");
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
        // Method to update a Mentee
        public async Task<Mentee> Update(Mentee mentee)
        {
            if (mentee == null)
            {
                throw new ArgumentNullException(nameof(mentee), "Mentee object cannot be null");
            }

            // Fetch the existing mentee from the database
            var existingMentee = await _dbcontext.Mentees.FindAsync(mentee.Mentee_Id);
            if (existingMentee == null)
            {
                throw new KeyNotFoundException($"Mentee with ID {mentee.Mentee_Id} not found.");
            }

            // Use Entry().CurrentValues.SetValues() to update the mentee
            _dbcontext.Entry(existingMentee).CurrentValues.SetValues(mentee);

            // Save changes to the database
            await _dbcontext.SaveChangesAsync();

            return existingMentee; // Return the updated mentee
        }




        // Method to delete a Mentee
        public async Task<Mentee> Delete(Mentee mentee)
        {
            if (mentee == null)
            {
                throw new ArgumentNullException(nameof(mentee), "Mentee object cannot be null");
            }

            var existingMentee = await _dbcontext.Mentees.FindAsync(mentee.Mentee_Id);
            if (existingMentee == null)
            {
                throw new KeyNotFoundException($"Mentee with ID {mentee.Mentee_Id} not found.");
            }

            // Remove mentee from the database and save changes
            _dbcontext.Mentees.Remove(existingMentee);
            await _dbcontext.SaveChangesAsync();
            return mentee;
        }

        // Method to get mentee by email and password
        public async Task<Mentee> GetByEmailAndPassword(string email, string password)
        {
            return await _dbcontext.Mentees
                .FirstOrDefaultAsync(m => m.StudentEmail == email && m.Password == password);
        }

        public async Task<Mentee> GetByEmail(string email)
        {
            return await _dbcontext.Mentees
                .FirstOrDefaultAsync(m => m.StudentEmail == email);
        }
    }
}
