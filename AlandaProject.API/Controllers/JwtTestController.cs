using AlandaProject.API.DTO;
using AlandaProject.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class JwtTestController : ControllerBase
{
    private readonly MyDbContext _context;
    private readonly TokenService _tokenService;

    public JwtTestController(MyDbContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    // Kullanıcı Girişi (POST: api/users/login)
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto loginDto)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == loginDto.Email || u.PhoneNumber == loginDto.PhoneNumber);

        if (user == null || user.PasswordHash != HashPassword(loginDto.Password))
        {
            return Unauthorized("Kullanıcı adı veya şifre yanlış.");
        }

        var token = _tokenService.GenerateToken(user);

        return Ok(new { Token = token, user.UserId, user.FullName, user.Email, user.PhoneNumber, user.Role });
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
