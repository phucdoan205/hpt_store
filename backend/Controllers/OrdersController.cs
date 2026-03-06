using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using backend.DTOs.Shopping.Order;
using backend.Models.Orders;


namespace backend.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public OrdersController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderRequestDto dto)
        {
            if (dto == null)
                return BadRequest(new { success = false, message = "Request body is required" });

            var sub = User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)?.Value;
            if (string.IsNullOrEmpty(sub) || !Guid.TryParse(sub, out var userId))
                return Unauthorized(new { success = false, message = "User not authenticated" });

            var user = await _db.Users.FindAsync(userId);
            if (user == null || user.IsDeleted)
                return Unauthorized(new { success = false, message = "User not found" });

            var order = _mapper.Map<Order>(dto);
            order.UserId = userId;
            order.OrderDate = DateTime.UtcNow;
            order.Status = "Pending";
            order.OrderCode = Guid.NewGuid().ToString();

            order.ShippingName = user.FullName ?? user.Username;
            order.ShippingPhone = user.PhoneNumber ?? string.Empty;
            order.ShippingAddress = dto.ShippingAddress;

            order.PaymentStatus = "Unpaid";
            order.TotalAmount = 0m;
            order.DiscountAmount = 0m;
            order.ShippingFee = 0m;
            order.FinalAmount = 0m;

            order.PaymentMethod = dto.PaymentMethod;

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            return Ok(order);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_db.Orders.ToList());
        }
    }
}
