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
            if (dto == null)
                return BadRequest(new { success = false, message = "Request body is required" });

            var sub = User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)?.Value;
            if (string.IsNullOrEmpty(sub) || !Guid.TryParse(sub, out var userId))
                return Unauthorized(new { success = false, message = "User not authenticated" });

            var variantExists = _db.ProductVariants.Any(v => v.Id == dto.ProductId);
            if (!variantExists)
                return NotFound(new { success = false, message = "Product variant not found" });

            var cartEntity = _db.Carts.FirstOrDefault(c => c.UserId == userId);
            if (cartEntity == null)
            {
                cartEntity = new Cart { UserId = userId };
                _db.Carts.Add(cartEntity);
                await _db.SaveChangesAsync();
            }

            var existingItem = _db.CartItems.FirstOrDefault(ci => ci.CartId == cartEntity.Id && ci.ProductVariantId == dto.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += dto.Quantity;
                _db.CartItems.Update(existingItem);
                await _db.SaveChangesAsync();
                return Ok(existingItem);
            }

            var cartItem = new CartItem
            {
                CartId = cartEntity.Id,
                ProductVariantId = dto.ProductId,
                Quantity = dto.Quantity
            };

            _db.CartItems.Add(cartItem);
            await _db.SaveChangesAsync();

            return Ok(cartItem);
        }

        [HttpGet]
        public IActionResult GetCart()
        {
            var sub = User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)?.Value;
            if (string.IsNullOrEmpty(sub) || !Guid.TryParse(sub, out var userId))
                return Unauthorized(new { success = false, message = "User not authenticated" });

            var cartItems = _db.CartItems
                .Where(ci => ci.Cart.UserId == userId)
                .ToList();

            return Ok(cartItems);
        }
    }
}
