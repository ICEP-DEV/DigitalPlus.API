﻿using DigitalPlus.API.Model;
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
    public class ModuleService : ICrudInterface<Module>
    {

        private readonly DigitalPlusDbContext _digitalPlusDbContext;

        public ModuleService(DigitalPlusDbContext digitalPlusDbContext)
        {
            _digitalPlusDbContext = digitalPlusDbContext
                ?? throw new ArgumentNullException(nameof(digitalPlusDbContext)); ;
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

        public async Task<Module> Get(int id)
        {
            var module = await _digitalPlusDbContext.Modules.FindAsync(id);
            if (module == null)
            {
                throw new KeyNotFoundException($"Module with ID {id} was not found.");
            }
            return module;
        }

        public async Task<IEnumerable<Module>> GetAll()
        {
           return await _digitalPlusDbContext.Modules.ToListAsync();
        }

        public Task<Module> Update(Module t)
        {
            throw new NotImplementedException();
        }
    }
}
