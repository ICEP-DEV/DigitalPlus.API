using DigitalPlus.API.Model;
using DigitalPlus.Data;
using DigitalPlus.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;  // Add for IList and IEnumerable
using System.Linq;
using System.Threading.Tasks;

namespace DigitalPlus.Service.Services
{
    public class AdminService : IIRegisterInterface<Administrator>
    {
        private readonly DigitalPlusDbContext _context;

        // Injecting the database context
        public AdminService(DigitalPlusDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Method to get administrator by email and password
        public async Task<Administrator> GetByEmailAndPassword(string email, string password)
        {
            // Assuming passwords are stored as plain text for simplicity (hash them in production)
            return await _context.Admins
                .FirstOrDefaultAsync(a => a.EmailAddress == email && a.Password == password);
        }

        // Get administrator by id
        public async Task<Administrator> Get(int id)
        {
            return await _context.Admins.FindAsync(id);
        }

        // Register a new administrator
        public async Task<Administrator> Register(Administrator admin)
        {
            await _context.Admins.AddAsync(admin);
            await _context.SaveChangesAsync();
            return admin;
        }

        // Update existing administrator
        public async Task<Administrator> Update(Administrator admin)
        {
            _context.Admins.Update(admin);
            await _context.SaveChangesAsync();
            return admin;
        }

        // Delete an administrator
        public async Task<bool> Delete(Administrator admin)
        {
            _context.Admins.Remove(admin);
            return await _context.SaveChangesAsync() > 0;
        }

        // Get all administrators (refactored to match the interface)
        public async Task<IEnumerable<Administrator>> GetAll()
        {
            return await _context.Admins.ToListAsync();
        }

        // Interface Method: Delete by returning the object itself instead of a bool
        async Task<Administrator> IIRegisterInterface<Administrator>.Delete(Administrator admin)
        {
            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();
            return admin;
        }

        // Interface Method: Get all administrators (matching the interface return type)
        async Task<IEnumerable<Administrator>> IIRegisterInterface<Administrator>.GetAll()
        {
            return await _context.Admins.ToListAsync();
        }


        public async Task<Administrator> GetByEmail(string email)
        {
            return await _context.Admins
                .FirstOrDefaultAsync(m => m.EmailAddress == email);
        }
    }
}
