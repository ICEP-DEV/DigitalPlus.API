using DigitalPlus.API.Model;
using DigitalPlus.Service.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

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
            _moduleService = moduleService;
        }

        //adding a module
        [HttpPost]
        public async Task<IActionResult> AddModule([FromBody] Module module)
        {
            var results= await _moduleService.Add(module);    
            return View(results);
        }


        //adding a module
        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] Department department)
        {
            var results = await _departmentService.Add(department);
            return View(results);
        }

        //adding a module
        [HttpPost]
        public async Task<IActionResult> AddCourse([FromBody] Course course)
        {
            var results = await _courseService.Add(course);
            return View(results);
        }


    }
}
