using DigitalPlus.API.Model;
using DigitalPlus.Data;
using DigitalPlus.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Department> Delete(Department department)
        {
            if (department == null) throw new ArgumentNullException(nameof(department), "department object cannot be null.");

            var existingDepartment = await _digitalPlusDbContext.Departments.FindAsync(department.Department_Id);
            if (existingDepartment == null)
            {
                throw new KeyNotFoundException($"Department with ID {department.Department_Id} not found. ");
            }

            _digitalPlusDbContext.Departments.Remove(existingDepartment);
            await _digitalPlusDbContext.SaveChangesAsync();
            return existingDepartment;
        }

        public async Task<Department> Get(int id)
        {

            var department = await _digitalPlusDbContext.Departments.FindAsync(id);
            if (department == null)
            {
                throw new KeyNotFoundException($"Department with ID {id} was not found.");
            }
            return department;
        }

        public async Task<IEnumerable<Department>> GetAll()
        {
            return await _digitalPlusDbContext.Departments.ToListAsync();
        }

        public Task<Department> Update(Department t)
        {
            throw new NotImplementedException();
        }
    }
}
