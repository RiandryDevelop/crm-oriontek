using CRM_OrionTek.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CRM_OrionTek.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly DbContext _context;

        public ClientRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            return await _context.Set<Client>().FindAsync(id);
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            return await _context.Set<Client>().ToListAsync();
        }

        public async Task AddClientAsync(Client client)
        {
            await _context.Set<Client>().AddAsync(client);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClientAsync(Client client)
        {
            _context.Set<Client>().Update(client);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteClientAsync(int id)
        {
            var client = await GetClientByIdAsync(id);
            _context.Set<Client>().Remove(client);
            await _context.SaveChangesAsync();
        }
    }
}
