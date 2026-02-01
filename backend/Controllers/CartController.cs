using Microsoft.AspNetCore.Mvc;
using backend.DTOs.Cart;
using backend.Models.Cart;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CartController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost("add")]
        public IActionResult Add(AddToCartDto dto)
        {
            var cart = new CartItem
            {
                UserId = 1,
                ProductId = dto.ProductId,
                ProductVariantId = dto.ProductVariantId,
                Quantity = dto.Quantity
            };

            _db.CartItems.Add(cart);
            _db.SaveChanges();

            return Ok(cart);
        }

        [HttpGet]
        public IActionResult GetCart()
        {
            return Ok(_db.CartItems.ToList());
        }
    }
}
