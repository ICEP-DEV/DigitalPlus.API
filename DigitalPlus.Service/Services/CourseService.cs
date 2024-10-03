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

        public async Task<Course> Delete(Course course)
        {
            if (course == null) throw new ArgumentNullException(nameof(course), "course object cannot be null.");

            var existingCourse = await _digitalPlusDbContext.Courses.FindAsync(course.Course_Id);
            if (existingCourse == null)
            {
                throw new KeyNotFoundException($"Course with ID {course.Course_Id} not found. ");
            }

            _digitalPlusDbContext.Courses.Remove(existingCourse);
            await _digitalPlusDbContext.SaveChangesAsync();
            return existingCourse;
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

        public async Task<Course> Update(Course course)
        {
            if (course == null) throw new ArgumentNullException(nameof(course), "course object cannot be null");

            var existingCourse = await _digitalPlusDbContext.Courses.FindAsync(course.Course_Id);
            if (existingCourse == null)
            {
                throw new KeyNotFoundException($"course with ID {course.Course_Id} not found");
            }


            _digitalPlusDbContext.Entry(existingCourse).CurrentValues.SetValues(course);
            await _digitalPlusDbContext.SaveChangesAsync();
            return course;
        }
    }
}
