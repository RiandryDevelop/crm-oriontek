using CRM_OrionTek_API.Data;
using CRM_OrionTek_API.Models;
using CRM_OrionTek_API.Services.TreatmentService;
using Microsoft.EntityFrameworkCore;

namespace CRM_OrionTek.Application.Services.UnitService
{
    public class UnitService : IUnitRepository
    {
        private readonly Context _context;
        private readonly IConfiguration _config;
        public UnitService(Context context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public async Task<Unit> Create(Unit unit)
        {
            unit.CreateDate = DateTime.Now;
            if (_config["production"] == "true")
            {
                unit.CreateDate = unit.CreateDate.AddHours(-4);
            }
            _context.Unit.Add(unit);
            await _context.SaveChangesAsync();
            return unit;
        }

        public async Task<Unit> Delete(int id)
        {
            var result = await _context.Unit.Where(e => e.unitId == id).FirstOrDefaultAsync();
            if (result != null)
            {
                result.estado = !result.estado;
                _context.Unit.Update(result);
                await _context.SaveChangesAsync();
            }
            return result;
        }


        public Task<List<Unit>> GetByname(string name)
        {
            var query = _context.Unit.AsQueryable();
            var searchQuery = name.ToLower();
            var results = query.Where(e => e.unitName.ToLower().Contains(searchQuery)).ToListAsync();
            return results;
        }

        public async Task<Dto_pagination> GetAllPaginated(int page, int size, string SearchData)
        {

            var query = _context.Unit.AsQueryable();

            // Aplicar filtro de búsqueda si query no es nulo ni vacío.
            if (!string.IsNullOrEmpty(SearchData))
            {
                var searchQuery = SearchData.ToLower();
                query = query.Where(e => e.unitName.ToLower().Contains(searchQuery));
                page = 1;
            }

            var totalRecords = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return new Dto_pagination { Data = items, CantidadRegistros = totalRecords };
        }

        public Task<Unit> GetOne(int id)
        {
            var results = _context.Unit.Where(e => e.unitId == id).FirstOrDefaultAsync();
            return results;
        }

        public async Task<Unit> Update(Unit unit)
        {
            unit.UpdateDate = DateTime.Now;
            if (_config["production"] == "true")
            {
                unit.UpdateDate = unit.UpdateDate.AddHours(-4);
            }
            _context.Unit.Update(unit);
            await _context.SaveChangesAsync();
            return unit;
        }

        public Task<List<Unit>> GetAll()
        {
            var results = _context.Unit.ToListAsync();
            return results;
        }
    }
}
