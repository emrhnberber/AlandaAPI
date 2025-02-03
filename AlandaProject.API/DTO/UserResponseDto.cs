using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlandaProject.API.DTO
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserResponseDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }

}
