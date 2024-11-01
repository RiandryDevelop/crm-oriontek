﻿using CRM_OrionTek_API.Models;
using CRM_OrionTek_API.Data;
using Microsoft.EntityFrameworkCore;


namespace CRM_OrionTek_API.Services.ClientService
{
    public class ClientService : IClient
    {
        private readonly Context _context;
        private readonly IConfiguration _config;
        public ClientService(Context context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public async Task<Client> Create(Client client)
        {
            client.CreateDate = DateTime.Now;
            _context.Client.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<Client> Delete(int id)
        {
            var client = await _context.Client.Include(c => c.Locations).FirstOrDefaultAsync(c => c.ClientId == id);
            if (client != null)
            {
                _context.Location.RemoveRange(client.Locations);
                _context.Client.Remove(client);
                await _context.SaveChangesAsync(); 
            }
            return client;
        }


        public Task<List<Client>> GetByname(string name)
        {
            var query = _context.Client.AsQueryable();
            var searchQuery = name.ToLower();
            var results = query.Where(e => e.Name.ToLower().Contains(searchQuery)).Include(e => e.Locations).ToListAsync();
            return results;
        }

        public async Task<Dto_pagination> GetAllPaginated(int page, int size, string searchData)
        {
            page = page <= 0 ? 1 : page;
            size = size <= 0 ? 10 : size; 

            
            var query = _context.Client
                .Include(e => e.Locations) 
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchData))
            {
                var searchQuery = searchData.ToLower();
                query = query.Where(e => e.Name.ToLower().Contains(searchQuery));
                page = 1;
            }

            var totalRecords = await query.CountAsync();

            var items = await query
                .OrderBy(e => e.Name) 
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return new Dto_pagination { Data = items, QuantityRecords = totalRecords };
        }


        public Task<Client> GetOne(int id)
        {
            var results = _context.Client
                .Where(e => e.ClientId == id)
                .Include(e => e.Locations)
                .FirstOrDefaultAsync();
            return results;
        }

        public async Task<Client> Update(Client client, int id)
        {
            var existingClient = await _context.Client.FindAsync(id);

            if (existingClient == null)
            {
                throw new KeyNotFoundException("Client not found.");
            }

            existingClient.UpdateDate = DateTime.Now;
            existingClient.Name = client.Name;
            existingClient.Locations = client.Locations;

            existingClient.UpdateDate = DateTime.Now;

            // Save changes to the database
            await _context.SaveChangesAsync();

            return existingClient;
        }



        public Task<List<Client>> GetAll()
        {
            var results = _context.Client
                .Include(e => e.Locations)
                .ToListAsync();
            return results;
        }
    }
}
