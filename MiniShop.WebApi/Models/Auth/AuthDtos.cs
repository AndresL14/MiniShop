namespace MiniShop.WebApi.Models.Auth
{
    public class RegisterRequest
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
    }

    public class LoginRequest
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
    }

    public class AuthResponse
    {
        public string Username { get; set; } = default!;
        public string Role { get; set; } = default!;
        public string Token { get; set; } = default!;
    }
}
