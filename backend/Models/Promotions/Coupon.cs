using backend.Models.Base;
using backend.Models.Users;

namespace backend.Models.Promotions
{
    public class Coupon : BaseModel
    {
        public string Name { get; set; }              // Tên hiển thị
        public string Code { get; set; }              // Mã nhập

        public decimal DiscountValue { get; set; }    // Giá trị giảm
        public bool IsPercentage { get; set; }        // % hay tiền mặt

        public decimal? MaxDiscountAmount { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        public CouponType Type { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<UserCoupon> UserCoupons { get; set; }
    }
}