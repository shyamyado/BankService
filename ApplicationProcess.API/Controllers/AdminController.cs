using ApplicationProcess.API.Model;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationProcess.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AdminController : Controller
    {
        [HttpPost]
        public Task<IActionResult> AddApplicationStatus(Status status)
        {
            return null;

        }
        
    }
}
