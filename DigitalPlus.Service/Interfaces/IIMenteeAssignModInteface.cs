using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Service.Interfaces
{
    public interface IIMenteeAssignModInteface<T>
    {
        Task<T> CreateAssignMod(T t);
        Task<IEnumerable<T>> GetAssignedModulesByMenteeId(int menteeId);

        Task<bool> DeleteAssignedModule(int moduleId);

        Task<T> UpdateAssignedModule(T t);
    }
}
