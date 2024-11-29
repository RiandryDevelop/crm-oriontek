using CRM_OrionTek_API.Models;

namespace CRM_OrionTek_API.Services.ClientService
{
    public class Dto_pagination
    {
        public object Data { get; set; }
        public int QuantityRecords { get; set; }
    }

    public interface IClient
    {
        Task<Client> Create(Client client);
        Task<Client> Update(Client client, int id);
        Task<Dto_pagination> GetAllPaginated(int page = 1, int size = 0, string? SearchData = "");
        Task<Client> GetOne(int id);
        Task<Client> Delete(int id);
        Task<List<Client>> GetByname(string name);
        Task<List<Client>> GetAll();
    }
}
