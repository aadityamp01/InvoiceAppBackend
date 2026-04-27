
using Microsoft.AspNetCore.Mvc;

namespace BuggyApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        private readonly string _connectionString;
        public DataController(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public IActionResult GetData()
        {
            string result = null;
            if (!string.IsNullOrEmpty(result)) 
            {
                return Ok(new { message = "Data fetched" });
            }
            return NotFound("No data");
        }
    }
}
