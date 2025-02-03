using AlandaProject.API.DTO;
using AlandaProject.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace AlandaProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegisterController : ControllerBase
    {
        private readonly MyDbContext _context;

        public UserRegisterController(MyDbContext context)
        {
            _context = context;
        }

        // Kullanıcı Kaydı (Register) (POST: api/users/register)
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserRegisterDto userDto)
        {
            if (_context.Users.Any(u => u.Email == userDto.Email))
            {
                return BadRequest("Bu mail zaten sisteme kayıtlı.");
            }

            var user = new User
            {
                FullName = userDto.FullName,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                PasswordHash = HashPassword(userDto.Password),
                Role = userDto.Role,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { user.UserId, user.FullName, user.Email, user.PhoneNumber, user.Role });
        }

        // Şifre Hashleme Metodu
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
