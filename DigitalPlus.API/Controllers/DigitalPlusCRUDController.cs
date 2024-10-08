using DigitalPlus.API.Model;
using DigitalPlus.Data;
using DigitalPlus.Data.Model;
using DigitalPlus.Service.Interfaces;
using DigitalPlus.Service.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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

        public DigitalPlusCrudController(ICrudInterface<Module> moduleService, ICrudInterface<Department> departmentService, ICrudInterface<Course> courseService, DigitalPlusDbContext digitalPlusDbContext, ICrudInterface<Complaint> complaintService, ICrudInterface<Appointment> appointmentService)
        {
            _moduleService = moduleService ?? throw new ArgumentNullException(nameof(moduleService));
            _departmentService = departmentService ?? throw new ArgumentNullException(nameof(departmentService));
            _courseService = courseService ?? throw new ArgumentNullException(nameof(courseService));
            _digitalPlusDbContext = digitalPlusDbContext;
            _complaintService = complaintService ?? throw new ArgumentNullException(nameof(complaintService));
            _complaintService = complaintService;
            _appointmentService = appointmentService;
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
           var respondWrapper= new RespondWrapper();
            var modules =await _moduleService.GetAll();

            if(modules.Count() > 0)
            {
                respondWrapper = new RespondWrapper
                {
                    Success = true,
                    Message="Successfully Got all Modules",
                    Result=modules
                };
            }
            else
            {
                respondWrapper = new RespondWrapper
                {
                    Success = false,
                    Message = "Unable to get Modules",
                    Result = modules
                };
            }
            return Ok(respondWrapper);
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
    }
}
