using backend.Models.Orders;
using backend.Models.Products;
using backend.Models.Base;
using backend.Models.Promotions;

namespace backend.Models.Users
{
    public class User : BaseModel
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }

        public string Email { get; set; }
        public string? GoogleId { get; set; }

        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? AvatarUrl { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public bool IsLocked { get; set; }

        // Navigation
        public ICollection<Order> Orders { get; set; }
        public ICollection<ProductReview> Reviews { get; set; }
        public ICollection<UserAddress> Addresses { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<UserCoupon> UserCoupons { get; set; }
    }
}