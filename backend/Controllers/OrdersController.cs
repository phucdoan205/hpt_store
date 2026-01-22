using Microsoft.AspNetCore.Mvc;
using AutoMapper;

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
        public IActionResult Create(CreateOrderDto dto)
        {
            var order = _mapper.Map<Order>(dto);
            order.UserId = 1;
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
