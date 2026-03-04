using Microsoft.AspNetCore.Mvc;
using backend.DTOs.Shopping.Cart;
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
        public async Task<IActionResult> Add(AddToCartRequestDto dto)
        {
            var sub = User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)?.Value;
            var userId = sub != null ? Guid.Parse(sub) : Guid.Empty;

            var cartEntity = _db.Carts.FirstOrDefault(c => c.UserId == userId);
            if (cartEntity == null)
            {
                cartEntity = new Cart { UserId = userId };
                _db.Carts.Add(cartEntity);
                _db.SaveChanges();
            }

            var cartItem = new CartItem
            {
                CartId = cartEntity.Id,
                ProductVariantId = dto.ProductId,
                Quantity = dto.Quantity
            };

            _db.CartItems.Add(cartItem);
            _db.SaveChanges();

            return Ok(cartItem);
        }

        [HttpGet]
        public IActionResult GetCart()
        {
            return Ok(_db.CartItems.ToList());
        }
    }
}
