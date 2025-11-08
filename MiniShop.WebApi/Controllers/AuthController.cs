using Microsoft.AspNetCore.Mvc;
using MiniShop.Domain.Entities;
using MiniShop.Infrastructure.Persistence;
using MiniShop.Infrastructure.Services;
using MiniShop.WebApi.Models.Auth;
using System.Security.Cryptography;
using System.Text;

namespace MiniShop.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly JwtService _jwtService;

        public AuthController(AppDbContext db, JwtService jwtService)
        {
            _db = db;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            if (_db.Users.Any(u => u.Username == request.Username))
                return BadRequest("El usuario ya existe.");

            var user = new User
            {
                Username = request.Username,
                PasswordHash = HashPassword(request.Password),
                Role = "User"
            };

            _db.Users.Add(user);
            _db.SaveChanges();

            return Ok(new { message = "Usuario registrado correctamente." });
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var user = _db.Users.SingleOrDefault(u => u.Username == request.Username);
            if (user == null || !VerifyPassword(request.Password, user.PasswordHash))
                return Unauthorized(new { message = "Usuario o contraseña incorrectos." });

            var token = _jwtService.GenerateToken(user.Id, user.Username, user.Role);

            return Ok(new AuthResponse
            {
                Username = user.Username,
                Role = user.Role,
                Token = token
            });
        }

        private static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            return Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }

        private static bool VerifyPassword(string password, string hash)
            => HashPassword(password) == hash;
    }
}
