using DigitalPlus.API.Model;
using DigitalPlus.Data;
using DigitalPlus.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
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
            // Hash the password or compare based on how passwords are stored
            // Assuming passwords are stored as plain text for simplicity (not recommended in production)
            var admin = await Task.Run(() =>
                _context.Admins
                    .FirstOrDefault(a => a.EmailAddress == email && a.Password == password)
            );

            return admin;
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

        // Get all administrators
        public async Task<IList<Administrator>> GetAll()
        {
            return await _context.Admins.ToListAsync();
        }

        Task<Administrator> IIRegisterInterface<Administrator>.Delete(Administrator t)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Administrator>> IIRegisterInterface<Administrator>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
