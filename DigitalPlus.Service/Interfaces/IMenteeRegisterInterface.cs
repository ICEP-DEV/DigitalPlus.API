using DigitalPlus.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Service.Interfaces
{
    public interface IMenteeRegisterInterface
    {
        Task<MenteeRegister> AddMenteeRegister(MenteeRegister menteeRegister);
        Task<IEnumerable<MenteeRegister>> GetRegisterByMenteerId(int menteeId);

        Task<IEnumerable<MenteeRegister>> GetRegisterBymoduleId(int moduleId);

        Task<IEnumerable<MenteeRegister>> GetAll();
    }
}
