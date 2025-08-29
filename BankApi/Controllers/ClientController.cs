using Microsoft.AspNetCore.Mvc;
using BankApi.Data; 
using BankApi.Models;
using BankApi.Services;
using Microsoft.EntityFrameworkCore;

namespace BankApi.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ClientService _service;
        public ClientController(ClientService service)
        {
            _service = service;

        }

        [HttpGet]
        public async Task<IEnumerable<Client>> Get()
        {
            return await _service.GetAllClientsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetById(int id)
        {
            var client = await _service.GetClientByIdAsync(id);
            if (client is null)
            {
                return ClientNotFound(id);
            }

            return client;
        }

        [HttpPost]
        public async Task<IActionResult> CreateClientAsync(Client client)
        {
            var newClient = await _service.CreateClientAsync(client);
            return CreatedAtAction(nameof(GetById), new { id = newClient.ID }, newClient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Client client)
        {
            if (id != client.ID)
                return BadRequest();

            var clientToUpdate = await _service.GetClientByIdAsync(id);

            if (clientToUpdate is not null)
            {
                await _service.UpdateClientAsync(id, client);
                return NoContent();
            }
            else
            {
                return ClientNotFound(id);
            }
        }

        [HttpDelete("{id}")]
        public async Task <IActionResult> DeleteClientAsync(int id)
        {

            var clientToDelete = await _service.GetClientByIdAsync(id);

            if (clientToDelete is not null)
            {
                await _service.DeleteClientAsync(id);
                return NoContent();
            }
            else
            {
                return ClientNotFound(id);
            }
        }

        [NonAction]
        public NotFoundObjectResult ClientNotFound(int id)
        {
            return NotFound(new { Message = $"Client with ID {id} not found." });
        }



    }
}
