using BankApi.Data;
using BankApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
//por fin termine por hoy
namespace BankApi.Services

{
    public class ClientService
    {
        private readonly BankContext _context;
        public ClientService(BankContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client?> GetClientByIdAsync(int id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task<Client> CreateClientAsync(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task UpdateClientAsync(int id, Client client)
        {
            var existingClient = await GetClientByIdAsync(id);
            if (existingClient is null)
            {
                throw new KeyNotFoundException("Client not found");
            }
            else
            {
                existingClient.Name = client.Name;
                existingClient.PhoneNumber = client.PhoneNumber;
                existingClient.Email = client.Email;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteClientAsync(int id)
        {
            var clientToDelete = await GetClientByIdAsync(id);

            if (clientToDelete is not null)
            {
                _context.Clients.Remove(clientToDelete);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Client not found");
            }
        }
    }
}
