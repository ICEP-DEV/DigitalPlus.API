using DigitalPlus.API.Model;
using DigitalPlus.Data;
using DigitalPlus.Data.Model;
using DigitalPlus.Service.Interfaces;
using DigitalPlus.Service.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Module = DigitalPlus.API.Model.Module;

namespace DigitalPlus.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [EnableCors("corspolicy")]
    [ApiController]
    public class DigitalPlusCrudController : Controller
    {

        private readonly ICrudInterface<Module> _moduleService;
        private readonly ICrudInterface<Department> _departmentService;
        private readonly ICrudInterface<Course> _courseService;
        private readonly DigitalPlusDbContext _digitalPlusDbContext;
        private readonly ICrudInterface<Complaint> _complaintService;
        private readonly ICrudInterface<Appointment> _appointmentService;
        private readonly IScheduleService _scheduleService;

        public DigitalPlusCrudController(ICrudInterface<Module> moduleService, ICrudInterface<Department> departmentService, ICrudInterface<Course> courseService, DigitalPlusDbContext digitalPlusDbContext, ICrudInterface<Complaint> complaintService, ICrudInterface<Appointment> appointmentService, IScheduleService scheduleService)
        {
            _moduleService = moduleService ?? throw new ArgumentNullException(nameof(moduleService));
            _departmentService = departmentService ?? throw new ArgumentNullException(nameof(departmentService));
            _courseService = courseService ?? throw new ArgumentNullException(nameof(courseService));
            _digitalPlusDbContext = digitalPlusDbContext;
            _complaintService = complaintService ?? throw new ArgumentNullException(nameof(complaintService));
            _complaintService = complaintService;
            _appointmentService = appointmentService;
            _scheduleService = scheduleService;

        }

        //adding a module
        [HttpPost]
        public async Task<IActionResult> AddModule([FromBody] Module module)
        {
            var results= await _moduleService.Add(module);    
            return Ok(results);
        }

        //Get all Modules
        [HttpGet]
        public async Task<IActionResult> GetAllModules()
        {
            var modules =await _moduleService.GetAll();
            return Ok(modules);
        }

        //Get a module by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetModule(int id) { 
            var respondWrapper= new RespondWrapper();
            var module= await _moduleService.Get(id);
            if(module != null)
            {
                respondWrapper = new RespondWrapper
                {
                    Success = true,
                    Message = "Successfully Got a Module",
                    Result = module
                };

               
            }
            else
            {
                return NotFound();
            }

            return Ok(respondWrapper);
        }


        //Delete Module
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModule(int id)
        {
            var respondWrapper= new RespondWrapper();
            var module = await _moduleService.Get(id);

            if(module != null)
            {
                var result = await _moduleService.Delete(module);
                respondWrapper = new RespondWrapper
                {
                    Success = true,
                    Message = "Successfully deleted a Module",
                    Result = result
                };
            }
            else
            {
                return NotFound();
            }

            return Ok(respondWrapper);
        }

        //Update a Module
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateModule(int id,[FromBody] Module module)
        {
            var respondWrapper= new RespondWrapper();

            if (module == null || module.Module_Id != id)
            {
                return BadRequest("Module object is not null or ID mismatch");
            }
           
           var updateModule=await _moduleService.Update(module);
            if(updateModule == null)
            {
                return NotFound();
            }
            else
            {
                respondWrapper = new RespondWrapper
                {
                    Success = true,
                    Message = "Successfully updated a Module",
                    Result = updateModule
                };
            } 
            
            return Ok(respondWrapper);
        }

        // GET: api/modules/course/{courseId}
        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetModulesByCourseId(int courseId)
        {
            var modules = await _digitalPlusDbContext.Modules
                .Where(m => m.Course_Id == courseId)
                .ToListAsync();

            if (modules == null || !modules.Any())
            {
                return NotFound("No modules found for this course.");
            }

            return Ok(modules);
        }

        //adding a Department
        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] Department department)
        {
            var results = await _departmentService.Add(department);
            return Ok(results);
        }

        //Get all Departments
        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var respondWrapper = new RespondWrapper();
            var departments = await _departmentService.GetAll();

            if (departments.Count() > 0)
            {
                respondWrapper = new RespondWrapper
                {
                    Success = true,
                    Message = "Successfully Got all Departments",
                    Result = departments
                };
            }
            else
            {
                respondWrapper = new RespondWrapper
                {
                    Success = false,
                    Message = "Unable to Departments",
                    Result = departments
                };
            }
            return Ok(respondWrapper);
        }


        //Get a Department by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartment(int id)
        {
            var respondWrapper = new RespondWrapper();
            var department = await _departmentService.Get(id);
            if (department != null)
            {
                respondWrapper = new RespondWrapper
                {
                    Success = true,
                    Message = "Successfully Got a Module",
                    Result = department
                };


            }
            else
            {
                return NotFound();
            }

            return Ok(respondWrapper);
        }

        //Delete Department
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var respondWrapper = new RespondWrapper();
            var department = await _departmentService.Get(id);

            if (department != null)
            {
                var result = await _departmentService.Delete(department);
                respondWrapper = new RespondWrapper
                {
                    Success = true,
                    Message = "Successfully deleted a department",
                    Result = result
                };
            }
            else
            {
                return NotFound();
            }

            return Ok(respondWrapper);
        }

        //Update a Departments
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] Department department)
        {
            var respondWrapper = new RespondWrapper();

            if (department == null || department.Department_Id != id)
            {
                return BadRequest("Department object is not null or ID mismatch");
            }

            var updateDepartment = await _departmentService.Update(department);
            if (updateDepartment == null)
            {
                return NotFound();
            }
            else
            {
                respondWrapper = new RespondWrapper
                {
                    Success = true,
                    Message = "Successfully updated a Department",
                    Result = updateDepartment
                };
            }

            return Ok(respondWrapper);
        }

        //adding a Course
        [HttpPost]
        public async Task<IActionResult> AddCourse([FromBody] Course course)
        {
            var results = await _courseService.Add(course);
            return Ok(results);
        }


        //Get all Courses
        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var respondWrapper = new RespondWrapper();
            var courses = await _courseService.GetAll();

            if (courses.Count() > 0)
            {
                respondWrapper = new RespondWrapper
                {
                    Success = true,
                    Message = "Successfully Got all Courses",
                    Result = courses
                };
            }
            else
            {
                respondWrapper = new RespondWrapper
                {
                    Success = false,
                    Message = "Unable Courses",
                    Result = courses
                };
            }
            return Ok(respondWrapper);
        }

        //Get a Course by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourse(int id)
        {
            var respondWrapper = new RespondWrapper();
            var course = await _departmentService.Get(id);
            if (course != null)
            {
                respondWrapper = new RespondWrapper
                {
                    Success = true,
                    Message = "Successfully Got a Module",
                    Result = course
                };


            }
            else
            {
                return NotFound();
            }

            return Ok(respondWrapper);
        }

        //Delete Course
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var respondWrapper = new RespondWrapper();
            var course = await _courseService.Get(id);

            if (course != null)
            {
                var result = await _courseService.Delete(course);
                respondWrapper = new RespondWrapper
                {
                    Success = true,
                    Message = "Successfully deleted a course",
                    Result = result
                };
            }
            else
            {
                return NotFound();
            }

            return Ok(respondWrapper);
        }

        //Update a Course
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] Course course)
        {
            var respondWrapper = new RespondWrapper();

            if (course == null || course.Course_Id != id)
            {
                return BadRequest("Course object is not null or ID mismatch");
            }

            var updateCourse = await _courseService.Update(course);
            if (updateCourse == null)
            {
                return NotFound();
            }
            else
            {
                respondWrapper = new RespondWrapper
                {
                    Success = true,
                    Message = "Successfully updated a Course",
                    Result = updateCourse
                };
            }

            return Ok(respondWrapper);
        }
        //Get a course via depatmentID
        [HttpGet("department/{departmentId}")]
        public async Task<IActionResult> GetCoursesByDepartment(int departmentId)
        {
            var courses = await _digitalPlusDbContext.Courses
                .Where(c => c.Department_Id == departmentId)
                .ToListAsync();

            if (courses == null || !courses.Any())
            {
                return NotFound("No courses found for this department.");
            }

            return Ok(courses);
        }


        //Add compaint
        [HttpPost]
        public async Task<IActionResult> AddComplaint([FromBody] Complaint complaint)
        {
            var result = await _complaintService.Add(complaint);
            return Ok(result);
        }

        // Get all Complaints
        [HttpGet]
        public async Task<IActionResult> GetAllComplaints()
        {
            var respondWrapper = new RespondWrapper();
            var complaints = await _complaintService.GetAll();

            if (complaints.Count() > 0)
            {
                respondWrapper = new RespondWrapper
                {
                    Success = true,
                    Message = "Successfully Got all Complaints",
                    Result = complaints
                };
            }
            else
            {
                respondWrapper = new RespondWrapper
                {
                    Success = false,
                    Message = "Unable to get Complaints",
                    Result = complaints
                };
            }
            return Ok(respondWrapper);
        }

        // Get a Complaint by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComplaint(int id)
        {
            var respondWrapper = new RespondWrapper();
            var complaint = await _complaintService.Get(id);

            if (complaint != null)
            {
                respondWrapper = new RespondWrapper
                {
                    Success = true,
                    Message = "Successfully Got a Complaint",
                    Result = complaint
                };
            }
            else
            {
                return NotFound();
            }

            return Ok(respondWrapper);
        }

        // Delete a Complaint
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComplaint(int id)
        {
            var respondWrapper = new RespondWrapper();
            var complaint = await _complaintService.Get(id);

            if (complaint != null)
            {
                var result = await _complaintService.Delete(complaint);
                respondWrapper = new RespondWrapper
                {
                    Success = true,
                    Message = "Successfully deleted a Complaint",
                    Result = result
                };
            }
            else
            {
                return NotFound();
            }

            return Ok(respondWrapper);
        }

        // Update a Complaint
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComplaint(int id, [FromBody] Complaint complaint)
        {
            var respondWrapper = new RespondWrapper();

            if (complaint == null || complaint.ComplaintId != id)
            {
                return BadRequest("Complaint object is null or ID mismatch");
            }

            var updateComplaint = await _complaintService.Update(complaint);
            if (updateComplaint == null)
            {
                return NotFound();
            }
            else
            {
                respondWrapper = new RespondWrapper
                {
                    Success = true,
                    Message = "Successfully updated a Complaint",
                    Result = updateComplaint
                };
            }

            return Ok(respondWrapper);
        }


        //Appointment CRUD

        //adding an appointment
        
        [HttpPost]
        public async Task<IActionResult> AddAppointment([FromBody] Appointment appointment)
        {
            var results = await _appointmentService.Add(appointment);
            return Ok(results);
        }

        // Delete a appointments
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var respondWrapper = new RespondWrapper();
            var appointment = await _appointmentService.Get(id);

            if (appointment != null)
            {
                var result = await _appointmentService.Delete(appointment);
                respondWrapper = new RespondWrapper
                {
                    Success = true,
                    Message = "Successfully deleted a Appointment",
                    Result = result
                };
            }
            else
            {
                return NotFound();
            }

            return Ok(respondWrapper);
        }

        // Get a Appointment by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointment(int id)
        {
            var respondWrapper = new RespondWrapper();
            var appointment = await _appointmentService.Get(id);

            if (appointment != null)
            {
                respondWrapper = new RespondWrapper
                {
                    Success = true,
                    Message = "Successfully Got a appointment",
                    Result = appointment
                };
            }
            else
            {
                return NotFound();
            }

            return Ok(respondWrapper);
        }

        // Get all appointment
        [HttpGet]
        public async Task<IActionResult> GetAllAppointments()
        {
            var respondWrapper = new RespondWrapper();
            var appointment = await _appointmentService.GetAll();

            if (appointment.Count() > 0)
            {
                respondWrapper = new RespondWrapper
                {
                    Success = true,
                    Message = "Successfully Got all appointment",
                    Result = appointment
                };
            }
            else
            {
                respondWrapper = new RespondWrapper
                {
                    Success = false,
                    Message = "Unable to get Complaints",
                    Result = appointment
                };
            }
            return Ok(respondWrapper);
        }





        //SCHEDULES

        [HttpPost]
        public async Task<ActionResult<Schedule>> CreateSchedule(Schedule schedule)
        {
            var createdSchedule = await _scheduleService.CreateScheduleAsync(schedule);
            return CreatedAtAction(nameof(GetSchedule), new { id = createdSchedule.ScheduleId }, createdSchedule);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetAllSchedules()
        {
            var schedules = await _scheduleService.GetAllSchedulesAsync();
            return Ok(schedules);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Schedule>> GetSchedule(int id)
        {
            var schedule = await _scheduleService.GetScheduleByIdAsync(id);
            if (schedule == null) return NotFound();
            return Ok(schedule);
        }

        // New endpoint to get schedules by MentorId
        [HttpGet("mentor/{mentorId}")]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetSchedulesByMentorId(int mentorId)
        {
            var schedules = await _scheduleService.GetSchedulesByMentorIdAsync(mentorId);
            return Ok(schedules);
        }

        

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSchedule(int id, Schedule schedule)
        {
            if (id != schedule.ScheduleId) return BadRequest();

            await _scheduleService.UpdateScheduleAsync(schedule);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSchedule(int id)
        {
            var result = await _scheduleService.DeleteScheduleAsync(id);
            if (!result) return NotFound();

            return NoContent();
        }

        [HttpPut("mentor/{mentorId}")]
        public async Task<ActionResult> UpdateSchedulesByMentorId(int mentorId, Schedule updatedSchedule)
        {
            var result = await _scheduleService.UpdateSchedulesByMentorIdAsync(mentorId, updatedSchedule);
            if (!result) return NotFound();

            return NoContent();
        }

        // New endpoint to delete schedules by MentorId
        [HttpDelete("mentor/{mentorId}")]
        public async Task<ActionResult> DeleteSchedulesByMentorId(int mentorId)
        {
            var result = await _scheduleService.DeleteSchedulesByMentorIdAsync(mentorId);
            if (!result) return NotFound();

            return NoContent();
        }
    }
}
