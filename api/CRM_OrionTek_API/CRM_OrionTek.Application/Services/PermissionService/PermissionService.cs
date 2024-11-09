using CRM_OrionTek_API.Data;
using CRM_OrionTek_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Reflection;

namespace CRM_OrionTek.Application.Services.PermissionService
{
    public class PermissionService : IPermissionRepository
    {
        private readonly Context _context;
        public PermissionService(Context context)
        {
            _context = context;
        }

        public async Task<List<UserPermission>> CreateAndDeleteUserPermision(List<UserPermission> userPermissions, int userId)
        {
            // Verificar si existe un usuario, si no existe, crearlo.
            foreach (var item in userPermissions)
            {
                var UserPermission = await _context.UserPermission
                    .FirstOrDefaultAsync(e => e.UserId == item.UserId && e.PermissionId == item.PermissionId);
                if (UserPermission == null)
                {
                    _context.UserPermission.Add(item);
                }
                else {
                    _context.UserPermission.Update(item);
                }
            }

            await _context.SaveChangesAsync();

            // Obtener los permisos actuales del usuario.
            var usersPermissionsExisting = await _context.UserPermission
                .Where(e => e.UserId == userId)
                .ToListAsync();

            // Verificar y eliminar los permisos que ya no están en la lista.
            foreach (var item in usersPermissionsExisting)
            {
                var userPermission = userPermissions
                    .FirstOrDefault(e => e.UserId == item.UserId && e.PermissionId == item.PermissionId);
                if (userPermission == null)
                {
                    _context.UserPermission.Remove(item);
                }
            }

            await _context.SaveChangesAsync();

            // Devolver la lista actualizada de permisos del usuario.
            return await _context.UserPermission
                .Where(e => e.UserId == userId)
                .ToListAsync();
        }

        public Task<List<Permission>> GetAll() {
            var reslts = _context.Permission.ToListAsync();
            return reslts;
        }

        public async Task<List<Models.Module>> GetModules()
        {
            var opciones = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            var permission = await _context.Module.Include(a => a.Sections).ThenInclude(a => a.Permissions).ToListAsync();

            var modulosDto = permission.Select((object m) => new Module
            {
                moduloId = m.moduloId,
                Nombre = m.Nombre,
                Seccions = m.Seccions?.Select((object s) => new Seccion
                {
                    seccionId = s.seccionId,
                    Nombre = s.Nombre,
                    Permisos = s.Permisos.Select((object p) => new Permiso
                    {
                        permisoId = p.permisoId,
                        Nombre = p.Nombre
                    }).ToList()
                }).ToList()
            }).ToList();

            string jsonString = JsonSerializer.Serialize(modulosDto, opciones);
            return modulosDto;
        }

        public Task<List<UsuarioPermiso>> GetUserPermision(int userId)
        {
           var permissions = _context.UsuarioPermiso.Where(e => e.usuarioId == userId).ToListAsync();
            return permissions;
        }
    }
}
