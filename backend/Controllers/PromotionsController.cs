using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using backend.DTOs.Promotions;
using backend.Models.Promotions;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/promotions")]
    public class PromotionsController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public PromotionsController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet("coupons")]
        public IActionResult GetCoupons()
        {
            var coupons = _db.Coupons.ToList();
            var result = _mapper.Map<List<CouponDto>>(coupons);
            return Ok(result);
        }

        [HttpGet("coupons/{id}")]
        public IActionResult GetCouponById(Guid id)
        {
            var coupon = _db.Coupons.Find(id);
            if (coupon == null)
                return NotFound();

            var result = _mapper.Map<CouponDto>(coupon);
            return Ok(result);
        }

        [HttpPost("coupons")]
        public IActionResult CreateCoupon(CreateCouponDto dto)
        {
            if (dto == null)
                return BadRequest();

            var coupon = _mapper.Map<Coupon>(dto);
            _db.Coupons.Add(coupon);
            _db.SaveChanges();

            var result = _mapper.Map<CouponDto>(coupon);
            return CreatedAtAction(nameof(GetCouponById), new { id = coupon.Id }, result);
        }

        [HttpPut("coupons/{id}")]
        public IActionResult UpdateCoupon(Guid id, UpdateCouponDto dto)
        {
            if (dto == null)
                return BadRequest();

            var coupon = _db.Coupons.Find(id);
            if (coupon == null)
                return NotFound();

            _mapper.Map(dto, coupon);
            _db.SaveChanges();

            var result = _mapper.Map<CouponDto>(coupon);
            return Ok(result);
        }

        [HttpDelete("coupons/{id}")]
        public IActionResult DeleteCoupon(Guid id)
        {
            var coupon = _db.Coupons.Find(id);
            if (coupon == null)
                return NotFound();

            _db.Coupons.Remove(coupon);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpGet("user-coupons")]
        public IActionResult GetUserCoupons()
        {
            var userCoupons = _db.UserCoupons
                .Include(uc => uc.User)
                .Include(uc => uc.Coupon)
                .ToList();

            var result = _mapper.Map<List<UserCouponDto>>(userCoupons);
            return Ok(result);
        }

        [HttpGet("user-coupons/{id}")]
        public IActionResult GetUserCouponById(Guid id)
        {
            var userCoupon = _db.UserCoupons
                .Include(uc => uc.User)
                .Include(uc => uc.Coupon)
                .FirstOrDefault(uc => uc.Id == id);

            if (userCoupon == null)
                return NotFound();

            var result = _mapper.Map<UserCouponDto>(userCoupon);
            return Ok(result);
        }

        [HttpPost("user-coupons")]
        public IActionResult CreateUserCoupon(CreateUserCouponDto dto)
        {
            if (dto == null)
                return BadRequest(new { success = false, message = "Request body is required" });

            var userExists = _db.Users.Any(u => u.Id == dto.UserId && !u.IsDeleted);
            if (!userExists)
                return NotFound(new { success = false, message = "User not found" });

            var couponExists = _db.Coupons.Any(c => c.Id == dto.CouponId);
            if (!couponExists)
                return NotFound(new { success = false, message = "Coupon not found" });

            if (_db.UserCoupons.Any(uc => uc.UserId == dto.UserId && uc.CouponId == dto.CouponId))
                return BadRequest(new { success = false, message = "This user already has the coupon" });

            var userCoupon = _mapper.Map<UserCoupon>(dto);
            _db.UserCoupons.Add(userCoupon);
            _db.SaveChanges();

            var result = _mapper.Map<UserCouponDto>(userCoupon);
            return CreatedAtAction(nameof(GetUserCouponById), new { id = userCoupon.Id }, result);
        }

        [HttpPut("user-coupons/{id}")]
        public IActionResult UpdateUserCoupon(Guid id, UpdateUserCouponDto dto)
        {
            if (dto == null)
                return BadRequest();

            var userCoupon = _db.UserCoupons.Find(id);
            if (userCoupon == null)
                return NotFound();

            _mapper.Map(dto, userCoupon);
            _db.SaveChanges();

            var result = _mapper.Map<UserCouponDto>(userCoupon);
            return Ok(result);
        }

        [HttpDelete("user-coupons/{id}")]
        public IActionResult DeleteUserCoupon(Guid id)
        {
            var userCoupon = _db.UserCoupons.Find(id);
            if (userCoupon == null)
                return NotFound();

            _db.UserCoupons.Remove(userCoupon);
            _db.SaveChanges();

            return NoContent();
        }
    }
}