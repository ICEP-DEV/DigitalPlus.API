using DigitalPlus.API.Model;
using DigitalPlus.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Service.Interfaces
{
    public interface IAssignModService<T>
    {
        Task<T> CreateAssignMod(AssignModDto assignModDto);
        Task<IEnumerable<T>> GetAssignedModulesByMentorId(int mentorId); // Use this method only
        Task<bool> DeleteAssignedModule(int moduleId);
        Task<T> UpdateAssignedModule(AssignModDto assignModDto);
       

    }

}
