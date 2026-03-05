namespace backend.DTOs.Promotions
{
    public class UserCouponDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public Guid CouponId { get; set; }
        public string CouponName { get; set; }
        public bool IsUsed { get; set; }
        public DateTime? UsedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateUserCouponDto
    {
        public Guid UserId { get; set; }
        public Guid CouponId { get; set; }
    }

    public class UpdateUserCouponDto
    {
        public bool IsUsed { get; set; }
    }
}