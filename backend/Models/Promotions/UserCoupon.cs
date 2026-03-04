using backend.Models.Base;
using backend.Models.Users;

namespace backend.Models.Promotions
{
    public class UserCoupon : BaseModel
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid CouponId { get; set; }
        public Coupon Coupon { get; set; }

        public bool IsUsed { get; set; } = false;
        public DateTime? UsedAt { get; set; }
    }
}