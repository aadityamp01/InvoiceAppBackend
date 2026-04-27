
using InvoiceAppBackend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

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
        public async Task<IActionResult> GetData()
        {
            var invoiceDict = new Dictionary<int, Invoice>();

            await using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();

            var query = @"
                SELECT i.InvoiceID, i.CustomerName, it.Name, it.Price
                FROM Invoices i
                LEFT JOIN InvoiceItems it ON i.InvoiceID = it.InvoiceID";

            await using var cmd = new SqlCommand(query, conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                int invoiceId = reader.GetInt32(0);

                if (!invoiceDict.ContainsKey(invoiceId))
                {
                    invoiceDict[invoiceId] = new Invoice
                    {
                        InvoiceID = invoiceId,
                        CustomerName = reader.GetString(1),
                        InvoiceItems = new List<Item>()
                    };
                }

                if (!reader.IsDBNull(2))
                {
                    invoiceDict[invoiceId].InvoiceItems.Add(new Item
                    {
                        name = reader.GetString(2),
                        price = reader.GetDecimal(3)
                    });
                }
            }

            var result = invoiceDict.Values.ToList();

            if (result.Count == 0)
                return NotFound("No data found.");

            return Ok(result);
        }
    }
}
