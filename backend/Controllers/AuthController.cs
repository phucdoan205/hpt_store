using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly JwtService _jwt;

        public AuthController(AppDbContext db, JwtService jwt)
        {
            _db = db;
            _jwt = jwt;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            if (_db.Users.Any(x => x.Username == dto.Username))
                return BadRequest("Username exists");

            var user = new User
            {
                Username = dto.Username,
                PasswordHash = HashPassword(dto.Password),
                Email = dto.Email,
                FullName = dto.FullName,
                Role = "User",
                CreatedAt = DateTime.UtcNow
            };

            _db.Users.Add(user);
            _db.SaveChanges();

            return Ok("Register success");
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var user = _db.Users.FirstOrDefault(x => x.Username == dto.Username);
            if (user == null)
                return Unauthorized("Invalid credentials");

            if (user.IsLocked)
                return Unauthorized("Account is locked");

            if (!Verify(dto.Password, user.PasswordHash))
                return Unauthorized("Invalid credentials");

            var token = _jwt.GenerateToken(user);

            return Ok(new
            {
                token,
                role = user.Role,
                username = user.Username
            });
        }


        // ====== HASH ======
        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        private bool Verify(string password, string hash)
            => HashPassword(password) == hash;
    }
}
