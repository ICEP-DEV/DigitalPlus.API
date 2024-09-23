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
    public class CourseService : ICrudInterface<Course>
    {

        private readonly DigitalPlusDbContext _digitalPlusDbContext;

        public CourseService(DigitalPlusDbContext digitalPlusDbContext)
        {
            _digitalPlusDbContext = digitalPlusDbContext;
        }

        public async Task<Course> Add(Course course)
        {
            await _digitalPlusDbContext.Courses.AddAsync(course);
           await _digitalPlusDbContext.SaveChangesAsync();
            return course;
        }

        public Task<Course> Delete(Course t)
        {
            throw new NotImplementedException();
        }

        public async Task<Course> Get(int id)
        {
            var course = await _digitalPlusDbContext.Courses.FindAsync(id);
            if (course == null)
            {
                throw new KeyNotFoundException($"Course with ID {id} was not found.");
            }
            return course;
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            return await _digitalPlusDbContext.Courses.ToListAsync();
        }

        public Task<Course> Update(Course t)
        {
            throw new NotImplementedException();
        }
    }
}
