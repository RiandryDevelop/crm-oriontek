using CRM_OrionTek_API.Models;
using CRM_OrionTek_API.Services.ClientService;

namespace CRM_OrionTek_API.Services.LocationService
{
    public class Dto_pagination
    {
        public List<Location>? Data { get; set; }
        public int QuantityRecords { get; set; }
    }
    public interface ILocation
    {
        Task<Location> Create(Location brand);
        Task<Location> Update(Location brand);
        Task<Dto_pagination> GetAllPaginated(int page, int size, string SearchData);
        Task<Location> GetOne(int id);
        Task<Location> Delete(int id);
        Task<List<Location>> GetByname(string name);
        Task<List<Location>> GetAll();
    }
}
