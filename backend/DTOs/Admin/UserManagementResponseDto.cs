namespace backend.DTOs.Admin
{
    public class UserManagementResponseDto
    {
        public Guid Id { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }

        public string FullName { get; set; }

        public string Role { get; set; }

        public bool IsActive { get; set; }
    }
}