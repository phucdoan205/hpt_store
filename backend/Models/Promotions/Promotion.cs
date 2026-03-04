using backend.Models.Base;

namespace backend.Models.Promotions
{
    public class Promotion : BaseModel
    {
        public string Name { get; set; }

        public string DiscountType { get; set; } // Percentage / Fixed
        public decimal DiscountValue { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }
        public int Priority { get; set; }

        public ICollection<ProductPromotion> ProductPromotions { get; set; }
        public ICollection<PromotionCondition> Conditions { get; set; }
        public ICollection<Coupon> Coupons { get; set; }
    
    }
}