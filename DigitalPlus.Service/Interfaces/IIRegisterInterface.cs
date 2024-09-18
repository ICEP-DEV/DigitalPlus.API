using DigitalPlus.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Service.Interfaces
{
    public interface IIRegisterInterface<T>
    {
        Task<T> Register(T t);
        Task<T> Get(int Id);
        Task<T> Update(T t);
        Task<T> Delete(T t);
        Task<IEnumerable<T>> GetAll();

        Task<T> GetByEmailAndPassword(string email, string password);
    }
}
