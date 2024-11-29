using CRM_OrionTek_API.Models;

namespace CRM_OrionTek_API.Services.LocationService
{
    public class Dto_pagination
    {
        public List<Location>? Data { get; set; }
        public int QuantityRecords { get; set; }
    }

    public interface ILocation
    {
        Task<Location> Create(Location location);
        Task<Location> Update(Location location);
        Task<Dto_pagination> GetAllPaginated(int page, int size, string searchData);
        Task<Location> GetOne(int id);
        Task<Location> Delete(int id);
        //Task<List<Location>> GetByname(string name);
        Task<List<Location>> GetAll();
    }
}
