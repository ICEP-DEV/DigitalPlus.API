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

        public Task<Course> Get(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Course>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Course> Update(Course t)
        {
            throw new NotImplementedException();
        }
    }
}
