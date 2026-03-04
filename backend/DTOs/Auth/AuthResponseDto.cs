namespace backend.DTOs.Auth
{
    public class AuthResponseDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public DateTime AccessTokenExpiry { get; set; }

        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public string FullName { get; set; }
        public string? AvatarUrl { get; set; }
    }
}