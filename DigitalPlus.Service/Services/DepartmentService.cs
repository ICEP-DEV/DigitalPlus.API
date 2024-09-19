using DigitalPlus.API.Model;
using DigitalPlus.Data;
using DigitalPlus.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Service.Services
{
    public class DepartmentService : ICrudInterface<Department>
    {
        private readonly DigitalPlusDbContext _digitalPlusDbContext;

        public DepartmentService(DigitalPlusDbContext digitalPlusDbContext)
        {
            _digitalPlusDbContext = digitalPlusDbContext;
        }
        public async Task<Department> Add(Department department)
        {
            await _digitalPlusDbContext.Departments.AddAsync(department);
            await _digitalPlusDbContext.SaveChangesAsync();
            return department;
        }

        public Task<Department> Delete(Department t)
        {
            throw new NotImplementedException();
        }

        public Task<Department> Get(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Department>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Department> Update(Department t)
        {
            throw new NotImplementedException();
        }
    }
}
