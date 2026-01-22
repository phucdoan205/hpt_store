public class User
{
    public int Id { get; set; }

    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }

    public string? GoogleId { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string AvatarUrl { get; set; }

    public string Role { get; set; } // Admin | Staff | User
    public bool IsLocked { get; set; }
    public DateTime CreatedAt { get; set; }

    public ICollection<UserAddress> Addresses { get; set; }
    public ICollection<Order> Orders { get; set; }
}
