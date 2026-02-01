namespace backend.DTOs.Auth
{
    public class CreateStaffDto
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public required string FullName { get; set; }
        public required string PhoneNumber { get; set; }
        public required string AvatarUrl { get; set; }

        public bool IsLocked { get; set; }
    }
}