namespace AlandaProject.API.DTO
{
    public class UserRegisterDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; } // Plain password, hash işlemi yapılacak
        public string Role { get; set; } = "User";
    }
}
