using Microsoft.Extensions.Configuration;
using CRM_OrionTek.Domain.Entities;
using CRM_OrionTek.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRM_OrionTek.Infrastructure.Data;


namespace CRM_OrionTek.Application.Services.ClientService
{
    public class ClientService
    {
        private readonly IClientRepository clientRepository;
        private readonly Context _context;
        private readonly IConfiguration _config;

        public ClientService(Context context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<Client> Create(Client client)
        {
            if (client.Locations == null || !client.Locations.Any())
            {
                throw new ArgumentException("The client must have at least one location.");
            }

            foreach (var location in client.Locations)
            {
                if (!await _context.Country.AnyAsync(c => c.CountryId == location.CountryId))
                {
                    throw new ArgumentException("The provided CountryId is invalid.");
                }

                if (!await _context.Province.AnyAsync(p => p.ProvinceId == location.ProvinceId))
                {
                    throw new ArgumentException("The provided ProvinceId is invalid.");
                }

                if (!await _context.Municipality.AnyAsync(m => m.MunicipalityId == location.MunicipalityId && m.ProvinceId == location.ProvinceId))
                {
                    throw new ArgumentException("The provided MunicipalityId is invalid or does not correspond to the specified province.");
                }

                if (!await _context.District.AnyAsync(d => d.DistrictId == location.DistrictId && d.MunicipalityId == location.MunicipalityId))
                {
                    throw new ArgumentException("The provided DistrictId is invalid or does not correspond to the specified municipality.");
                }

                if (!await _context.Sector.AnyAsync(s => s.SectorId == location.SectorId && s.DistrictId == location.DistrictId))
                {
                    throw new ArgumentException("The provided SectorId is invalid or does not correspond to the specified district.");
                }
            }

            client.CreateDate = DateTime.UtcNow;
            _context.Client.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }



        public async Task<Client> Delete(int id)
        {
            var client = await _context.Client
                .Include(c => c.Locations)
                .FirstOrDefaultAsync(c => c.ClientId == id);

            if (client != null)
            {
                if (client.Locations != null) 
                {
                    _context.Location.RemoveRange(client.Locations);
                }

                _context.Client.Remove(client);
                await _context.SaveChangesAsync();
            }

            return client;
        }


        public Task<List<Client>> GetByname(string name)
        {
            var query = _context.Client.AsQueryable();
            var searchQuery = name.ToLower();
            var results = query
                .Where(e => e.ClientName.ToLower().Contains(searchQuery))
                .Include(e => e.Locations)
                .ToListAsync();
            return results;
        }

        public async Task<Dto_pagination> GetAllPaginated(int page, int size, string? searchData)
        {
            page = page <= 0 ? 1 : page;
            size = size <= 0 ? 10 : size;

            var query = _context.Client
             .Include(e => (IEnumerable<Location>)e.Locations!)
            .ThenInclude(l => l.Country)
            .Include(e => (IEnumerable<Location>)e.Locations!)
                .ThenInclude(l => l.Province)
            .Include(e => (IEnumerable<Location>)e.Locations!)
                .ThenInclude(l => l.Municipality)
            .Include(e => (IEnumerable<Location>)e.Locations!)
                .ThenInclude(l => l.District)
            .Include(e => (IEnumerable<Location>)e.Locations!)
                .ThenInclude(l => l.Sector)
            .AsQueryable();



            if (!string.IsNullOrWhiteSpace(searchData))
            {
                var searchQuery = searchData.ToLower();
                query = query.Where(e => e.ClientName.ToLower().Contains(searchQuery));
                page = 1; 
            }

            var totalRecords = await query.CountAsync();

            var items = await query
                .OrderBy(e => e.ClientName)
                .Skip((page - 1) * size)
                .Take(size)
                .Select(e => new
                {
                    e.ClientId,
                    e.ClientName,
                    e.CreateDate,
                    e.UpdateDate,
                    Locations = e.Locations.Select(l => new
                    {
                        l.LocationId,
                        l.CountryId,
                        Country = l.Country != null ? new { l.Country.CountryId, l.Country.CountryName } : null,
                        l.ProvinceId,
                        Province = l.Province != null ? new { l.Province.ProvinceId, l.Province.ProvinceName } : null,
                        l.MunicipalityId,
                        Municipality = l.Municipality != null ? new { l.Municipality.MunicipalityId, l.Municipality.MunicipalityName, l.Municipality.ProvinceId } : null,
                        l.DistrictId,
                        District = l.District != null ? new { l.District.DistrictId, l.District.DistrictName, l.District.MunicipalityId } : null,
                        l.SectorId,
                        Sector = l.Sector != null ? new { l.Sector.SectorId, l.Sector.SectorName, l.Sector.DistrictId } : null,
                        l.CreateDate,
                        l.UpdateDate
                    }).ToList()
                })
                .ToListAsync();

            return new Dto_pagination { Data = items, QuantityRecords = totalRecords };
        }


        public async Task<Client> GetOne(int id)
        {
            var result = await _context.Client
                .Where(e => e.ClientId == id)
                .Include(e => (IEnumerable<Location>)e.Locations!)
                    .ThenInclude(l => l.Country)
                .Include(e => (IEnumerable<Location>)e.Locations!)
                    .ThenInclude(l => l.Province)
                .Include(e => (IEnumerable<Location>)e.Locations!)
                    .ThenInclude(l => l.Municipality)
                .Include(e => (IEnumerable<Location>)e.Locations!)
                    .ThenInclude(l => l.District)
                .Include(e => (IEnumerable<Location>)e.Locations!)
                    .ThenInclude(l => l.Sector)
                .FirstOrDefaultAsync();

            return result;
        }



        public async Task<Client> Update(Client client, int id)
        {
            var existingClient = await _context.Client
                .Include(c => c.Locations)
                .FirstOrDefaultAsync(c => c.ClientId == id);

            if (existingClient == null)
            {
                throw new KeyNotFoundException("Client not found.");
            }

            if (client.Locations == null || !client.Locations.Any())
            {
                throw new ArgumentException("The client must have at least one location.");
            }

            foreach (var location in client.Locations)
            {
                if (!await _context.Country.AnyAsync(c => c.CountryId == location.CountryId))
                {
                    throw new ArgumentException("The provided CountryId is invalid.");
                }

                if (!await _context.Province.AnyAsync(p => p.ProvinceId == location.ProvinceId))
                {
                    throw new ArgumentException("The provided ProvinceId is invalid.");
                }

                if (!await _context.Municipality.AnyAsync(m => m.MunicipalityId == location.MunicipalityId && m.ProvinceId == location.ProvinceId))
                {
                    throw new ArgumentException("The provided MunicipalityId is invalid or does not correspond to the specified province.");
                }

                if (!await _context.District.AnyAsync(d => d.DistrictId == location.DistrictId && d.MunicipalityId == location.MunicipalityId))
                {
                    throw new ArgumentException("The provided DistrictId is invalid or does not correspond to the specified municipality.");
                }

                if (!await _context.Sector.AnyAsync(s => s.SectorId == location.SectorId && s.DistrictId == location.DistrictId))
                {
                    throw new ArgumentException("The provided SectorId is invalid or does not correspond to the specified district.");
                }
            }

            existingClient.ClientName = client.ClientName;
            existingClient.Locations = client.Locations;
            existingClient.UpdateDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingClient;
        }



        public Task<List<Client>> GetAll()
        {
            var results = _context.Client
                .Include(e => (IEnumerable<Location>)e.Locations!)
                    .ThenInclude(l => l.Country)
                .Include(e => (IEnumerable<Location>)e.Locations!)
                    .ThenInclude(l => l.Province)
                .Include(e => (IEnumerable<Location>)e.Locations!)
                    .ThenInclude(l => l.Municipality)
                .Include(e => (IEnumerable<Location>)e.Locations!)
                    .ThenInclude(l => l.District)
                .Include(e => (IEnumerable<Location>)e.Locations!)
                    .ThenInclude(l => l.Sector)
                .ToListAsync();
            return results;
        }
    }
}
