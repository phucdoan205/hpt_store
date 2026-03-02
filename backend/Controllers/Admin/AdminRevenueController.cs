// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;

// [ApiController]
// [Route("api/admin/revenue")]
// [Authorize(Roles = "Admin")]
// public class AdminRevenueController : ControllerBase
// {
//     private readonly AppDbContext _db;
// // 
//     public AdminRevenueController(AppDbContext db)
//     {
//         _db = db;
//     }

//     [HttpGet]
//     public IActionResult GetRevenue(DateTime from, DateTime to)
//     {
//         var revenue = _db.Orders
//             .Where(x => x.Status == "Completed" && x.OrderDate >= from && x.OrderDate <= to)
//             .Sum(x => x.FinalAmount);

//         return Ok(new { revenue });
//     }
// }