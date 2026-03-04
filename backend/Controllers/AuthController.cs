using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using backend.DTOs.Auth;
using backend.Models.Users;


namespace backend.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly JwtService _jwt;
        private readonly IConfiguration _config;

        public AuthController(AppDbContext db, JwtService jwt, IConfiguration config)
        {
            _db = db;
            _jwt = jwt;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                    return BadRequest(new { success = false, message = "Username, email and password are required" });

                if (_db.Users.Any(x => x.Username == dto.Username && !x.IsDeleted))
                    return BadRequest(new { success = false, message = "Username already exists" });

                if (_db.Users.Any(x => x.Email == dto.Email && !x.IsDeleted))
                    return BadRequest(new { success = false, message = "Email already exists" });

                var user = new User
                {
                    Username = dto.Username,
                    PasswordHash = HashPassword(dto.Password),
                    Email = dto.Email,
                    FullName = dto.FullName,
                    PhoneNumber = dto.PhoneNumber,
                    AvatarUrl = dto.AvatarUrl,
                    Role = "User",
                    IsEmailVerified = false,
                    IsLocked = false,
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false
                };

                _db.Users.Add(user);
                await _db.SaveChangesAsync();

                return Ok(new { success = true, message = "Register successful" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            try
            {
                var user = _db.Users.FirstOrDefault(x => (x.Username == dto.EmailOrUsername || x.Email == dto.EmailOrUsername) && !x.IsDeleted);
                if (user == null)
                    return Unauthorized(new { success = false, message = "Invalid credentials" });

                if (user.IsLocked)
                    return Unauthorized(new { success = false, message = "Account is locked" });

                if (!Verify(dto.Password, user.PasswordHash))
                    return Unauthorized(new { success = false, message = "Invalid credentials" });

                var token = _jwt.GenerateToken(user);
                var refreshToken = _jwt.GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(int.Parse(_config["Jwt:RefreshTokenExpireDays"] ?? "7"));
                _db.Users.Update(user);
                await _db.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    data = new
                    {
                        token,
                        refreshToken,
                        role = user.Role,
                        username = user.Username,
                        userId = user.Id
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequestDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.RefreshToken))
                    return BadRequest(new { success = false, message = "Refresh token is required" });

                var user = _db.Users.FirstOrDefault(x => x.RefreshToken == dto.RefreshToken && !x.IsDeleted);
                if (user == null)
                    return Unauthorized(new { success = false, message = "Invalid refresh token" });

                if (!_jwt.ValidateRefreshToken(user.RefreshToken, user.RefreshTokenExpiry ?? DateTime.UtcNow))
                    return Unauthorized(new { success = false, message = "Refresh token expired" });

                var newToken = _jwt.GenerateToken(user);
                var newRefreshToken = _jwt.GenerateRefreshToken();

                user.RefreshToken = newRefreshToken;
                user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(int.Parse(_config["Jwt:RefreshTokenExpireDays"] ?? "7"));
                _db.Users.Update(user);
                await _db.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    data = new
                    {
                        token = newToken,
                        refreshToken = newRefreshToken,
                        role = user.Role,
                        username = user.Username
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginRequestDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.IdToken))
                    return BadRequest(new { success = false, message = "ID token is required" });

                var payload = ValidateGoogleToken(dto.IdToken);
                if (payload == null)
                    return Unauthorized(new { success = false, message = "Invalid Google token" });

                var googleId = payload["sub"].ToString();
                var email = payload["email"].ToString();
                var fullName = payload.ContainsKey("name") ? payload["name"].ToString() : email.Split("@")[0];

                var user = _db.Users.FirstOrDefault(x => x.GoogleId == googleId && !x.IsDeleted);

                if (user == null)
                {
                    var existingUserByEmail = _db.Users.FirstOrDefault(x => x.Email == email && !x.IsDeleted);
                    if (existingUserByEmail != null)
                    {
                        existingUserByEmail.GoogleId = googleId;
                    }
                    else
                    {
                        user = new User
                        {
                            Username = email.Split("@")[0],
                            Email = email,
                            FullName = fullName,
                            GoogleId = googleId,
                            PasswordHash = HashPassword(Guid.NewGuid().ToString()), // Random password
                            Role = "User",
                            IsEmailVerified = true,
                            IsLocked = false,
                            CreatedAt = DateTime.UtcNow,
                            IsDeleted = false
                        };
                        _db.Users.Add(user);
                    }

                    if (user != null)
                    {
                        user = existingUserByEmail ?? user;
                    }
                }

                if (user.IsLocked)
                    return Unauthorized(new { success = false, message = "Account is locked" });

                var token = _jwt.GenerateToken(user);
                var refreshToken = _jwt.GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(int.Parse(_config["Jwt:RefreshTokenExpireDays"] ?? "7"));
                _db.Users.Update(user);
                await _db.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    data = new
                    {
                        token,
                        refreshToken,
                        role = user.Role,
                        username = user.Username,
                        userId = user.Id,
                        email = user.Email
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        private bool Verify(string password, string hash)
            => HashPassword(password) == hash;

        private Dictionary<string, object> ValidateGoogleToken(string idToken)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(idToken);

                if (token.ValidTo < DateTime.UtcNow)
                    return null;

                var payload = new Dictionary<string, object>();
                foreach (var claim in token.Claims)
                {
                    payload[claim.Type] = claim.Value;
                }

                return payload;
            }
            catch
            {
                return null;
            }
        }
    }
}
