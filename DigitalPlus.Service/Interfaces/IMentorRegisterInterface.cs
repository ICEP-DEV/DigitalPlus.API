using DigitalPlus.Data.Dto;
using DigitalPlus.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Service.Interfaces
{
    public interface IMentorRegisterInterface
    {
        Task<MentorRegister> AddRegister(InsertMentorRegisterDto insertMentorRegisterDto);
        Task<IEnumerable<MentorRegister>> GetRegisterByMentorId(int mentorId);

        Task<IEnumerable<MentorRegister>> GetRegiserBymoduleId(int moduleId);

        Task<IEnumerable<MentorRegister>> GetAll();

        Task<IEnumerable<MentorRegister>> GetRegisterByStatusAandModuleId(bool activation,int moduleId);

        Task<IEnumerable<MentorRegister>> GetRegisterByStatus(bool activation);

        Task<IEnumerable<MentorRegister>> GetRegisterByMentorIdAandModuleId(int mentorId, int moduleId);

        Task<IEnumerable<MentorRegister>> GetRegisterById(int id);
    }
}
