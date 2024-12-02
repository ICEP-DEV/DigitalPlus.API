using DigitalPlus.Data;
using DigitalPlus.Data.Model;
using DigitalPlus.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Service.Services
{
    public class MentorKeyService : IIMentorKeyServiceInterface
    {
        private readonly DigitalPlusDbContext _dbContext;

        public MentorKeyService(DigitalPlusDbContext dbContext)
        {
            _dbContext = dbContext;
        }

      
        public async Task<MentorKey> CreateMentorKey(MentorKey mentorKey)
        {
            mentorKey.Date = DateTime.UtcNow;
            _dbContext.MentorKeys.Add(mentorKey);
            await _dbContext.SaveChangesAsync();
            return mentorKey;
        }

        public async Task<bool> DeleteMentorKey(int keyId)
        {
            var key = await _dbContext.MentorKeys.FindAsync(keyId);
            if (key == null) return false;

            _dbContext.MentorKeys.Remove(key);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<MentorKey>> GetAllKeys()
        {
            return await _dbContext.MentorKeys.ToListAsync();
        }

        public async Task<MentorKey> GetKeyById(int keyId)
        {
            return await _dbContext.MentorKeys.FindAsync(keyId);
        }

        public async Task<List<MentorKey>> GetKeysByMentorId(int mentorId)
        {
            return await _dbContext.MentorKeys
           .Where(k => k.MentorId == mentorId)
           .ToListAsync();
        }

        public async Task<bool> MentorExists(int mentorId)
        {
            return await _dbContext.Mentors.AnyAsync(m => m.MentorId == mentorId);
        }

        public Task<MentorKey> UpdateMentorKey(int keyId, MentorKey updatedKey)
        {
            throw new NotImplementedException();
        }
    }
}
