using CRM_OrionTek.Domain.Entities;

namespace CRM_OrionTek.Domain.Interfaces
{
    public class DTO_UserHash
    {
        public string? UserName { get; set; }
    }
    public interface IAuthRepository
    {
        bool ValidateCredentials(string usuario, string password, string hashPassword);
        Task<User> GetUser(string userName);
        string HashPassword(User usuario);
        bool ValidatePassword(string usuario, string password, string hashPassword);
        string CreateToken(int userId, string userName, int? sucursalId);
    }
}
