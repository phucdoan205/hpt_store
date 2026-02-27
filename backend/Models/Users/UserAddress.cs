namespace backend.Models.Users
{
    public class UserAddress
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public required string ContactName { get; set; }
        public required string ContactPhone { get; set; }
        public required string AddressLine { get; set; }
        public required string Province { get; set; }
        public required string District { get; set; }
        public required string Ward { get; set; }

        public bool IsDefault { get; set; }
    }
}