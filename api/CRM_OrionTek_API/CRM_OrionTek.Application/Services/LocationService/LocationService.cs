using CRM_OrionTek_API.Data;
using CRM_OrionTek_API.Models;
using CRM_OrionTek_API.Services.LocationService;
using Microsoft.EntityFrameworkCore;

namespace CRM_OrionTek.Application.Services.LocationService
{
    public class LocationService : ILocationRepository
    {
        private readonly Context _context;
        private readonly IConfiguration _config;
        public LocationService(Context context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public async Task<Location> Create(Location location)
        {
            location.CreateDate = DateTime.Now;
            _context.Location.Add(location);
            await _context.SaveChangesAsync();
            return location;
        }

        public async Task<Location> Delete(int id)
        {
            var result = await _context.Location.Where(e => e.LocationId == id).FirstOrDefaultAsync();
            if (result != null)
            {
                _context.Location.Update(result);
                await _context.SaveChangesAsync();
            }
            return result;
        }


        //public Task<List<Location>> GetByname(string name)
        //{
        //    var query = _context.Location.AsQueryable();
        //    var searchQuery = name.ToLower();
        //    var results = query.Where(e => e.LocationName.ToLower().Contains(searchQuery)).ToListAsync();
        //    return results;
        //}

        public async Task<Dto_pagination> GetAllPaginated(int page, int size, string searchData)
        {
            // Validación de la página y el tamaño
            page = page <= 0 ? 1 : page;
            size = size <= 0 ? 10 : size;

            // Incluyendo las relaciones definidas en el modelo
            var query = _context.Location
                .Include(l => l.Country)
                .Include(l => l.Province)
                .Include(l => l.Municipality)
                .Include(l => l.District)
                .Include(l => l.Sector)
                .AsQueryable();

            // Manejo de la búsqueda
            if (!string.IsNullOrWhiteSpace(searchData))
            {
                var searchQuery = searchData.ToLower();
                query = query.Where(e =>
                    e.Country != null && e.Country.CountryName.ToLower().Contains(searchQuery) ||
                    e.Province != null && e.Province.ProvinceName.ToLower().Contains(searchQuery) ||
                    e.Municipality != null && e.Municipality.MunicipalityName.ToLower().Contains(searchQuery) ||
                    e.District != null && e.District.DistrictName.ToLower().Contains(searchQuery) ||
                    e.Sector != null && e.Sector.SectorName.ToLower().Contains(searchQuery));
                page = 1; // Reinicia la paginación si hay búsqueda
            }

            // Obtener el número total de registros
            var totalRecords = await query.CountAsync();

            // Obtener los elementos paginados
            var items = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            // Devolver los datos paginados
            return new Dto_pagination { Data = items, QuantityRecords = totalRecords };
        }



        public Task<Location> GetOne(int id)
        {
            var results = _context.Location.Where(e => e.LocationId == id).FirstOrDefaultAsync();
            return results;
        }

        public async Task<Location> Update(Location location)
        {
            location.UpdateDate = DateTime.Now;
            
            _context.Location.Update(location);
            await _context.SaveChangesAsync();
            return location;
        }

        public Task<List<Location>> GetAll()
        {
            var results = _context.Location.ToListAsync();
            return results;
        }
    }
}
