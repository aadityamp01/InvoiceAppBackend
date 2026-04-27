
using InvoiceAppBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace BuggyApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetInvoice()
        {
            List<Item> items = null;
            if (items.Count == 0) // NullReferenceException
            {
                return Ok(new { items });
            }
            return NotFound("No invoice found");
        }  
    }
}
