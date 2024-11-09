using CRM_OrionTek.Domain.Entities;

namespace CRM_OrionTek.Domain.Interfaces
{
    //public class Dto_pagination
    //{
    //    public List<User>? Data { get; set; }
    //    public int CantidadRegistros { get; set; }
    //}
    public interface IUserRepository
    {
        Task<Dto_pagination> GetAllPaginated(int page, int size, string SearchData);
        Task<User> GetOne(int id);
        Task<User> Create(User user);
        Task<User> Update(User user);
        Task<User> Delete(int id);
        Task<User> GetByUserName(string userName);
        Task<List<User>> UsersByname(string userName);
    }
}
