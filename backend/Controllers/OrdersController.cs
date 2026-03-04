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
            var order = _mapper.Map<Order>(dto);
            var sub = User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)?.Value;
            order.UserId = sub != null ? Guid.Parse(sub) : Guid.Empty;
            order.OrderDate = DateTime.UtcNow;
            order.Status = "Pending";

            _db.Orders.Add(order);
            _db.SaveChanges();

            return Ok(order);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_db.Orders.ToList());
        }
    }
}
