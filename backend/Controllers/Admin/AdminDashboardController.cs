// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;

// [ApiController]
// [Route("api/admin/dashboard")]
// [Authorize(Roles = "Admin")]
// public class AdminDashboardController : ControllerBase
// {
//     private readonly AppDbContext _db;

//     public AdminDashboardController(AppDbContext db)
//     {
//         _db = db;
//     }

//     [HttpGet]
//     public async Task<IActionResult> GetDashboard()
//     {
//         var totalOrders = await _db.Orders.CountAsync();
//         var pendingOrders = await _db.Orders.CountAsync(x => x.Status != "Completed");

//         var totalRevenue = await _db.Orders
//             .Where(x => x.Status == "Completed")
//             .SumAsync(x => x.FinalAmount);

//         var lowStock = await _db.ProductVariants.CountAsync(x => x.Quantity < 10);
//         var inStock = await _db.ProductVariants.CountAsync(x => x.Quantity >= 10);

//         var topProducts = await _db.OrderDetails
//             .GroupBy(x => new { x.ProductId, x.SnapshotProductName })
//             .Select(g => new
//             {
//                 g.Key.ProductId,
//                 ProductName = g.Key.SnapshotProductName,
//                 Sold = g.Sum(x => x.Quantity)
//             })
//             .OrderByDescending(x => x.Sold)
//             .Take(5)
//             .ToListAsync();

//         var pendingOrderList = await _db.Orders
//             .Where(x => x.Status != "Completed")
//             .OrderByDescending(x => x.OrderDate)
//             .Take(5)
//             .Select(x => new
//             {
//                 x.Id,
//                 x.OrderCode,
//                 x.ShippingName,
//                 x.Status,
//                 x.OrderDate
//             })
//             .ToListAsync();

//         return Ok(new
//         {
//             totalOrders,
//             pendingOrders,
//             totalRevenue,
//             lowStock,
//             inStock,
//             topProducts,
//             pendingOrderList
//         });
//     }
// }