using CRM_OrionTek_API.Data;
using CRM_OrionTek_API.Models;
using CRM_OrionTek_API.Services.LocationService;
using Microsoft.EntityFrameworkCore;

namespace MeditodApi.Services.LocationService
{
    public class LocationService : ILocation
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


        public Task<List<Location>> GetByname(string name)
        {
            var query = _context.Location.AsQueryable();
            var searchQuery = name.ToLower();
            var results = query.Where(e => e.LocationName.ToLower().Contains(searchQuery)).ToListAsync();
            return results;
        }

        public async Task<Dto_pagination> GetAllPaginated(int page, int size, string SearchData)
        {

            var query = _context.Location.AsQueryable();

            if (!string.IsNullOrEmpty(SearchData))
            {
                var searchQuery = SearchData.ToLower();
                query = query.Where(e => e.LocationName.ToLower().Contains(searchQuery));
                page = 1;
            }

            var totalRecords = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

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
