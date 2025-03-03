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

        public async Task ValidateCourseCodeAsync(string courseCode)
        {
            if (string.IsNullOrWhiteSpace(courseCode))
            {
                throw new ArgumentException("Course code cannot be null or empty.");
            }

            bool exists = await _digitalPlusDbContext.Courses.AnyAsync(m => m.Course_Code == courseCode);

            if (exists)
            {
                throw new InvalidOperationException($"Course code '{courseCode}' already exists.");
            }
        }

        public async Task<Course> Add(Course course)
        {
            await ValidateCourseCodeAsync(course.Course_Code);

            var exists = await _digitalPlusDbContext.Departments.FindAsync(course.Department_Id);

            if (exists == null)
            {
                throw new InvalidOperationException($"Deparment Id '{course.Department_Id}' does not exists.");
            }

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

        public async Task<List<Course>> GetCoursesByDepartmentId(int departmentId)
        {
            return await _digitalPlusDbContext.Courses
                .Where(c => c.Department_Id == departmentId)
                .ToListAsync();
        }
    }
}
