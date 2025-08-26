using Microsoft.AspNetCore.Mvc;
using BankApi.Data; 
using BankApi.Models;
 
namespace BankApi.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : Controller
    {
        private readonly BankContext _context;
        public ClientController(BankContext context)
        {
            _context = context;

        }

        [HttpGet]
        public IEnumerable<Client> Get()
        {
            return _context.Clients.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Client> GetById(int id)
        {
            var client = _context.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }
            return client;
        }

        [HttpPost]
        public IActionResult Create(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = client.ID }, client);
        }

    }
}
