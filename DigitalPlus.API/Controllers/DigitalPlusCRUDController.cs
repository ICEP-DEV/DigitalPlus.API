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
        
        public DigitalPlusCrudController(ICrudInterface<Module> moduleService)
        {
            _moduleService = moduleService;
        }

        [HttpPost]
        public async Task<IActionResult> AddModule([FromBody] Module module)
        {
            var results= await _moduleService.Add(module);    
            return View(results);
        }
    }
}
