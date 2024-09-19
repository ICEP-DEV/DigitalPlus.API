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
    public class ModuleService : ICrudInterface<Module>
    {

        private readonly DigitalPlusDbContext _digitalPlusDbContext;

        public ModuleService(DigitalPlusDbContext digitalPlusDbContext)
        {
            _digitalPlusDbContext = digitalPlusDbContext;
        }

        public async Task<Module> Add(Module module)
        {

            await _digitalPlusDbContext.Modules.AddAsync(module);
            await _digitalPlusDbContext.SaveChangesAsync();
            return module;
        }

        public Task<Module> Delete(Module module)
        {
            throw new NotImplementedException();
        }

        public Task<Module> Get(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Module>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Module> Update(Module t)
        {
            throw new NotImplementedException();
        }
    }
}
