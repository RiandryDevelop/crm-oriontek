using CRM_OrionTek.Domain.Entities;

namespace CRM_OrionTek.Domain.Interfaces
{
    public interface IPermissionRepository
    {
        Task<List<Permission>> GetAll();
        Task<List<Module>> GetModules();
        Task<List<UserPermission>> GetUserPermision(int userId);
        Task<List<UserPermission>> CreateAndDeleteUserPermision(List<UserPermission> UserPermissions, int UserId);
    }
}
