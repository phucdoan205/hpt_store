using backend.Models.Promotions;

namespace backend.DTOs.Promotions
{
    public class CouponDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal DiscountValue { get; set; }
        public bool IsPercentage { get; set; }
        public decimal? MaxDiscountAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public CouponType Type { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateCouponDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal DiscountValue { get; set; }
        public bool IsPercentage { get; set; }
        public decimal? MaxDiscountAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public CouponType Type { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class UpdateCouponDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal DiscountValue { get; set; }
        public bool IsPercentage { get; set; }
        public decimal? MaxDiscountAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public CouponType Type { get; set; }
        public bool IsActive { get; set; }
    }
}