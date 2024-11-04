using CRM_OrionTek_API.Models;
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
            client.CreateDate = DateTime.UtcNow;
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
            var results = query
                .Where(e => e.ClientName.ToLower().Contains(searchQuery))
                .Include(e => e.Locations)
                .ToListAsync();
            return results;
        }

        public async Task<Dto_pagination> GetAllPaginated(int page, int size, string? searchData)
        {
            // Validación de la página y el tamaño
            page = page <= 0 ? 1 : page;
            size = size <= 0 ? 10 : size;

            // Construir la consulta base con carga completa de las entidades relacionadas
            var query = _context.Client
                .Include(e => e.Locations)
                    .ThenInclude(l => l.Country)
                .Include(e => e.Locations)
                    .ThenInclude(l => l.Province)
                .Include(e => e.Locations)
                    .ThenInclude(l => l.Municipality)
                .Include(e => e.Locations)
                    .ThenInclude(l => l.District)
                .Include(e => e.Locations)
                    .ThenInclude(l => l.Sector)
                .AsQueryable();

            // Aplicar filtro de búsqueda si hay datos de búsqueda
            if (!string.IsNullOrWhiteSpace(searchData))
            {
                var searchQuery = searchData.ToLower();
                query = query.Where(e => e.ClientName.ToLower().Contains(searchQuery));
                page = 1; // Reinicia la paginación si hay una búsqueda
            }

            // Calcular el número total de registros
            var totalRecords = await query.CountAsync();

            // Obtener los elementos paginados, incluyendo todos los campos
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

            // Devolver los datos paginados
            return new Dto_pagination { Data = items, QuantityRecords = totalRecords };
        }


        public Task<Client> GetOne(int id)
        {
            var results = _context.Client
                .Where(e => e.ClientId == id)
                .Include(e => e.Locations)
                    .ThenInclude(l => l.Country)
                .Include(e => e.Locations)
                    .ThenInclude(l => l.Province)
                .Include(e => e.Locations)
                    .ThenInclude(l => l.Municipality)
                .Include(e => e.Locations)
                    .ThenInclude(l => l.District)
                .Include(e => e.Locations)
                    .ThenInclude(l => l.Sector)
                .FirstOrDefaultAsync();
            return results;
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

            existingClient.ClientName = client.ClientName;
            existingClient.UpdateDate = DateTime.UtcNow;

            // Actualizar las `Locations` existentes
            foreach (var location in client.Locations)
            {
                var existingLocation = existingClient.Locations
                    .FirstOrDefault(l => l.LocationId == location.LocationId);

                if (existingLocation != null)
                {
                    existingLocation.CountryId = location.CountryId;
                    existingLocation.ProvinceId = location.ProvinceId;
                    existingLocation.MunicipalityId = location.MunicipalityId;
                    existingLocation.DistrictId = location.DistrictId;
                    existingLocation.SectorId = location.SectorId;
                    existingLocation.UpdateDate = DateTime.UtcNow;
                }
                else
                {
                    existingClient.Locations.Add(location);
                }
            }

            await _context.SaveChangesAsync();
            return existingClient;
        }

        public Task<List<Client>> GetAll()
        {
            var results = _context.Client
                .Include(e => e.Locations)
                    .ThenInclude(l => l.Country)
                .Include(e => e.Locations)
                    .ThenInclude(l => l.Province)
                .Include(e => e.Locations)
                    .ThenInclude(l => l.Municipality)
                .Include(e => e.Locations)
                    .ThenInclude(l => l.District)
                .Include(e => e.Locations)
                    .ThenInclude(l => l.Sector)
                .ToListAsync();
            return results;
        }
    }
}
