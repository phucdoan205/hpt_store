using backend.DTOs.Auth;
using backend.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/admin/staffs")]
[Authorize(Roles = "Admin")]
public class AdminStaffController : ControllerBase
{
    private readonly AppDbContext _db;

    public AdminStaffController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult GetStaffs()
    {
        return Ok(_db.Users.Where(x => x.Role == "Staff").ToList());
    }

    [HttpPost]
    public IActionResult CreateStaff(CreateStaffDto dto)
    {
        if (_db.Users.Any(x => x.Username == dto.Username))
            return BadRequest("Username exists");

        var staff = new User
        {
            Username = dto.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            FullName = dto.FullName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            AvatarUrl = dto.AvatarUrl,
            Role = "Staff",
            CreatedAt = DateTime.UtcNow
        };

        _db.Users.Add(staff);
        _db.SaveChanges();

        return Ok();
    }

    [HttpPut("{id}/lock")]
    public IActionResult LockStaff(int id)
    {
        var staff = _db.Users.FirstOrDefault(x => x.Id == id && x.Role == "Staff");
        if (staff == null) return NotFound();

        staff.IsLocked = !staff.IsLocked;
        _db.SaveChanges();

        return Ok(staff.IsLocked);
    }
}