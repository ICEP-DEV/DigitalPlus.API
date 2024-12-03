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

        Task<IEnumerable<MentorRegister>> GetAll();
    }
}
