using backend.Models.Orders;

namespace backend.Models.Users
{
    public class User
    {
        public int Id { get; set; }

        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
        public required string Email { get; set; }

        public string? GoogleId { get; set; }
        public required string FullName { get; set; }
        public required string PhoneNumber { get; set; }
        public required string AvatarUrl { get; set; }

        public required string Role { get; set; } // Admin | Staff | User
        public bool IsLocked { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<UserAddress> Addresses { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}