
using InvoiceAppBackend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace BuggyApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly string _connStr;

        public InvoiceController(IConfiguration config)
        {
            _connStr = config.GetConnectionString("DefaultConnection")!;

        }

        [HttpGet]
        public IActionResult GetInvoice()
        {
            var items = new List<Item>();

            using var conn = new SqlConnection(_connStr);
            conn.Open();

            using var cmd = new SqlCommand(
                "SELECT Name, Price FROM InvoiceItems", conn);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                items.Add(new Item
                {
                    name = reader.GetString(0),
                    price = reader.GetDecimal(1)
                });
            }

            if (items.Count == 0)
                return NotFound("No invoice found");

            return Ok(new { items });
        }
    }
}
