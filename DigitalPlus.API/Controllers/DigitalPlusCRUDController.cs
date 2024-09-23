using DigitalPlus.API.Model;
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

        public DigitalPlusCrudController(ICrudInterface<Module> moduleService, ICrudInterface<Department> departmentService, ICrudInterface<Course> courseService)
        {
            _moduleService = moduleService ?? throw new ArgumentNullException(nameof(moduleService));
            _departmentService = departmentService ?? throw new ArgumentNullException(nameof(departmentService));
            _courseService = courseService ?? throw new ArgumentNullException(nameof(courseService));
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
                    Message = "Unable Modules",
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


    }
}
