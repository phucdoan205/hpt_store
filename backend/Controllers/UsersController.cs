using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using backend.DTOs.Users;
using backend.Models.Users;
using System.Security.Cryptography;
using System.Text;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public UsersController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var users = _db.Users
                    .Where(x => !x.IsDeleted)
                    .OrderByDescending(x => x.CreatedAt)
                    .ToList();

                var result = _mapper.Map<List<UserResponseDto>>(users);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var user = _db.Users.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
                if (user == null)
                    return NotFound(new { success = false, message = "User not found" });

                var result = _mapper.Map<UserResponseDto>(user);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequestDto dto)
        {
            try
            {
                if (dto == null || string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                    return BadRequest(new { success = false, message = "Username, email and password are required" });

                var existingUser = _db.Users.FirstOrDefault(x => (x.Username.ToLower() == dto.Username.ToLower() || x.Email.ToLower() == dto.Email.ToLower()) && !x.IsDeleted);
                if (existingUser != null)
                    return BadRequest(new { success = false, message = "Username or email already exists" });

                var user = new User
                {
                    Username = dto.Username,
                    Email = dto.Email,
                    PasswordHash = HashPassword(dto.Password),
                    FullName = dto.FullName,
                    PhoneNumber = dto.PhoneNumber,
                    DateOfBirth = dto.DateOfBirth,
                    Role = dto.Role,
                    IsEmailVerified = false,
                    IsLocked = false,
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false
                };

                _db.Users.Add(user);
                await _db.SaveChangesAsync();

                var result = _mapper.Map<UserResponseDto>(user);
                return CreatedAtAction(nameof(GetById), new { id = user.Id }, new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserRequestDto dto)
        {
            try
            {
                var user = _db.Users.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
                if (user == null)
                    return NotFound(new { success = false, message = "User not found" });

                if (!string.IsNullOrWhiteSpace(dto.FullName))
                    user.FullName = dto.FullName;

                if (!string.IsNullOrWhiteSpace(dto.PhoneNumber))
                    user.PhoneNumber = dto.PhoneNumber;

                if (dto.DateOfBirth.HasValue)
                    user.DateOfBirth = dto.DateOfBirth;

                if (!string.IsNullOrWhiteSpace(dto.AvatarUrl))
                    user.AvatarUrl = dto.AvatarUrl;

                if (dto.IsLocked.HasValue)
                    user.IsLocked = dto.IsLocked.Value;

                if (!string.IsNullOrWhiteSpace(dto.Role))
                    user.Role = dto.Role;

                user.UpdatedAt = DateTime.UtcNow;

                _db.Users.Update(user);
                await _db.SaveChangesAsync();

                var result = _mapper.Map<UserResponseDto>(user);
                return Ok(new { success = true, message = "User updated successfully", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var user = _db.Users.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
                if (user == null)
                    return NotFound(new { success = false, message = "User not found" });

                user.IsDeleted = true;
                user.UpdatedAt = DateTime.UtcNow;

                _db.Users.Update(user);
                await _db.SaveChangesAsync();

                return Ok(new { success = true, message = "User deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
