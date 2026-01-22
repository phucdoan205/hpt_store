using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _db;

        public AdminController(AppDbContext db)
        {
            _db = db;
        }

        // ✅ ADMIN TẠO NHÂN VIÊN
        [HttpPost("create-staff")]
        public IActionResult CreateStaff(CreateStaffDto dto)
        {
            if (_db.Users.Any(x => x.Username == dto.Username))
                return BadRequest("Username already exists");

            var staff = new User
            {
                Username = dto.Username,
                PasswordHash = HashPassword(dto.Password),
                Email = dto.Email,
                FullName = dto.FullName,

                Role = "Staff",
                IsLocked = dto.IsLocked,
                CreatedAt = DateTime.UtcNow
            };

            _db.Users.Add(staff);
            _db.SaveChanges();

            return Ok("Staff created successfully");
        }

        // ✅ DANH SÁCH NHÂN VIÊN
        [HttpGet("staffs")]
        public IActionResult GetStaffs()
        {
            var staffs = _db.Users
                .Where(x => x.Role == "Staff")
                .Select(x => new
                {
                    x.Id,
                    x.Username,
                    x.Email,
                    x.FullName,
                    x.IsLocked,
                    x.CreatedAt
                })
                .ToList();

            return Ok(staffs);
        }

        // ✅ KHÓA / MỞ NHÂN VIÊN
        [HttpPut("staff/{id}/lock")]
        public IActionResult ToggleLock(int id)
        {
            var staff = _db.Users.FirstOrDefault(x => x.Id == id && x.Role == "Staff");
            if (staff == null) return NotFound();

            staff.IsLocked = !staff.IsLocked;
            _db.SaveChanges();

            return Ok(new { staff.Id, staff.IsLocked });
        }

        // ===== HASH =====
        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
