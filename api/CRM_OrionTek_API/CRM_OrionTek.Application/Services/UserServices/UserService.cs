using CRM_OrionTek_API.Data;
using CRM_OrionTek_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CRM_OrionTek.Application.Services.UserService
{
    public class UserService : IUser
    {
        private readonly Context _context;
        private readonly IAuth _auth;
        private readonly IConfiguration _config;
        public UserService(Context context, IAuth auth, IConfiguration config)
        {
            _context = context;
            _auth = auth;
            _config = config;
        }
        public async Task<User> Create(User user)
        {
            
            user.Password = user.Password.ToUpper();
            user.Password = _auth.hashPassword(user);
            user.CreateDate = DateTime.Now;
            
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Delete(int id)
        {
            var user = await _context.User.Where(e => e.UserId == id).FirstOrDefaultAsync();
            if (user == null) {
                return null;
            }

            _context.User.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<Dto_pagination> GetAllPaginated(int page, int size, string SearchData)
        {
            
            var query = _context.User.AsQueryable();

            // Aplicar filtro de búsqueda si query no es nulo ni vacío.
            if (!string.IsNullOrEmpty(SearchData))
            {
                var searchQuery = SearchData.ToLower();
                query = query.Where(e => e.UserName.ToLower().Contains(searchQuery));
                page = 1;
            }

            var totalRecords = await query.CountAsync();

            var users = await query
                .Skip((page - 1) * size)
                .Take(size)

                .ToListAsync();

            foreach (var user in users) {
                user.CreatorName = _context.User.Where(e => e.UserId == user.CreatedBy).FirstOrDefault()?.Name;
            }

            return new Dto_pagination { Data = users, CantidadRegistros = totalRecords };
        }

        public Task<User> GetByUserName(string userName)
        {
            var user = _context.User.Where(e => e.UserName == userName).FirstOrDefaultAsync();
            return user;
        }

        public Task<User> GetOne(int id)
        {
            var user = _context.User.Where(e => e.UserId == id)
                .Include(e => e.UserPermissions)
                .Include(e => e.UserGroups)
                .FirstOrDefaultAsync();
            return user;
        }

        public async Task<User> Update(User user)
        {
            try
            {
                var passUser = await _context.User.Where(e => e.UserId == user.UserId).FirstOrDefaultAsync();

                //Delete userPermissions.
                await deleteUserPermissions(user);

                //Delete userGroup.
                await deleteUserGroup(user);

                if (passUser?.Password != user.Password) {
                    user.Password = user.Password.ToUpper();
                    user.Password = _auth.hashPassword(user);
                }

                user.UpdateDate = DateTime.Now;
                if (_config["production"] == "true")
                {
                    user.UpdateDate = user.UpdateDate.AddHours(-4);
                }
                _context.User.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception error) {
                Console.WriteLine(error);
            }
            return user;
        }

        public async Task<List<UserPermission>> deleteUserPermissions(User user) {
            var documentosActuales = _context.UserPermission.Where(e => e.UserId == user.UserId).ToList();

            foreach (var document in documentosActuales) {
                var deleteItem = user.UserPermissions.Where(e => e.UserPermissionId == document.UserPermissionId).FirstOrDefault();
                if (deleteItem == null) {
                    _context.UserPermission.Remove(document);
                }
            }
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
            return documentosActuales;
        }

      
        public async Task<List<UserGroup>> DeleteUserGroup(User user)
        {
            var documentosActuales = _context.UserGroup.Where(e => e.UserId == user.UserId).ToList();

            foreach (var document in documentosActuales)
            {
                var deleteItem = user.UserGroups.Where(e => e.UserGroupId == document.UserGroupId).FirstOrDefault();
                if (deleteItem == null)
                {
                    _context.UserGroup.Remove(document);
                }
            }
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
            return documentosActuales;
        }



        public Task<List<User>> UsersByname(string userName)
        {
            var searchQuery = userName.ToLower();
            var users = _context.User.Where(e => e.UserName.ToLower().Contains(searchQuery)).ToListAsync();
            return users;
        }
    }
}
