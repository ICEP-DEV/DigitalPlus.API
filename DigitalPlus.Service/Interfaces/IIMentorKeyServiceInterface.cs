using DigitalPlus.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Service.Interfaces
{
    public interface IIMentorKeyServiceInterface
    {
        Task<List<MentorKey>> GetKeysByMentorId(int mentorId);
        Task<MentorKey> GetKeyById(int keyId);
        Task<MentorKey> CreateMentorKey(MentorKey mentorKey);
        Task<MentorKey> UpdateMentorKey(int keyId, MentorKey updatedKey);
        Task<bool> DeleteMentorKey(int keyId);
        Task<bool> MentorExists(int mentorId);
        Task<List<MentorKey>> GetAllKeys();
    }
}
