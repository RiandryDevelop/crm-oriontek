
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;

namespace CRM_OrionTek.Application.Services.UserService
{
    public class AuthService : IAuthRepository
    {
        private readonly Context _context;
        private IConfiguration _config;

        public AuthService(Context context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public bool ValidateCredentials(string usuario, string password, string hashPassword)
        {
            password = password.ToUpper();
            bool validate = validatePassword(usuario, password, hashPassword);
            return validate;
        }

        public async Task<User> GetUser(string userName)
        {
            var userExist = await _context.User.AnyAsync(e => e.UserName == userName);
            if (userExist == false) { return null; }

            var user = await _context.User.Where(e => e.UserName == userName).FirstOrDefaultAsync();
            return user;
        }

        public string HashPassword(User user) {

            var passwordHasher = new PasswordHasher<DTO_UserHash>();

            var user = new DTO_UserHash
            {
                UserName = user.UserName
            };

            // Hashear la contraseña
            string hashedPassword = passwordHasher.HashPassword(user, user.Password);

            return hashedPassword;
        }

        public bool ValidatePassword(string usuario, string password, string hashPassword)
        {
            var passwordHasher = new PasswordHasher<DTO_UserHash>();

            var userValidate = new DTO_UserHash
            {
                UserName = usuario
            };

            var result = passwordHasher.VerifyHashedPassword(userValidate, hashPassword, password);
            return result == PasswordVerificationResult.Success;
        }

        public string CreateToken(int userId, string userName, int? sucursalId) {

            var issuer = _config["Jwt:Issuer"];

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim("UserId", userId.ToString()),
            new Claim("UserName", userName),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = issuer,
                Audience = issuer
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
