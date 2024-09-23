using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Service.Interfaces
{
    public interface ICrudInterface<T>
    {
        Task<T> Add(T t);
        Task<T> Get(int id);
        Task<T> Update(T t);
        Task<T> Delete(T t);
        Task<IEnumerable<T>> GetAll();
    }
}
