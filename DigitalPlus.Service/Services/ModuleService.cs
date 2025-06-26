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
    public class ModuleService : ICrudInterface<LearningModule>
    {

        private readonly DigitalPlusDbContext _digitalPlusDbContext;

        public ModuleService(DigitalPlusDbContext digitalPlusDbContext)
        {
            _digitalPlusDbContext = digitalPlusDbContext
                ?? throw new ArgumentNullException(nameof(digitalPlusDbContext)); ;
        }


        public async Task ValidateModuleCodeAsync(string moduleCode)
        {
            if (string.IsNullOrWhiteSpace(moduleCode))
            {
                throw new ArgumentException("Module code cannot be null or empty.");
            }

            bool exists = await _digitalPlusDbContext.Modules.AnyAsync(m => m.Module_Code == moduleCode);

            if (exists)
            {
                throw new InvalidOperationException($"Module code '{moduleCode}' already exists.");
            }
        }
       
        public async Task<LearningModule> Add(LearningModule module)
        {
            await ValidateModuleCodeAsync(module.Module_Code);
            var exists = await _digitalPlusDbContext.Courses.FindAsync(module.Course_Id);

            if (exists == null)
            {
                throw new InvalidOperationException($"Course Id '{module.Course_Id}' does not exists.");
            }

            await _digitalPlusDbContext.Modules.AddAsync(module);
            await _digitalPlusDbContext.SaveChangesAsync();
            return module;
        }

        public async Task<LearningModule> Delete(LearningModule module)
        {
            if (module == null) throw new ArgumentNullException(nameof(module),"Module object cannot be null.");

            var existingModule=await _digitalPlusDbContext.Modules.FindAsync(module.Module_Id);
            if (existingModule == null)
            {
                throw new KeyNotFoundException($"Module with ID {module.Module_Id} not found. ");
            }

            _digitalPlusDbContext.Modules.Remove(existingModule);
            await _digitalPlusDbContext.SaveChangesAsync();
            return existingModule;
        }

        public async Task<LearningModule> Get(int id)
        {
            var module = await _digitalPlusDbContext.Modules.FindAsync(id);
            if (module == null)
            {
                throw new KeyNotFoundException($"Module with ID {id} was not found.");
            }
            return module;
        }

        public async Task<IEnumerable<LearningModule>> GetAll()
        {
           return await _digitalPlusDbContext.Modules.ToListAsync();
        }

        public async Task<LearningModule> Update(LearningModule module)
        {
            if(module == null) throw new ArgumentNullException(nameof (module), "Module object cannot be null");

            var existingModule = await _digitalPlusDbContext.Modules.FindAsync(module.Module_Id);
            if(existingModule == null)
            {
                throw new KeyNotFoundException($"Module with ID {module.Module_Id} not found");
            }

          
            _digitalPlusDbContext.Entry(existingModule).CurrentValues.SetValues(module);
            await _digitalPlusDbContext.SaveChangesAsync();
            return module;
        }

        
      
    }
}
