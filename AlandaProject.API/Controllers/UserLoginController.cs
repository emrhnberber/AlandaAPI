using AlandaProject.API.DTO;
using AlandaProject.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace AlandaProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {

        private readonly MyDbContext _context;

        public UserLoginController(MyDbContext context)
        {
            _context = context;
        }

        // Kullanıcı Girişi (POST: api/users/login)
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(UserLoginDto loginDto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == loginDto.Email || u.PhoneNumber == loginDto.PhoneNumber);

            if (user == null)
            {
                return NotFound("Kullanıcı Adı veya Şifre yanlış.");
            }

            if (user.PasswordHash != HashPassword(loginDto.Password))
            {
                return Unauthorized("Kullanıcı Adı veya Şifre yanlış.");
            }

            return Ok(new { user.UserId, user.FullName, user.Email, user.PhoneNumber, user.Role });
        }

        // Şifre Hashleme Metodu (RegisterController ile aynı şekilde)
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
