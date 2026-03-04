namespace backend.DTOs.Users
{
    public class UpdateUserRequestDto
    {
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? AvatarUrl { get; set; }
        public bool? IsLocked { get; set; }
        public string? Role { get; set; }
    }
}
